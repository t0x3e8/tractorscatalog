using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Drawing.Drawing2D;
using Enceladus.Api.UI;

namespace Enceladus.UIToolbox
{
    public struct DualScrollerDisplayValue
    {
        public string  Up;
        public string Down;

        public DualScrollerDisplayValue(string up, string Down)
        {
            this.Up = up;
            this.Down = Down;
        }
    }

    public partial class DualScroller : ScrollerBase, IResizableClient
    {
        #region Fields and Properties
        protected Point thumbLPosition = Point.Empty;
        protected Point thumbRPosition = Point.Empty;

        protected bool isLSelected = false;
        protected bool isRSelected = false;

        protected int valueL = 10;
        public virtual int ValueLeft
        {
            get { return this.valueL; }
            set
            {
                if (value > 0)
                    this.valueL = value;
                else
                    this.valueL = 0;
                
                if (ValueChanged != null)
                    this.ValueChanged(this, new EventArgs());

                this.Invalidate();
            }
        }

        protected DualScrollerDisplayValue displayLeftValue;
        public DualScrollerDisplayValue DisplayLeftValue
        {
            get { return this.displayLeftValue; }
            set { this.displayLeftValue = value; }
        }

        protected int valueR = 20;
        public virtual int ValueRight
        {
            get { return this.valueR; }
            set
            {
                if (value > 0)
                    this.valueR = value;
                else
                    this.valueR = 0;

                if (ValueChanged != null)
                    this.ValueChanged(this, new EventArgs());

                this.Invalidate();
            }
        }
        
        protected DualScrollerDisplayValue displayRightValue;
        public DualScrollerDisplayValue DisplayRightValue
        {
            get { return this.displayRightValue; }
            set { this.displayRightValue = value; }
        }

        protected int spaceValue = 1;
        public virtual int SpaceValue
        {
            get { return this.spaceValue; }
            set { this.spaceValue = value; }
        }

        public event EventHandler ValueChanged;

        public override Size BarSize
        {
            get
            {
                Size size = new Size();
                size.Width = this.Width - this.Padding.Left - this.Padding.Right - (2 * this.ThumbSize.Width);
                size.Height = this.Height - this.Padding.Top - this.Padding.Bottom;
                return size;
            }
        }

        protected string labelUp;
        public virtual string LabelUp
        {
            get { return this.labelUp; }
            set { this.labelUp = value; }
        }

        protected string labelDown;
        public virtual string LabelDown
        {
            get { return this.labelDown; }
            set { this.labelDown = value; }
        }
        #endregion

        #region Constructors
        public DualScroller()
        {
            InitializeComponent();
            this.clickPadding = 0;
            this.labelPadding = 0;
            this.labelUp = "kW";
            this.labelDown = "PS";

            this.MaximalExpectedFontSize = 5;
            this.Step = 10;
            this.SupportResizing = true;
        }
        #endregion

        #region Methods
        protected override void OnPaint(PaintEventArgs pevent)
        {
            base.OnPaint(pevent);

            this.DrawLabels(pevent.Graphics);

            this.thumbLPosition = this.Calculate(this.valueL, this.minValue, this.valueR, false);
            this.thumbRPosition = this.Calculate(this.valueR, this.valueL, this.maxValue, true);

            this.DrawProgress(pevent.Graphics);
            this.DrawThumbs(pevent.Graphics);
            this.DrawValues(pevent.Graphics);
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            base.OnMouseDown(e);

            if (this.IsThumbClicked(this.thumbLPosition, e.Location))
                this.isLSelected = true;
            else if (this.IsThumbClicked(this.thumbRPosition, e.Location))
                this.isRSelected = true;
        }

        protected override void OnMouseUp(MouseEventArgs e)
        {
            base.OnMouseUp(e);
            if (this.isLSelected)
                this.isLSelected = false;
            else if (this.isRSelected)
                this.isRSelected = false;
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);

            if (this.isLSelected)
                this.ValueLeft = this.Calculate(e.Location, this.minValue, this.valueR, false);
            else if (this.isRSelected)
                this.ValueRight = this.Calculate(e.Location, this.valueL, this.maxValue, true);
                this.Invalidate();
        }

        protected virtual void DrawThumbs(Graphics g)
        {
            g.DrawImage(Resource1.Thumb, this.thumbLPosition.X, this.thumbLPosition.Y, this.thumbSize.Width, this.thumbSize.Height);
            g.DrawImage(Resource1.Thumb, this.thumbRPosition.X, this.thumbRPosition.Y, this.thumbSize.Width, this.thumbSize.Height);
        }

        protected virtual void DrawProgress(Graphics g)
        {
            Rectangle rect = new Rectangle(this.thumbLPosition.X + 1, this.Padding.Top, this.thumbRPosition.X - this.thumbLPosition.X, this.Height - this.Padding.Vertical);
            GraphicsPath path = this.CalculatePath(rect);

            using (SolidBrush brush = new SolidBrush(Defines.WildStawberryColor))
            {
                g.FillPath(brush, path);
            }
        }

