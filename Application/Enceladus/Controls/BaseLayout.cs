using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace Enceladus
{
    partial class BaseLayout : UserControl, IChangeLanguage
    {
        #region Fields and Properties
        private MainWindow window;
        public MainWindow Window
        {
            get { return this.window; }
        }

        #endregion

        #region Constructors
        public BaseLayout()
        {
            this.DoubleBuffered = true;
            this.SetStyle(ControlStyles.DoubleBuffer | ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint, true);
            this.SetStyle(ControlStyles.ResizeRedraw, false);
            this.SetStyle(ControlStyles.FixedHeight, true);
            this.SetStyle(ControlStyles.FixedWidth, true);
            this.SetStyle(ControlStyles.Opaque, true);

            InitializeComponent();
        }

        public BaseLayout(MainWindow window)
            : this()
        {
            this.window = window;
        }

        protected override void OnControlAdded(ControlEventArgs e)
        {
            base.OnControlAdded(e);
            this.ChangeLanguage();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            this.PaintParentBackground(e);

            if (this.BackgroundImage != null)
            {
                if (this.BackgroundImageLayout == ImageLayout.Stretch)
                    e.Graphics.DrawImage(this.BackgroundImage, new Rectangle(0, 0, this.Width, this.Height));
                else
                    e.Graphics.DrawImage(this.BackgroundImage, new Point(0, 0));
            }
            else
                e.Graphics.FillRectangle(new SolidBrush(this.BackColor), new Rectangle(0, 0, this.Width, this.Height));
        }

        public virtual void Activate() { }

        public virtual void Deactivate() { }

        public virtual void ChangeLanguage() { }

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

        #endregion
    }
}
