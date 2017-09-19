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
    public partial class SingleRecord : UserControl, IResizableClient, ILocationDepended
    {
        #region Fields and Properties
        protected int maxLabelWidth;
        public virtual int LabelMaxWidth
        {
            get { return this.maxLabelWidth; }
            set
            {
                if (this.maxLabelWidth != value)
                {
                    this.maxLabelWidth = value;

                    this.UpdateSize(this.lastFreeWidthToUse);
                    this.Invalidate();
                }
            }
        }

        protected int maxValueWidth;
        public virtual int ValueMaxWidth
        {
            get { return this.maxValueWidth; }
            set
            {
                if (this.maxValueWidth != value)
                {
                    this.maxValueWidth = value;

                    this.UpdateSize(this.lastFreeWidthToUse);
                    this.Invalidate();
                }
            }
        }

        protected int maxUnitWidth;
        public virtual int UnitMaxWidth
        {
            get { return this.maxUnitWidth; }
            set
            {
                if (this.maxUnitWidth != value)
                {
                    this.maxUnitWidth = value;

                    this.UpdateSize(this.lastFreeWidthToUse);
                    this.Invalidate();
                }
            }
        }
        
        protected int minUnitWidth;
        public virtual int UnitMinWidth
        {
            get { return this.minUnitWidth; }
            set
            {
                if (this.minUnitWidth != value)
                {
                    this.minUnitWidth = value;

                    this.UpdateSize(this.lastFreeWidthToUse);
                    this.Invalidate();
                }
            }
        }

        protected int minValueWidth;
        public virtual int ValueMinWidth
        {
            get { return this.minValueWidth; }
            set
            {
                if (this.minValueWidth != value)
                {
                    this.minValueWidth = value;

                    this.UpdateSize(this.lastFreeWidthToUse);
                    this.Invalidate();
                }
            }
        }

        protected int minLabelWidth;
        public virtual int LabelMinWidth
        {
            get { return this.minLabelWidth; }
            set
            {
                if (this.minLabelWidth != value)
                {
                    this.minLabelWidth = value;

                    this.UpdateSize(this.lastFreeWidthToUse);
                    this.Invalidate();
                }
            }
        }
        
        protected int whiteSpace = 2;
        public virtual int WhiteSpace
        {
            get { return this.whiteSpace; }
            set
            {
                if (whiteSpace != value)
                {
                    this.whiteSpace = value;

                    this.UpdateSize(this.lastFreeWidthToUse);
                    this.Invalidate();
                }
            }

        }


        protected string label = string.Empty;
        public virtual string Label
        {
            get { return this.label; }
            set
            {
                if (this.label != value)
                {
                    this.label = value;
                    this.Invalidate();
                }
            }
        }

        protected string value = string.Empty;
        public virtual string Value
        {
            get { return this.value; }
            set
            {
                if (this.value != value)
                {
                    this.value = value;
                    this.Invalidate();
                }
            }
        }

        protected Units unit = Units.None;
        public virtual Units Unit
        {
            get { return this.unit; }
            set
            {
                if (this.unit != value)
                {
                    this.unit = value;
                    this.Invalidate();
                }
            }
        }
        
        protected StringAlignment valueSide = StringAlignment.Far;
        public virtual StringAlignment ValueSide
        {
            get { return this.valueSide; }
            set
            {
                if (this.valueSide != value)
                {
                    this.valueSide = value;
                    this.Invalidate();
                }
            }
        }

        protected int lastFontSize = 0;
        protected int lastFreeWidthToUse = 0;
        public Rectangle LabelRectangle { get; protected set; }
        public Rectangle ValueRectagle { get; protected set; }
        public Rectangle UnitRectangle { get; protected set; }

        public bool LastInRow { get; set; }
        public int Order { get; set; }

#if DEBUG
        public Rectangle TotalMaximalSize
        {
            get { return new Rectangle(0, 0, this.LabelMaxWidth + this.WhiteSpace + this.ValueMaxWidth + this.WhiteSpace + this.UnitMaxWidth, 17); }
        }

        public Rectangle TotalMinimalSize
        {
            get { return new Rectangle(0, 0, this.LabelMinWidth + this.WhiteSpace + this.ValueMinWidth + this.WhiteSpace + this.UnitMinWidth, 17); }
        }
#endif
        #endregion

        #region Constructors
        public SingleRecord()
        {
            InitializeComponent();

            this.SetStyle(ControlStyles.UserPaint, true);
            this.SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            this.SetStyle(ControlStyles.DoubleBuffer, true);
            this.SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
            this.SetStyle(ControlStyles.SupportsTransparentBackColor, true);
            this.UpdateStyles();

            this.Height = 17;
            this.CurrentFontSize = FontSize.Tiny;

            this.Order = 999;
            this.SupportResizing = true;
        }
        #endregion

        #region Methods
        protected override void OnPaint(PaintEventArgs e)
        {
            e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            e.Graphics.TextRenderingHint = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;

            //this.DrawDesignMode(e.Graphics);

            this.DrawLabel(e.Graphics);
            this.DrawValue(e.Graphics);
            this.DrawUnits(e.Graphics);
        }

        private void DrawUnits(Graphics g)
        {
            if (this.unit != Units.None)
            {
                StringFormat sf = DrawingToolbox.CreateStringFormat(StringAlignment.Center, StringAlignment.Near);
                using (Brush brush = new SolidBrush(Defines.CarrotColor))
                {
                    g.DrawString(StringManager.GetUnit(this.Unit), this.DetermineValueFontSize(this.CurrentFontSize), brush, this.UnitRectangle, sf);
                }
            }
        }

        private void DrawValue(Graphics g)
        {
            StringFormat sf = DrawingToolbox.CreateStringFormat(StringAlignment.Near, this.ValueSide);

            using (Brush brush = new SolidBrush(Defines.ParsnipColor))
                g.FillRectangle(brush, this.ValueRectagle);

            using (Pen pen = new Pen(Defines.LeekColor))
                g.DrawRectangle(pen, this.ValueRectagle);

            using (Brush brush = new SolidBrush(Defines.CarrotColor))
                g.DrawString(this.Value, this.DetermineValueFontSize(this.CurrentFontSize), brush, this.ValueRectagle, sf);
        }

        private void DrawLabel(Graphics g)
        {
            StringFormat sf = DrawingToolbox.CreateStringFormat(StringAlignment.Center, StringAlignment.Far);
            using (Brush brush = new SolidBrush(Defines.CarrotColor))
            {
                g.DrawString(this.Label, this.DetermineLabelFontSize(this.CurrentFontSize), brush, this.LabelRectangle, sf);
            }
        }

        private void DrawDesignMode(Graphics g)
        {
            // Units
            this.DrawRectangle(g, Color.Red, this.UnitRectangle);

            // Value
            this.DrawRectangle(g, Color.Yellow, this.ValueRectagle);

            // Label
            this.DrawRectangle(g, Color.Green, this.LabelRectangle);
        }

        private void DrawRectangle(Graphics g, Color penColor, Rectangle rect)
        {
            using (Pen pen = new Pen(penColor))
            {
                g.DrawRectangle(pen, rect);
            }
        }

        public int UpdateSize(int freeWidthToUse)
        {
            this.lastFreeWidthToUse = freeWidthToUse;
            int height = this.Height;

            this.LabelRectangle = this.CalculateLabelRectangle(ref freeWidthToUse, height);
            this.ValueRectagle = this.CalculateValueRectangle(ref freeWidthToUse, height);
            this.UnitRectangle = this.CalculateUnitRectangle(ref freeWidthToUse, height);

            this.Size = new Size(this.LabelRectangle.Width + this.WhiteSpace + this.ValueRectagle.Width + this.WhiteSpace + this.UnitRectangle.Width, height);
            return freeWidthToUse;
        }

        private Rectangle CalculateLabelRectangle(ref int freeWidthToUse, int height)
        {
            int width = this.LabelMinWidth + freeWidthToUse;

            if (width > this.LabelMaxWidth && this.LabelMaxWidth != 0)
            {
                freeWidthToUse = width - this.LabelMaxWidth;
                width = this.LabelMaxWidth;
            }
            else
            {
                freeWidthToUse = 0;
            }

            return new Rectangle(0, 0, width, height);
        }

        private Rectangle CalculateValueRectangle(ref int freeWidthToUse, int height)
        {
            int width = this.ValueMinWidth + freeWidthToUse;

            if (width > this.ValueMaxWidth && this.LabelMaxWidth != 0)
            {
                freeWidthToUse = width - this.ValueMaxWidth;
                width = this.ValueMaxWidth;
            }
            else
            {
                freeWidthToUse = 0;
            }

            return new Rectangle(this.LabelRectangle.Width + this.WhiteSpace, 0, width, height - 1);
        }

        private Rectangle CalculateUnitRectangle(ref int freeWidthToUse, int height)
        {
            int width = this.UnitMinWidth + freeWidthToUse;

            if (this.UnitMinWidth == 0 && this.UnitMaxWidth == 0)
                width = 0;
            else if (width > this.UnitMaxWidth && this.UnitMaxWidth != 0)
            {
                freeWidthToUse = width - this.UnitMaxWidth;
                width = this.UnitMaxWidth;
            }
            else
            {
                freeWidthToUse = 0;
                width = this.UnitMinWidth;
            }

            return new Rectangle(this.LabelRectangle.Width + this.WhiteSpace + this.ValueRectagle.Width + this.WhiteSpace, 0, width, height);
        }
        
        #region IClientResize implementation
        public void ApplyFontSize(int fontSize)
        {
            if (fontSize != lastFontSize)
            {
                lastFontSize = fontSize;
                this.CurrentFontSize = FontSize.Tiny + fontSize;
                this.Invalidate();
            }
        }

        [Browsable(false)]
        public FontSize CurrentFontSize { get; protected set; }

        [Browsable(false)]
        public int MaximalExpectedFontSize { get; set; }

        [Browsable(false)]
        public PokeDelegate InformResizer { get; set; }
        public bool SupportResizing { get; set; }

        private System.Drawing.Font DetermineLabelFontSize(FontSize fontSize)
        {
            switch (fontSize)
            {
                case FontSize.Tiny: return Defines.TinyBoldFont;
                case FontSize.Small: return Defines.SmallBoldFont;
                case FontSize.Normal:
                case FontSize.Big:
                case FontSize.Huge:
                default:
                    return Defines.NormalBoldFont;
            }
        }

        private System.Drawing.Font DetermineValueFontSize(FontSize fontSize)
        {
            switch (fontSize)
            {
                default:
                case FontSize.Tiny: return Defines.TinyBoldFont;
                case FontSize.Small: return Defines.SmallBoldFont;
                case FontSize.Normal:
                case FontSize.Big:
                case FontSize.Huge:
                    return Defines.NormalBoldFont;
            }
        }
        #endregion
        #endregion
    }
}
