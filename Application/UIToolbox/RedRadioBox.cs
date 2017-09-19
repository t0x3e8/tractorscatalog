using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace Enceladus.UIToolbox
{
    public partial class RedRadioBox : UserControl
    {
        #region Fields and Properties
        protected readonly Size BoxSize = new Size(12, 12);
        protected readonly int boxLeftPadding = 10;
        protected readonly int spaceBetweenBoxAndText = 10;
        public event EventHandler CheckboxClicked;

        protected int clickEdge = 3;
        public virtual int ClickEdge
        {
            get { return this.clickEdge; }
            set { this.clickEdge = value; }
        }

        protected bool isChecked = false;
        public virtual bool IsChecked
        {
            get { return this.isChecked; }
            set
            {
                this.isChecked = value;
                this.Invalidate();
            }
        }

        [Browsable(true)]
        protected string content;
        public virtual string Content
        {
            get { return this.content; }
            set
            {
                this.content = value;
                this.Invalidate();
            }
        }
        #endregion

        #region Constructors
        public RedRadioBox()
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
        protected override void OnPaint(PaintEventArgs pevent)
        {
            pevent.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            pevent.Graphics.TextRenderingHint = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;

            this.FillBox(pevent.Graphics);
            this.DrawBoxBorder(pevent.Graphics);

            if (this.isChecked)
                this.DrawBall(pevent.Graphics);

            if (string.IsNullOrEmpty(this.Content) == false)
                this.DrawText(pevent.Graphics);
        }

        private void DrawText(Graphics g)
        {
            int x = this.boxLeftPadding + this.BoxSize.Width + this.spaceBetweenBoxAndText;
            int y = this.CalculateBoxHeight();
            using (SolidBrush brush = new SolidBrush(Defines.CarrotColor))
            {
                g.DrawString(this.Content, Defines.TinyBoldFont, brush, new Point(x, y));
            }
        }

        private void DrawBall(Graphics g)
        {
            using (Image image = Resource1.ball_red8x8)
            {
                g.DrawImage(image, new Rectangle(this.boxLeftPadding + 2, this.CalculateBoxHeight() + 2, this.BoxSize.Width - 4, this.BoxSize.Height - 4));
            }
        }

        protected virtual void FillBox(Graphics g)
        {
            using (SolidBrush brush = new SolidBrush(Defines.MangoColor))
            {
                g.FillEllipse(brush, this.boxLeftPadding, this.CalculateBoxHeight(), this.BoxSize.Width, this.BoxSize.Height);
            }
        }
        
        protected virtual void DrawBoxBorder(Graphics g)
        {
            using (Pen pen = new Pen(Defines.MilkColor, 1.0f))
            {
                g.DrawEllipse(pen, this.boxLeftPadding, this.CalculateBoxHeight(), this.BoxSize.Width, this.BoxSize.Height);
            }
        }

        protected virtual int CalculateBoxHeight()
        {
            return (this.Height / 2) - (this.BoxSize.Height / 2);
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            base.OnMouseDown(e);

            if (this.IsClicked(e.Location))
            {
                if (this.CheckboxClicked != null)
                    this.CheckboxClicked(this, new EventArgs());

                this.isChecked = !this.isChecked;

                this.Invalidate();
            }
        }

        protected virtual bool IsClicked(Point mousePosition)
        {
            Rectangle rect = new Rectangle(this.boxLeftPadding - this.clickEdge,
                this.CalculateBoxHeight() - this.clickEdge,
                this.BoxSize.Width + (2 * this.clickEdge),
                this.BoxSize.Height + (2 * this.clickEdge));
            return rect.Contains(mousePosition);
        }
        #endregion
    }
}
