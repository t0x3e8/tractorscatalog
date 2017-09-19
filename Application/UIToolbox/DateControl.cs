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
    public partial class DateControl : UserControl, IResizableClient  
    {
        #region Fields
        protected readonly Size ClickSize = new Size(12, 12);
        protected readonly string[] Years = new string[26] { "1988", "1990", "1992", "1994", "1996", "1997", "1998", "1999", "2000", "2001", "2002", "2003", "2004", "2005", "2006", "2007", "2008", "2009", "2010", "2011", "2012", "2013", "2014", "2015", "2016", "2017" };
        protected readonly float LeftEndingWidth = 20f;
        protected readonly float RightEndingWidth = 20f;
        protected SizeF labelSize;                     // predefined year label size, having this as readonly with speed up a bit application
        //protected readonly Font labelFont = Defines.NormalFont;   // only one place with the font
        protected readonly float spaceBetweenBallAndLabel = 5;  // the space from a ball and a year
        protected readonly float cornerBarRadius = 3f;          // used for rounding the date control frame  
        protected readonly float ballWidth = 5f;                // width of each ball frame 
        protected readonly float ballHeight = 3f;               // height of each ball frame
        protected readonly float staticHeight = 50;             // instead of using this.Height you should stick to this otherwise the control will fall apart

        protected GraphicsPath dateControlPath = null;          // frame
        protected List<RectangleF> clickBoundaries;
        protected List<PointF> ballsPosition;
        protected List<PointF> labelesPosition;
        protected List<bool> values;                            // the values represents boolean for each ball
        protected IToggler allYearsToggler = null;              // the interface to support Toggler control

        // region selection regions
        protected Point pressedPointStart = Point.Empty;
        protected Point pressedPointEnd = Point.Empty;
        protected Rectangle selectedRegion = Rectangle.Empty;
        #endregion

        #region Properties
        /// <summary>
        /// Determines if all years are already selected.
        /// </summary>
        public virtual bool AllYearsSelected
        {
            get
            {
                bool result = true;
                for (int i = 0; i < values.Count; i++)
                {
                    if (values[i] == false)
                    {
                        result = false;
                        break;
                    }
                }

                return result;
            }
        }

        /// <summary>
        /// Determines if all years are already unselected.
        /// </summary>
        public virtual bool AllYearsUnselected
        {
            get
            {
                bool result = true;
                for (int i = 0; i < values.Count; i++)
                {
                    if (values[i])
                    {
                        result = false;
                        break;
                    }
                }

                return result;
            }
        }

        /// <summary>
        /// Get or set toggler control, this olso attach to SelectAll and DeselectAll events
        /// </summary>
        public virtual IToggler AllYearsToggler
        {
            get { return this.allYearsToggler; }
            set
            {
                this.allYearsToggler = value;

                this.allYearsToggler.SelectAll -= allYearsToggler_SelectAll;
                this.allYearsToggler.DeselectAll -= allYearsToggler_DeselectAll;

                this.allYearsToggler.SelectAll += allYearsToggler_SelectAll;
                this.allYearsToggler.DeselectAll += allYearsToggler_DeselectAll;
            }
        }
        #endregion

        #region Constructors
        public DateControl()
        {
            this.SetStyle(ControlStyles.UserPaint, true);
            this.SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            this.SetStyle(ControlStyles.DoubleBuffer, true);
            this.SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
            this.SetStyle(ControlStyles.SupportsTransparentBackColor, true);
            this.UpdateStyles();

            InitializeComponent();

            this.InitializeDefaultValues(this.Years.Length);

            this.CurrentFontSize = FontSize.Normal;
            this.MaximalExpectedFontSize = 5;
            this.SupportResizing = true;
        }

        private void InitializeDefaultValues(int valuesNumber)
        {
            this.values = new List<bool>(valuesNumber);
            for (int i = 0; i < valuesNumber; i++)
            {
                this.values.Add(false);
            }
        }
        #endregion

        #region Methods
        #region Toggler support methods
        protected void allYearsToggler_SelectAll(object sender, EventArgs e)
        {
            for (int i = 0; i < this.values.Count; i++)
            {
                if (this.values[i] == false)
                    this.values[i] = true;
            }

            // for sake of performance I didnt use ChangeItemState method
            this.InformToggler();
            this.Invalidate();
        }

        protected void allYearsToggler_DeselectAll(object sender, EventArgs e)
        {
            for (int i = 0; i < this.values.Count; i++)
            {
                if (this.values[i] == true)
                    this.values[i] = false;
            }

            // for sake of performance I didnt use ChangeItemState method
            this.InformToggler();
            this.Invalidate();
        }
        #endregion

        #region Drawing methods
        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
            e.Graphics.TextRenderingHint = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;

            Font font = this.DetermineFont(this.CurrentFontSize);   

            this.DrawControl(e.Graphics);
            this.DrawBalls(e.Graphics);
            this.DrawLabels(e.Graphics);
            this.DrawSelectionRectangle(e.Graphics);

#if Debug
            //this.DrawClickBoundaries(e.Graphics); // debug method
#endif
        }

        protected virtual void DrawControl(Graphics g)
        {
            if (this.dateControlPath != null)
            {
                using (Brush brush = new SolidBrush(Defines.MangoColor))
                {
                    g.FillPath(brush, this.dateControlPath);
                }

                using (Pen pen = new Pen(Defines.MilkColor, 1.5f))
                {
                    g.DrawPath(pen, this.dateControlPath);
                }
            }
        }

        protected virtual void DrawBalls(Graphics g)
        {
            for (int i = 0; i < this.values.Count; i++)
            {
                if (this.values[i])
                {
                    using (Image image = Resource1.ball_red8x8)
                    {
                        g.DrawImage(image, this.ballsPosition[i].X + 1, this.ballsPosition[i].Y + 1, 8, 9);
                    }
                }
            }
        }

        protected virtual void DrawLabels(Graphics g)
        {
            StringFormat sf = DrawingToolbox.CreateStringFormat(StringAlignment.Center, StringAlignment.Center);

            using (Brush brush = new SolidBrush(Defines.CarrotColor))
            {
                for (int i = 0; i < this.labelesPosition.Count; i++)
                {
                    RectangleF rect = new RectangleF(this.labelesPosition[i], labelSize);
                    g.DrawString(this.Years[i], this.DetermineFont(this.CurrentFontSize), brush, rect, sf);
                }
            }
        }

        protected virtual void DrawSelectionRectangle(Graphics g)
        {
            if (this.pressedPointStart != Point.Empty && this.pressedPointEnd != Point.Empty)
            {
                Rectangle rect = DrawingToolbox.BuildRectangleUponTwoPoints(this.pressedPointStart, this.pressedPointEnd);
                this.selectedRegion = DrawingToolbox.Trim(rect, this.Size);

                using (Pen pen = new Pen(Defines.CherryColor))
                {
                    g.DrawRectangle(pen, this.selectedRegion);
                }

                using (Brush brush = new SolidBrush(Defines.RadishColor))
                {
                    g.FillRectangle(brush, this.selectedRegion);
                }
            }
        }

