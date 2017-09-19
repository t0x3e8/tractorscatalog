using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;

namespace Enceladus.UIToolbox
{
    public partial class SingleScroller : ScrollerBase
    {
        #region Fields and Properties
        protected Point actualPosition = Point.Empty;
        protected bool isSelected = false;

        protected int value = 10;
        [DefaultValue(10)]
        public virtual int Value
        {
            get { return this.value; }
            set
            {
                if (value > 0)
                    this.value = value;
                else
                    this.value = 0;
                this.Invalidate();
            }
        }

        protected Position horizont = Position.None;
        [Browsable(true)]
        public virtual Position Horizont
        {
            get { return this.horizont; }
            set
            {
                this.horizont = value;
                this.Invalidate();
            }
        }

        protected Position vertical = Position.None;
        [Browsable(true)]
        public virtual Position Vertical
        {
            get { return this.vertical; }
            set
            {
                this.vertical = value;
                this.Invalidate();
            }
        }

        protected RedInputBox inputBox;
        public virtual RedInputBox InputBox
        {
            get { return this.inputBox; }
            set
            {
                this.inputBox = value;
                this.CalculateInputBoxPosition();
                if (this.inputBox != null)
                {
                    this.inputBox.MinimumValue = this.minValue;
                    this.inputBox.MaximumValue = this.maxValue;
                    this.inputBox.TextChanged += new EventHandler(inputBox_TextChanged);
                    this.inputBox.Disposed += new EventHandler(inputBox_Disposed);
                }
            }
        }

        protected ControlLabel label;
        public virtual ControlLabel Label
        {
            get { return this.label; }
            set
            {
                this.label = value;
                this.CalculateLabelPosition();
            }
        }

        public override int MinimalValue
        {
            set
            {
                base.MinimalValue = value;
                if (this.inputBox != null)
                    this.inputBox.MinimumValue = value;
            }
        }

        public override int MaximalValue
        {
            set
            {
                base.MaximalValue = value;
                if (this.inputBox != null)
                    this.inputBox.MaximumValue = value;
            }
        }
        #endregion

        #region Constructors
        public SingleScroller()
            : base()
        {
            InitializeComponent();

            this.Value = this.maxValue;
        }
        #endregion

        #region Methods
        protected override void OnPaint(PaintEventArgs pevent)
        {
            base.OnPaint(pevent);

            this.actualPosition = this.Calculate(this.Value);

            if (this.Enabled)
                this.DrawProgress(pevent.Graphics);

            this.DrawThumb(pevent.Graphics);

            if (this.inputBox == null)
                this.DrawHoveringLabel(pevent.Graphics, this.Enabled);
            else
                this.inputBox.Text = this.Enabled ? this.value.ToString() : string.Empty;
            //this.Debug(pevent.Graphics);
        }

        protected virtual void DrawProgress(Graphics g)
        {
            Rectangle rect = new Rectangle(this.Padding.Left, this.Padding.Top, this.actualPosition.X - this.Padding.Left + 1, this.Height - this.Padding.Vertical);
            GraphicsPath path = this.CalculatePath(rect);

            using (SolidBrush brush = new SolidBrush(Defines.WildStawberryColor))
            {
                g.FillPath(brush, path);
            }
        }

        protected virtual void Debug(Graphics g)
        {
            Rectangle rect = new Rectangle(0, 0, this.Width - 1, this.Height - 1);
            Rectangle rectPad = new Rectangle(this.Padding.Left, this.Padding.Top, this.Width - this.Padding.Horizontal, this.Height - this.Padding.Vertical);
            Rectangle rectBar = new Rectangle(Padding.Left, Padding.Top, BarSize.Width, BarSize.Height);
            g.DrawRectangle(Pens.Yellow, rect);
            g.DrawRectangle(Pens.Green, rectPad);
            g.DrawRectangle(Pens.Blue, rectBar);
        }

