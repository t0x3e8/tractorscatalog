using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using Enceladus.StringLibrary;

namespace Enceladus.UIToolbox
{
    public partial class BrandInfoBox : UserControl
    {
        #region Fields and Properties
        protected Side side;
        public virtual Side Side
        {
            get { return this.side; }
            set
            {
                this.side = value;
                this.Invalidate();
            }
        }

        public virtual Uri HomePageLink { get; set; }

        protected Image logo;
        public virtual Image Logo
        {
            get { return this.logo; }
            set
            {
                this.logo = value;
                this.Invalidate();
            }
        }
        protected Point logoPosition;
        public virtual Point LogoPosition
        {
            get { return this.logoPosition; }
            set
            {
                this.logoPosition = value;
                this.Invalidate();
            }
        }
        protected string[] brandData = new string[0];
        public string[] BrandData
        {
            get { return this.brandData; }
            set
            {
                this.brandData = value;
                this.Invalidate();
            }
        }
        protected Point textPosition;
        public virtual Point TextPosition
        {
            get { return this.textPosition; }
            set
            {
                this.textPosition = value;
                this.Invalidate();
            }
        }

        protected Rectangle EmailBounds { get; set; }
        protected string Email { get; set; }
        #endregion

        #region Constructors
        public BrandInfoBox()
        {
            InitializeComponent();

            this.SetStyle(ControlStyles.UserPaint, true);
            this.SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            this.SetStyle(ControlStyles.DoubleBuffer, true);
            this.SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
            this.SetStyle(ControlStyles.SupportsTransparentBackColor, true);
            this.UpdateStyles();

            this.Size = new Size(264, 265);
            this.LogoPosition = new Point(30, 20);
            this.EmailBounds = Rectangle.Empty;
            this.Email = string.Empty;
        }
        #endregion

        #region Methods
        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            e.Graphics.TextRenderingHint = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;

            // set the rounded region
            System.IntPtr ptrBorder = DrawingToolbox.CreateRoundRectRgn(0, 0, this.Width, this.Height, 6, 6);
            try
            {
                this.Region = System.Drawing.Region.FromHrgn(ptrBorder);
            }
            finally
            {
                DrawingToolbox.DeleteObject(ptrBorder);
            }

            this.PaintParentBackground(e);
            this.DrawBox(e.Graphics);
            this.DrawLogo(e.Graphics);
            this.DrawInformation(e.Graphics);
        }


        private void PaintParentBackground(PaintEventArgs e)
        {
            if (Parent != null)
            {
                Rectangle rect = new Rectangle(Left, Top, Width, Height);
                e.Graphics.TranslateTransform(-rect.X, -rect.Y);

                try
                {
                    using (PaintEventArgs pea = new PaintEventArgs(e.Graphics, rect))
                    {
                        pea.Graphics.SetClip(rect);
                        InvokePaintBackground(Parent, pea);
                        InvokePaint(Parent, pea);
                    }
                }
                finally
                {
                    e.Graphics.TranslateTransform(rect.X, rect.Y);
                }
            }
            else
            {
                e.Graphics.FillRectangle(SystemBrushes.Control, ClientRectangle);
            }
        }

        protected void DrawInformation(Graphics g)
        {
            int padding = 1;
            int y = this.TextPosition.Y;
            int emptyLine = 15;

            for (int i = 0; i < this.BrandData.Length; i++)
            {
                string line = this.BrandData[i];
                if (string.IsNullOrEmpty(line))
                {
                    y += emptyLine;
                    continue;
                }

                if (line.Contains("@"))
                {
                    int emailStarts = line.LastIndexOf(" ", line.IndexOf("@")) + 1;
                    SizeF textSizeBeforeEmail = g.MeasureString(line.Substring(0, emailStarts), this.Font);
                    SizeF emailSize = g.MeasureString(line.Substring(emailStarts), Defines.NormalUnderlineFont);

                    using (Brush brush = new SolidBrush(Defines.PepperColor))
                    {
                        this.Email = line.Substring(emailStarts);
                        g.DrawString(this.Email, Defines.NormalUnderlineFont, brush, new PointF(this.TextPosition.X + textSizeBeforeEmail.Width, y));
                    }

                    this.EmailBounds = new Rectangle(this.TextPosition.X + (int)textSizeBeforeEmail.Width, y, (int)emailSize.Width, (int)emailSize.Height);
                    line = line.Substring(0, emailStarts);
                }

                using (Brush brush = new SolidBrush(this.ForeColor))
                {
                    g.DrawString(line, this.Font, brush, new PointF(this.TextPosition.X, y));
                }


                y += (int)g.MeasureString(line, this.Font).Height + padding;
            }
        }

        protected void DrawLogo(Graphics g)
        {
            if (this.Logo != null)
            {
                g.DrawImageUnscaled(this.Logo, this.LogoPosition);
            }
        }

        protected void DrawBox(Graphics g)
        {
            Image boxImage = (this.Side == UIToolbox.Side.Left) ? Resource1.LeftBox : Resource1.RightBox;
            g.DrawImage(boxImage, 0, 0, this.Width, this.Height);
        }

        protected override void OnMouseEnter(EventArgs e)
        {
            base.OnMouseEnter(e);
            this.Cursor = Cursors.Hand;
        }

        protected override void OnMouseLeave(EventArgs e)
        {
            base.OnMouseLeave(e);
            this.Cursor = Cursors.Default;
        }

        protected override void OnMouseClick(MouseEventArgs e)
        {
            base.OnMouseClick(e);
            
            if (e.Button == System.Windows.Forms.MouseButtons.Left)
            {
                if (this.EmailBounds.Contains(e.Location))
                {
                    try
                    {
                        System.Diagnostics.Process proc = new System.Diagnostics.Process();
                        proc.StartInfo.FileName = string.Format("mailto:{0}?subject=Schleppermarkt&body= ", this.Email);
                        proc.Start();
                    }
                    catch
                    {
                        MessageBox.Show(ResourceReader.GetString("MsgNoAssosiatedEmailTool"), ResourceReader.GetString("MsgError"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                {
                    if (this.HomePageLink != null)
                    {
                        try
                        {
                            System.Diagnostics.Process.Start(this.HomePageLink.AbsoluteUri);
                        }
                        catch { }
                    }
                }
            }
        }
        #endregion
    }

    public enum Side
    {
        Left, Right
    }
}
