using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using Enceladus.Api.UI;

namespace Enceladus.UIToolbox
{
    public partial class RedCheckBox : UserControl, IResizableClient
    {
        #region Fields and Properties
        protected readonly Size BoxSize = new Size(12, 12);
        protected readonly int boxLeftPadding = 10;
        protected readonly int spaceBetweenBoxAndText = 10;
        public event EventHandler CheckboxChecked;

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
        public RedCheckBox()
        {
            this.SetStyle(ControlStyles.UserPaint, true);
            this.SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            this.SetStyle(ControlStyles.DoubleBuffer, true);
            this.SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
            this.SetStyle(ControlStyles.SupportsTransparentBackColor, true);
            this.UpdateStyles();

            this.MaximalExpectedFontSize = 5;
            this.SupportResizing = true;
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
                this.DrawTickMark(pevent.Graphics);

            if (string.IsNullOrEmpty(this.Content) == false)
                this.DrawText(pevent.Graphics);
        }

        private void DrawText(Graphics g)
        {
            int x = this.boxLeftPadding + this.BoxSize.Width + this.spaceBetweenBoxAndText;
            int y = this.CalculateBoxHeight() - this.lastFontSizeChange/ 2;
            using (SolidBrush brush = new SolidBrush(Defines.CarrotColor))
            {
                g.DrawString(this.Content, this.DetermineFont(this.CurrentFontSize), brush, new Point(x, y));
            }
        }

        private void DrawTickMark(Graphics g)
        {
            using (Image image = Resource1.RedTick)
            {
                g.DrawImage(image, this.boxLeftPadding, this.CalculateBoxHeight());
            } 
        }

        protected virtual void FillBox(Graphics g)
        {
            using (SolidBrush brush = new SolidBrush(Defines.MangoColor))
            {
                g.FillRectangle(brush, this.boxLeftPadding, this.CalculateBoxHeight(), this.BoxSize.Width, this.BoxSize.Height);
            }
        }

        protected virtual void DrawBoxBorder(Graphics g)
        {
            using (Pen pen = new Pen(Defines.MilkColor, 1.0f))
            {
                g.DrawRectangle(pen, this.boxLeftPadding, this.CalculateBoxHeight(), this.BoxSize.Width, this.BoxSize.Height);
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
                this.isChecked = !this.isChecked;

                if (this.CheckboxChecked != null)
                    this.CheckboxChecked(this, new EventArgs());

                this.Invalidate();
            }
        }

        protected virtual bool IsClicked(Point mousePosition)
        {
            Rectangle rect = new Rectangle(this.boxLeftPadding - this.clickEdge , 
                this.CalculateBoxHeight() - this.clickEdge, 
                this.BoxSize.Width + (2 * this.clickEdge), 
                this.BoxSize.Height + (2 * this.clickEdge));
            return rect.Contains(mousePosition);
        }
        #endregion

        #region IResizeClient implementation
        private int lastFontSizeChange = 0;
        public virtual void ApplyFontSize(int fontSizeChange)
        {
            if (this.lastFontSizeChange != fontSizeChange)
            {
                this.lastFontSizeChange = fontSizeChange;
                this.CurrentFontSize = FontSize.Tiny + fontSizeChange - 1;

                this.Font = this.DetermineFont(this.CurrentFontSize);
            }
        }

        public FontSize CurrentFontSize { get; protected set; }

        private int expectedFontSize;
        public virtual int MaximalExpectedFontSize
        {
            get { return this.expectedFontSize; }
            set
            {
                if (this.expectedFontSize != value)
                {
                    this.expectedFontSize = value;
                    if (this.InformResizer != null)
                        this.InformResizer();
                }
            }
        }

        public Font DetermineFont(FontSize fontSize)
        {
            switch (fontSize)
            {
                case FontSize.Tiny: return Defines.TinyBoldFont;
                case FontSize.Small: return Defines.SmallBoldFont;
                case FontSize.Normal: return Defines.SmallBoldFont;
                case FontSize.Big: return Defines.NormalFont;
                case FontSize.Huge: return Defines.NormalFont;
                default: return Defines.TinyBoldFont;
            }
        }

        public PokeDelegate InformResizer { get; set; }
        public bool SupportResizing { get; set; }
        #endregion
    }
}
