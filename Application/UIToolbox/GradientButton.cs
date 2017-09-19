using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Drawing.Drawing2D;
using System.Diagnostics;
using Enceladus.Api;

namespace Enceladus.UIToolbox
{
    public partial class GradientButton : UserControl
    {
        #region Fields and Properties
        protected bool isClicked;
        protected bool isHover;
        public virtual ICommand Command { get; set; }
        public new string Text { get; set; }        
        public event EventHandler<CommandEventArgs> CommandExecuting;
        #endregion

        #region Constructors
        public GradientButton()
        {
            InitializeComponent();

            this.SetStyle(ControlStyles.UserPaint, true);
            this.SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            this.SetStyle(ControlStyles.DoubleBuffer, true);
            this.SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
            this.SetStyle(ControlStyles.SupportsTransparentBackColor, true);
            this.UpdateStyles();
        }
        #endregion

        #region Methods
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

        protected override void OnPaint(PaintEventArgs pevent)
        {
            pevent.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            pevent.Graphics.TextRenderingHint = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;

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

            this.PaintParentBackground(pevent);
            this.DrawBackground(pevent.Graphics);
            this.DrawText(pevent.Graphics);          
        }

        protected override void OnPaintBackground(PaintEventArgs pevent)
        {
            
        }

        protected virtual void DrawBackground(Graphics g)
        {
            if (this.isClicked)
                g.DrawImage(Resource1.Button_Clicked, 0, 0, this.Width, this.Height);
            else if (this.isHover)
                g.DrawImage(Resource1.Button_Hover, 0, 0, this.Width, this.Height);
            else
                g.DrawImage(Resource1.Button, 0, 0, this.Width, this.Height);
        }

        protected virtual void DrawText(Graphics g)
        {
            StringFormat sf = DrawingToolbox.CreateStringFormat(StringAlignment.Center, StringAlignment.Center);
            using (SolidBrush brush = new SolidBrush(this.Enabled ? Defines.GarlicColor : Defines.GrapeColor))
            {
                g.DrawString(this.Text, Defines.EnormousFont, brush, new RectangleF(0, 0, this.Width, this.Height), sf);
            }
        }

        protected override void OnMouseDown(MouseEventArgs mevent)
        {
            base.OnMouseDown(mevent);
            if (mevent.Button == System.Windows.Forms.MouseButtons.Left)
            {
                this.isClicked = true;
                this.Invalidate();
            }
        }

        protected override void OnMouseUp(MouseEventArgs mevent)
        {
            base.OnMouseUp(mevent);
            if (this.isClicked)
            {
                this.isClicked = false;
                this.Invalidate();
            }
        }

        protected override void OnMouseEnter(EventArgs e)
        {
            base.OnMouseHover(e);
            this.isHover = true;
            this.Cursor = Cursors.Hand;
            this.Invalidate();
        }

        protected override void OnMouseLeave(EventArgs e)
        {
            base.OnMouseLeave(e);
            this.isHover = false;
            this.Cursor = Cursors.Default;
            this.Invalidate();
        }

        protected override void OnMouseClick(MouseEventArgs e)
        {
            if (this.Command != null)
            {
                CommandEventArgs eventArgs = new CommandEventArgs();

                if (this.CommandExecuting != null)
                    this.CommandExecuting(this, eventArgs);

                if (!eventArgs.Cancel && this.Command.AutoCommandExecution)
                    this.Command.Execute<object>(eventArgs.CommandArgument);
            }
        }

        protected override void OnResize(EventArgs e)
        {
            this.Invalidate();
        }
        #endregion
    }
}
