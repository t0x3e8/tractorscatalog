using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace Enceladus.UIToolbox
{
    public partial class DoubleBufferedPictureBox : PictureBox
    {
        public DoubleBufferedPictureBox()
        {
            InitializeComponent();

            this.SetStyle(ControlStyles.UserPaint, true);
            this.SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            this.SetStyle(ControlStyles.DoubleBuffer, true);
            this.SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
            this.SetStyle(ControlStyles.SupportsTransparentBackColor, true);
            this.UpdateStyles();
        }

        protected override void OnPaint(PaintEventArgs pe)
        {
            base.OnPaint(pe);

            pe.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            pe.Graphics.TextRenderingHint = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;

            this.PaintParentBackground(pe);

            if (this.Image != null)
            {
                if (this.BackgroundImageLayout == ImageLayout.Stretch)
                    pe.Graphics.DrawImage(this.Image, 0, 0, this.Width + 1, this.Height + 1);
                else
                    pe.Graphics.DrawImage(this.Image, 0, 0, this.Image.Width + 1, this.Image.Height + 1);
            }
        }

        protected override void OnPaintBackground(PaintEventArgs pevent)
        {
            // this is empty on purpuse
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
                e.Graphics.FillRectangle(Brushes.Transparent, ClientRectangle);
            }
        }
    }
}
