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
    public partial class InfoBox : UserControl
    {
        #region Fields and Properties
        protected string[] descritpion = new string[0];
        public virtual string[] Description
        {
            get { return this.descritpion; }
            set
            {
                this.descritpion = value;
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
        public InfoBox()
        {
            InitializeComponent();

            this.SetStyle(ControlStyles.UserPaint, true);
            this.SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            this.SetStyle(ControlStyles.DoubleBuffer, true);
            this.SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
            this.SetStyle(ControlStyles.SupportsTransparentBackColor, true);
            this.UpdateStyles();

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

            this.DrawText(e.Graphics);
        }

        protected void DrawText(Graphics g)
        {
            int padding = 0;
            int y = this.TextPosition.Y;
            int emptyLine = 15;

            for (int i = 0; i < this.Description.Length; i++)
            {
                string line = this.Description[i];
                if (string.IsNullOrEmpty(line))
                {
                    y += emptyLine;
                    continue;
                }

                if (line.Contains("@"))
                {
                    int emailStarts = line.LastIndexOf(" ", line.IndexOf("@")) + 1;
                    SizeF textSizeBeforeEmail = TextRenderer.MeasureText(line.Substring(0, emailStarts), this.Font);
                    SizeF emailSize = TextRenderer.MeasureText(line.Substring(emailStarts), Defines.NormalUnderlineFont);

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
            }
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);

            bool isOverEmailBounds = this.EmailBounds.Contains(e.Location);
            if (isOverEmailBounds)
                this.Cursor = Cursors.Hand;
            else
                this.Cursor = Cursors.Default;
        }
        #endregion
    }
}
