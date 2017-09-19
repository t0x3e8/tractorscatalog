using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Diagnostics;
using Enceladus.Api.UI;

namespace Enceladus.UIToolbox
{
    public class BorderedPanel : Panel, IResizableClient
    {
        #region Fields and Properties
        protected Color borderColor = Defines.CherryColor;
        public virtual Color BorderColor
        {
            get { return this.borderColor; }
            set
            {
                this.borderColor = value;
                this.Invalidate();
            }
        }

        protected float borderWidth = 1f;
        public virtual float BorderWidth
        {
            get { return this.borderWidth; }
            set
            {
                this.borderWidth = value;
                this.Invalidate();
            }
        }

        protected bool isHovered = false;
        protected System.Timers.Timer timer;
        private const int ALHA_SPEED = 10;
        protected int alpha;

        protected string caption = null;
        public virtual string Caption
        {
            get { return this.caption; }
            set
            {
                this.caption = value;
                this.Invalidate();
            }
        }

        protected bool showBorder = false;
        public virtual bool ShowBorder
        {
            get { return this.showBorder; }
            set { this.showBorder = value; }
        }

        protected bool autoChildresArrange = false;
        public bool AutoChildrenArrange
        {
            get { return this.autoChildresArrange; }
            set
            {
                if (this.autoChildresArrange != value)
                {
                    this.autoChildresArrange = value;
                    this.ArrangeChildres();
                }
            }
        }

        private Size lastSize = Size.Empty;
        #endregion

        #region Constructors
        public BorderedPanel()
        {
            this.SetStyle(ControlStyles.UserPaint, true);
            this.SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            this.SetStyle(ControlStyles.DoubleBuffer, true);
            this.SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
            this.SetStyle(ControlStyles.SupportsTransparentBackColor, true);
            this.BackColor = Color.Transparent;

            this.UpdateStyles();

            this.timer = new System.Timers.Timer();
            this.timer.Elapsed += new System.Timers.ElapsedEventHandler(timer_Elapsed);
            this.timer.Interval = 30;
            this.timer.AutoReset = true;

            this.Margin = new Padding(4, 0, 0, 0);
            this.MaximalExpectedFontSize = 5;
            this.AutoScroll = false;
            this.SupportResizing = true;
        }

        void timer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            if (this.isHovered)
                this.alpha += ALHA_SPEED;
            else
                this.alpha -= ALHA_SPEED;


            if (this.alpha > 100)
            {
                this.alpha = 100;
                this.timer.Stop();
            }
            if (this.alpha < 0)
            {
                this.alpha = 0;
                this.timer.Stop();
            }

            this.Invalidate();
        }
        #endregion

        #region Methods
        protected override void OnPaint(PaintEventArgs e)
        {
            //base.OnPaint(e);
            e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            e.Graphics.TextRenderingHint = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;

            if (this.showBorder)
                this.DrawBorder(e.Graphics);

            if (!string.IsNullOrEmpty(this.caption))
                this.DrawCaption(e.Graphics);

            //e.Graphics.DrawString(this.Height.ToString(), this.Font, Brushes.Red, new PointF(150, 0)); 
        }

        protected virtual void DrawBorder(System.Drawing.Graphics g)
        {
            using (Pen pen = new Pen(Color.FromArgb((int)(this.alpha), this.borderColor), this.BorderWidth))
            {
                Rectangle rect = new Rectangle(this.Margin.Left, this.Margin.Top, this.Width - this.Margin.Horizontal, this.Height - this.Margin.Vertical);
                GraphicsPath roundedRectPath = DrawingToolbox.GetRoundedRectanglePath(this.PrepareRect(rect), 5);
                g.DrawPath(pen, roundedRectPath);
            }
        }

        protected virtual void DrawCaption(System.Drawing.Graphics g)
        {
            using (Brush brush = new SolidBrush(Defines.CabbageColor))
            {
                g.DrawString(this.caption, this.DetermineFont(this.CurrentFontSize), brush, new PointF(this.Margin.Left, this.Margin.Top));
            }
        }

        protected virtual Rectangle PrepareRect(Rectangle rect)
        {
            int padding = 1;
            Rectangle newRect = new Rectangle(rect.Left + padding, rect.Top + padding, rect.Width - 2 * padding, rect.Height - 2 * padding);

            return newRect;
        }

        protected override void OnMouseEnter(EventArgs e)
        {
            base.OnMouseEnter(e);
            if (this.showBorder && !this.isHovered)
            {
                this.isHovered = true;
                this.timer.Start();
            }
        }

        protected override void OnMouseLeave(EventArgs e)
        {
            base.OnMouseLeave(e);

            if (this.showBorder && !this.Bounds.Contains(this.Parent.PointToClient(Control.MousePosition)))
            {
                this.isHovered = false;
                this.timer.Start();
            }
        }

        protected override void OnSizeChanged(EventArgs e)
        {
            base.OnSizeChanged(e);

            if (this.lastSize == Size.Empty)
                this.lastSize = this.Size;

            if (this.AutoChildrenArrange)
                this.ArrangeChildres();
        }

        protected virtual void ArrangeChildres()
        {
            List<ILocationDepended> controlsToArrange = this.PrepareControls();
            controlsToArrange.Sort(new LocationDependedComparer());

            Point startLocation = new Point(0, 19);
            int freeWidthToUse = this.Size.Width - this.lastSize.Width;
            foreach (ILocationDepended controlToArrange in controlsToArrange)
            {
                controlToArrange.Location = startLocation;
                freeWidthToUse = controlToArrange.UpdateSize(freeWidthToUse);

                if (controlToArrange.LastInRow)
                {
                    freeWidthToUse = this.Size.Width - this.lastSize.Width;
                    startLocation = new Point(0, startLocation.Y + controlToArrange.Size.Height + 2);
                }
                else
                    startLocation = new Point(startLocation.X + controlToArrange.Size.Width, startLocation.Y);
            }
        }

        protected List<ILocationDepended> PrepareControls()
        {
            List<ILocationDepended> locationDependedControlCollection = new List<ILocationDepended>();

            foreach (var control in this.Controls)
            {
                if (control is ILocationDepended)
                {
                    ILocationDepended locationDependedControl = (ILocationDepended)control;

                    if (locationDependedControl != null)
                        locationDependedControlCollection.Add(locationDependedControl);
                }
            }

            return locationDependedControlCollection;
        }

        public new void ResumeLayout(bool performLayout)
        {
            base.ResumeLayout(performLayout);

            try
            {
                if (this.AutoChildrenArrange)
                    this.ArrangeChildres();
            }
            catch { }

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

                this.Invalidate();
            }
        }

        public virtual FontSize CurrentFontSize { get; protected set; }

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

        public virtual Font DetermineFont(FontSize fontSize)
        {
            switch (fontSize)
            {
                case FontSize.Tiny: return Defines.NormalItalicFont;
                case FontSize.Small: return Defines.BigItalicFont;
                case FontSize.Normal: return Defines.HugeItalicFont;
                case FontSize.Big: return Defines.HugeItalicFont;
                case FontSize.Huge: return Defines.HugeItalicFont;
                default: return Defines.NormalItalicFont;
            }
        }

        public virtual PokeDelegate InformResizer { get; set; }

        public bool SupportResizing { get; set; }
        #endregion
    }

    internal class LocationDependedComparer : IComparer<ILocationDepended>
    {
        public int Compare(ILocationDepended x, ILocationDepended y)
        {
            return x.Order.CompareTo(y.Order);
        }
    }
}
