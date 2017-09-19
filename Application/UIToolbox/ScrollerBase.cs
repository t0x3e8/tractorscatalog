using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Drawing.Drawing2D;

namespace Enceladus.UIToolbox
{
    public enum Position
    {
        None, Up, Bottom, Left, Right
    }

    public abstract partial class ScrollerBase : UserControl
    {
        #region Fields and Properties
        protected int clickPadding = 5;
        protected int labelPadding = 3;

        public virtual Size BarSize
        {
            get
            {
                Size size = new Size();
                size.Width = this.Width - this.Padding.Vertical - this.ThumbSize.Width;
                size.Height = this.Height - this.Padding.Top - this.Padding.Bottom;
                return size;
            }
        }

        protected int minValue = 0;
        [DefaultValue(1)]
        public virtual int MinimalValue
        {
            get { return this.minValue; }
            set 
            {
                if (value >= 0)
                    this.minValue = value;
                else
                    this.minValue = 0;

                this.Invalidate();
            }
        }

        protected int maxValue = 100;
        [DefaultValue(100)]
        public virtual int MaximalValue
        {
            get { return this.maxValue; }
            set { this.maxValue = value; }
        }
        
        protected Size thumbSize = new Size(12, 20);
        public virtual Size ThumbSize
        {
            get { return this.thumbSize; }
            set { this.thumbSize = value; }
        }
        
        public virtual int Step { get; set; }
        #endregion
        
        #region Constructors
        public ScrollerBase()
        {
            InitializeComponent();
            this.SetStyle(ControlStyles.UserPaint, true);
            this.SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            this.SetStyle(ControlStyles.DoubleBuffer, true);
            this.SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
            this.SetStyle(ControlStyles.SupportsTransparentBackColor, true);
            this.SetStyle(ControlStyles.Selectable, false);
            this.UpdateStyles();

            this.Padding = new Padding(10, 15, 10, 15);
            this.Step = 10;
        }
        #endregion

        #region Methods
        protected override void OnPaint(PaintEventArgs pevent)
        {
            pevent.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            pevent.Graphics.TextRenderingHint = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;

            GraphicsPath path = this.CalculatePath();
            this.FillScrollerBack(pevent.Graphics, path);
            this.DrawBorder(pevent.Graphics, path);
        }

        protected virtual void DrawBorder(Graphics g, GraphicsPath path)
        {
            using (Pen pen = new Pen(Defines.MilkColor))
            {
                g.DrawPath(pen, path);
            }
        }

        protected virtual void FillScrollerBack(Graphics g, GraphicsPath path)
        {
            using (SolidBrush brush = new SolidBrush(Defines.MangoColor))
            {
                g.FillPath(brush, path);
            }
        }

        protected virtual GraphicsPath CalculatePath()
        {
            Rectangle rect = new Rectangle(Padding.Left, Padding.Top, this.Width - this.Padding.Horizontal - 1, this.Height - this.Padding.Vertical);
            return this.CalculatePath(rect);
        }

        protected virtual GraphicsPath CalculatePath(Rectangle rect)
        {
            return DrawingToolbox.GetRoundedRectanglePath(rect, 2);
        }

        #endregion
    }
}