        protected virtual void DrawHoveringLabel(Graphics g, bool isEnabled)
        {
            if (isEnabled && this.horizont != Position.None && this.vertical != Position.None)
            {
                PointF labelPosition = new PointF();

                string valueText = ((int)this.value).ToString();
                SizeF textSize = g.MeasureString(valueText, Defines.TinyBoldFont);

                if (this.horizont == Position.Left)
                    labelPosition.X = this.actualPosition.X - this.labelPadding - (int)textSize.Width;
                else if (this.horizont == Position.Right)
                    labelPosition.X = this.actualPosition.X + this.thumbSize.Width + this.labelPadding;

                if (this.vertical == Position.Up)
                    labelPosition.Y = this.actualPosition.Y - (int)textSize.Height;
                else if (this.vertical == Position.Bottom)
                    labelPosition.Y = this.actualPosition.Y + this.thumbSize.Height;

                using (SolidBrush brush = new SolidBrush(Defines.CarrotColor))
                {
                    g.DrawString(valueText, Defines.TinyBoldFont, brush, labelPosition);
                }
            }
        }

        protected override void OnMouseUp(MouseEventArgs e)
        {
            base.OnMouseUp(e);

            if (this.isSelected)
                this.isSelected = false;
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            base.OnMouseDown(e);
            this.isSelected = this.IsThumbClicked(e.Location);
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);

            if (this.isSelected)
            {
                this.Value = this.Calculate(e.Location);
                this.Invalidate();
            }
        }

        protected virtual bool IsThumbClicked(Point mousePosition)
        {
            Rectangle rect = new Rectangle(
                this.actualPosition.X - this.clickPadding,
                this.actualPosition.Y - this.clickPadding,
                this.thumbSize.Width + (2 * this.clickPadding),
                this.thumbSize.Height + (2 * this.clickPadding));

            return rect.Contains(mousePosition);
        }

        protected virtual void DrawThumb(Graphics g)
        {
            Image image = this.Enabled ? Resource1.Thumb : DrawingToolbox.SetImageOpacity(Resource1.Thumb, 0.5f);
            g.DrawImage(image, this.actualPosition.X, this.actualPosition.Y, this.thumbSize.Width, this.thumbSize.Height);
        }

        protected virtual Point Calculate(float value)
        {
            if (value < this.minValue)
                value = this.minValue;
            else if (value > this.maxValue)
                value = this.maxValue;

            float scale = this.maxValue - this.minValue;
            double ratio = this.BarSize.Width / scale;
            double x = value * ratio;
            x += this.Padding.Left;

            int y = (this.Padding.Top + base.BarSize.Height / 2) - (this.thumbSize.Height / 2);

            return new Point((int)Math.Round(x, 0), y);
        }

        protected virtual int Calculate(Point point)
        {
            int position = point.X - this.Padding.Left;
            float scale = this.maxValue - this.minValue;
            double ratio = scale / this.BarSize.Width;
            double value = position * ratio;

            if (value < this.minValue)
                value = this.minValue;
            else if (value > this.maxValue)
                value = this.maxValue;

            if (value != maxValue)
                value -= value % this.Step;

            return (int)Math.Round(value, 0);
        }

        protected override void OnMove(EventArgs e)
        {
            base.OnMove(e);
            this.CalculateInputBoxPosition();
            this.CalculateLabelPosition();
        }

        protected override void OnSizeChanged(EventArgs e)
        {
            base.OnSizeChanged(e);
            this.CalculateInputBoxPosition();
            this.CalculateLabelPosition();
        }

        protected override void OnCreateControl()
        {
            base.OnCreateControl();
            this.CalculateInputBoxPosition();
            this.CalculateLabelPosition();
        }

        void inputBox_Disposed(object sender, EventArgs e)
        {
            this.inputBox.TextChanged -= this.inputBox_TextChanged;
        }

        void inputBox_TextChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(this.inputBox.Text))
                this.Value = int.Parse(this.inputBox.Text);
        }

        protected virtual void CalculateLabelPosition()
        {
            if (label != null)
            {
                this.label.Location = new Point(this.Location.X - this.label.Width,
                    (this.Location.Y + (this.Height / 2)) - (this.label.Height / 2));
            }
        }

        protected virtual void CalculateInputBoxPosition()
        {
            if (this.inputBox != null)
            {
                this.inputBox.Location = new Point(this.Location.X + this.Size.Width,
                    (this.Location.Y + (this.Height / 2)) - (this.inputBox.Height / 2));
            }
        }

        protected override void OnEnabledChanged(EventArgs e)
        {
            base.OnEnabledChanged(e);
            this.InputBox.Enabled = this.Enabled;
            this.Invalidate();
        }
        #endregion
    }
}