#if DEBUG
        protected virtual void DrawClickBoundaries(Graphics g)
        {
            using (Pen pen = new Pen(Color.Blue))
            {
                g.DrawRectangles(pen, this.clickBoundaries.ToArray());
            }
        }
#endif
        #endregion

        protected override void OnSizeChanged(EventArgs e)
        {
            base.OnSizeChanged(e);

            this.CalculateBeforeDrawing();
        }

        protected virtual void CalculateBeforeDrawing()
        {
            float spaceBetweenBalls = (this.Width - this.Padding.Horizontal - this.LeftEndingWidth - this.RightEndingWidth) / this.Years.Length;
            float clickPadding = spaceBetweenBalls / 4;
            float height = staticHeight - this.Padding.Vertical;
            float x = this.Padding.Left;
            float y = this.Padding.Top;
            float xYearLabel, yYearLabel, newX = 0;
            bool isYearUp = false;

            this.labelSize = new SizeF(40, this.DetermineFont(this.CurrentFontSize).GetHeight());
            this.dateControlPath = new GraphicsPath();
            this.ballsPosition = new List<PointF>(this.Years.Length);
            this.labelesPosition = new List<PointF>(this.Years.Length);
            this.clickBoundaries = new List<RectangleF>(this.Years.Length);

            // bottom left side, from right to left
            dateControlPath.AddLine(x + this.LeftEndingWidth, y + height - ballHeight,
                         x + cornerBarRadius, y + height - ballHeight);
            dateControlPath.AddArc(x, y + height - (cornerBarRadius * 2) - ballHeight, cornerBarRadius * 2, cornerBarRadius * 2, 90, 90);

            // left, from bottom to top
            dateControlPath.AddLine(x, y + height - ballHeight - cornerBarRadius,
                         x, y + ballHeight + cornerBarRadius);
            dateControlPath.AddArc(x, y + ballHeight, cornerBarRadius * 2, cornerBarRadius * 2, 180, 90);

            // top left side, from left to right
            dateControlPath.AddLine(x + cornerBarRadius, y + ballHeight,
                         x + this.LeftEndingWidth, y + ballHeight);

            // top frame
            for (int i = 0; i < this.Years.Length; i++)
            {
                newX = x + (i * spaceBetweenBalls) + this.LeftEndingWidth;
                dateControlPath.AddArc(newX, y, ballWidth * 2, ballHeight * 2, 180, 180);

                // for sake of performance, we calculate everything once before drawing
                // this block calculates the ball position
                this.ballsPosition.Add(new PointF(newX, y));

                // this block calculates the labels positions
                xYearLabel = this.ballsPosition[i].X + (this.ballWidth / 2) - (this.labelSize.Width / 2);
                yYearLabel = (isYearUp) ? yYearLabel = this.ballsPosition[i].Y + height + this.spaceBetweenBallAndLabel :
                                      yYearLabel = this.ballsPosition[i].Y - this.labelSize.Height - this.spaceBetweenBallAndLabel;
                isYearUp = !isYearUp;

                this.labelesPosition.Add(new PointF(xYearLabel, yYearLabel));

                // this block calculates the click boundaries for the ball
                this.clickBoundaries.Add(new RectangleF(this.ballsPosition[i].X - clickPadding, this.ballsPosition[i].Y - clickPadding,
                                                        (ballWidth * 2) + (clickPadding * 2), height + (clickPadding * 2)));
            }

            // this is needed to shift the carriage at the end of the just drawn ball
            newX += (ballWidth * 2);

            // top right side, from left to right
            dateControlPath.AddLine(newX, y + ballHeight,
                         newX + this.RightEndingWidth - cornerBarRadius, y + ballHeight);
            dateControlPath.AddArc(newX + this.RightEndingWidth - (cornerBarRadius * 2), y + ballHeight, cornerBarRadius * 2, cornerBarRadius * 2, 270, 90);

            // right, from top to bottom
            dateControlPath.AddLine(newX + this.RightEndingWidth, y + ballHeight + cornerBarRadius,
                 newX + this.RightEndingWidth, y + height - ballHeight - cornerBarRadius);
            dateControlPath.AddArc(newX + this.RightEndingWidth - (cornerBarRadius * 2), y + height - ballHeight - (cornerBarRadius * 2), cornerBarRadius * 2, cornerBarRadius * 2, 0, 90);

            //bottom right side, from right to left
            dateControlPath.AddLine(newX + this.RightEndingWidth - cornerBarRadius, y + height - ballHeight,
                 newX, y + height - ballHeight);

            // bottom frame
            for (int i = 0; i < this.Years.Length; i++)
            {
                x = newX - (i * spaceBetweenBalls) - (ballWidth * 2);
                dateControlPath.AddArc(x, y + height - (ballHeight * 2), ballWidth * 2, ballHeight * 2, 00, 180);
            }
        }

        protected virtual int GetClickedValue(Point point)
        {
            for (int i = 0; i < this.clickBoundaries.Count; i++)
            {
                RectangleF rect = this.clickBoundaries[i];
                if (rect.Contains(point))
                    return i;
            }

            return -1;
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            base.OnMouseDown(e);

            int clickedValue = this.GetClickedValue(e.Location);
            if (clickedValue != -1)
            {
                this.ChangeItemState(clickedValue, !this.values[clickedValue]);
                this.Invalidate();
            }
            else
            {
                this.pressedPointStart = e.Location;
            }
        }

        protected override void OnMouseUp(MouseEventArgs e)
        {
            base.OnMouseUp(e);

            if (this.pressedPointStart != Point.Empty && this.selectedRegion != Rectangle.Empty)
            {
                this.SelectUnselect(this.selectedRegion);

                this.selectedRegion = Rectangle.Empty;
                this.pressedPointStart = Point.Empty;
                this.Invalidate();
            }
        }

        protected virtual void SelectUnselect(Rectangle selectionRectangle)
        {
            List<int> itemsToChange = new List<int>();
            bool newValue = true;

            for (int i = 0; i < this.clickBoundaries.Count; i++)
            {
                if (RectangleF.Intersect(selectionRectangle, this.clickBoundaries[i]) != Rectangle.Empty)
                {
                    itemsToChange.Add(i);
                    if (newValue == true && this.values[i])
                        newValue = false;
                }
            }

            for (int i = 0; i < itemsToChange.Count; i++)
            {
                this.ChangeItemState(itemsToChange[i], newValue);
            }
        }

        protected void ChangeItemState(int itemIndex, bool newValue)
        {
            this.values[itemIndex] = newValue;
            this.InformToggler();
        }

        protected void InformToggler()
        {
            if (this.AllYearsToggler != null)
            {
                if (this.AllYearsSelected)
                    this.AllYearsToggler.State = true;
                else
                    this.AllYearsToggler.State = false;
            }
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);
            if (this.pressedPointStart != Point.Empty)
            {
                this.pressedPointEnd = e.Location;
                this.Invalidate();
            }
        }

        public IList<string> GetSelectedYears()
        {
            IList<string> selectedYears = new List<string>();

            for (int i = 0; i < values.Count; i++)
            {
                if (values[i] == true)
                    selectedYears.Add(Years[i]);
            }
            return selectedYears;
        }
        #endregion

        #region IResizer implementation
        int lastFontSizeChange = 0;
        public void ApplyFontSize(int fontSizeChange)
        {
            if (this.lastFontSizeChange != fontSizeChange)
            {
                lastFontSizeChange = fontSizeChange;
                this.CurrentFontSize = FontSize.Tiny + fontSizeChange - 1;

                this.CalculateBeforeDrawing();
                this.Invalidate();
            }
        }

        public Font DetermineFont(FontSize fontSize)
        {
            switch (fontSize)
            {
                case FontSize.Tiny: return Defines.TinyFont;
                case FontSize.Small: return Defines.SmallFont;
                case FontSize.Normal: return Defines.NormalFont;
                case FontSize.Big: return Defines.BigFont;
                case FontSize.Huge: return Defines.HugeFont;
                default: return Defines.NormalFont;
            }
        }

        private int maximalExpectedFontSize;
        public FontSize CurrentFontSize { get; protected set; }
        public int MaximalExpectedFontSize
        {
            get { return this.maximalExpectedFontSize; }
            set
            {
                if (this.maximalExpectedFontSize != value)
                {
                    this.maximalExpectedFontSize = value;
                    if (this.InformResizer != null)
                        this.InformResizer();
                }
            }
        }
        public PokeDelegate InformResizer { get; set; }
        public bool SupportResizing { get; set; }
        #endregion
    }
}