        protected virtual bool IsThumbClicked(Point thumbPosition, Point mousePosition)
        {
            Rectangle rect = new Rectangle(
                thumbPosition.X - this.clickPadding,
                thumbPosition.Y - this.clickPadding,
                this.thumbSize.Width + (2 * this.clickPadding),
                this.thumbSize.Height + (2 * this.clickPadding));

            return rect.Contains(mousePosition);
        }

        protected virtual Point Calculate(float value, int minValue, int maxValue, bool isRight)
        {
            if (value < minValue)
                value = minValue;
            else if (value > maxValue)
                value = maxValue;

            if (isRight && value == minValue)
                value += this.spaceValue;
            else if (!isRight && value == maxValue)
                value -= this.spaceValue;

            value -= this.minValue;

            float scale = this.maxValue - this.minValue;
            double ratio = this.BarSize.Width/ scale;
            double x = value * ratio;
            x = (isRight) ? x + this.Padding.Left + this.thumbSize.Width : x + this.Padding.Left;

            int y = (this.Padding.Top + base.BarSize.Height / 2) - (this.thumbSize.Height / 2);

            return new Point((int)Math.Round(x, 0), y);
        }

        protected virtual int Calculate(Point point, int minValue, int maxValue, bool isRight)
        {
            int position = (isRight) ?  point.X - this.Padding.Left - this.thumbSize.Width : point.X - this.Padding.Left;
            float scale = this.maxValue - this.minValue;
            double ratio = scale / this.BarSize.Width;
            double value = position * ratio;

            if (value < minValue)
                value = minValue;
            else if (value > maxValue)
                value = maxValue;

            if (isRight && value == minValue)
                value += this.spaceValue + this.Step;
            else if (!isRight && value == maxValue)
                value -= this.spaceValue;

            if (value != maxValue)
                value -= value % this.Step;
            
            return (int)Math.Round(value, 0);
        }

        protected virtual void DrawValues(Graphics g)
        {
            SizeF upLTextSize = g.MeasureString(this.displayLeftValue.Up, Defines.TinyBoldFont);
            PointF upLeftPosition = new PointF(this.thumbLPosition.X - this.labelPadding - (int)upLTextSize.Width, this.thumbLPosition.Y - (int)upLTextSize.Height);
            SizeF upRTextSize = g.MeasureString(this.displayRightValue.Up, Defines.TinyBoldFont);
            PointF upRightPosition = new PointF(this.thumbRPosition.X + this.labelPadding + (int)this.thumbSize.Width, this.thumbRPosition.Y - (int)upRTextSize.Height);
            using (SolidBrush brush = new SolidBrush(Defines.CarrotColor))
            {
                g.DrawString(this.displayLeftValue.Up, Defines.TinyBoldFont, brush, upLeftPosition);
                g.DrawString(this.displayRightValue.Up, Defines.TinyBoldFont, brush, upRightPosition);
            }


            SizeF downLTextSize = g.MeasureString(this.displayLeftValue.Down, Defines.TinyBoldFont);
            PointF downLeftPosition = new PointF(this.thumbLPosition.X - this.labelPadding - (int)upLTextSize.Width, this.thumbLPosition.Y + this.thumbSize.Height);
            SizeF downRTextSize = g.MeasureString(this.displayRightValue.Down, Defines.TinyBoldFont);
            PointF downRightPosition = new PointF(this.thumbRPosition.X + this.labelPadding + (int)this.thumbSize.Width, this.thumbRPosition.Y + (int)thumbSize.Height);
            using (SolidBrush brush = new SolidBrush(Defines.WildStawberryColor))
            {
                g.DrawString(this.displayLeftValue.Down, Defines.TinyBoldFont, brush, downLeftPosition);
                g.DrawString(this.displayRightValue.Down, Defines.TinyBoldFont, brush, downRightPosition);
            }
        }

        protected virtual void DrawLabels(Graphics g)
        {
            int padding = 20;
            int x = 0;
            int y = this.Height / 2;
            StringFormat sf = new StringFormat();
            sf.Alignment = StringAlignment.Near;
            sf.LineAlignment = StringAlignment.Near;

            g.DrawString(this.labelUp, this.DetermineFont(this.CurrentFontSize), new SolidBrush(Defines.CarrotColor), x, y - padding, sf);
            sf.LineAlignment = StringAlignment.Far;
            g.DrawString(this.labelDown, this.DetermineFont(this.CurrentFontSize), new SolidBrush(Defines.WildStawberryColor), x, y + padding, sf);
        }
        #endregion

        #region IResizeClient Implementation
        private int lastFontSizeChange = 0;
        public virtual void ApplyFontSize(int fontSizeChange)
        {
            if (this.lastFontSizeChange != fontSizeChange)
            {
                this.lastFontSizeChange = fontSizeChange;
                this.CurrentFontSize = FontSize.Tiny + fontSizeChange - 1;

                this.Invalidate();
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
                case FontSize.Big: return Defines.NormalBoldFont;
                case FontSize.Huge: return Defines.NormalBoldFont;
                default: return Defines.TinyBoldFont;
            }
        }

        public PokeDelegate InformResizer { get; set; }
        public bool SupportResizing { get; set; }
        #endregion
    }
}
