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
    public partial class Tab : Control
    {
        #region Fields and Properties
        private bool isHover;

        private string content;
        public string Content
        {
            get { return this.content; }
            set { this.content = value; }
        }

        private Image image;
        public Image Image
        {
            get { return this.image; }
            private set
            {
                this.image = value;
                if (!this.isOwnSize)
                this.Width = this.image.Width;
                this.Height = this.image.Height;
            }
        }

        public TabType TabType { get; set; }
        bool isOwnSize = false;
        public int TabWidth
        {
            get { return this.Width; }
            set
            {
                this.isOwnSize = true;
                this.Width = value;
                this.Invalidate();
            }
        }

        public TabSize TabSize { get; set; }
        #endregion

        #region Constructors
        public Tab()
        {
            this.InitializeComponent();

            this.TabType = UIToolbox.TabType.Inactive;
            this.TabSize = UIToolbox.TabSize.Big;
            this.UpdateTab();
            this.content = "Dummy";

            this.SetStyle(ControlStyles.UserPaint, true);
            this.SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            this.SetStyle(ControlStyles.DoubleBuffer, true);
            this.SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
            this.SetStyle(ControlStyles.SupportsTransparentBackColor, true);
            this.UpdateStyles();

            this.BackColor = Color.Transparent;
        }

        public Tab(TabType type)
            :this()
        {
            this.TabType = type;
            this.UpdateTab();
        }
        #endregion

        #region Methods
        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            
            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
            e.Graphics.TextRenderingHint = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;

            this.DrawTab(e.Graphics);
            this.DrawContentText(e.Graphics);
            //this.DrawRegionBorder(e.Graphics);
        }

        protected override void OnMouseEnter(EventArgs e)
        {
            base.OnMouseEnter(e);
            this.isHover = true;
            this.Cursor = Cursors.Hand;
            this.Invalidate();
        }

        protected override void OnMouseLeave(EventArgs e)
        {
            base.OnMouseLeave(e);

            this.isHover = false;
            this.Cursor = Cursors.Default;
            this.Invalidate();
        }

        protected virtual void DrawRegionBorder(Graphics g)
        {
            RectangleF r = this.Region.GetBounds(g);
            g.DrawPath(new Pen(Color.Yellow, 1), (this.TabType == UIToolbox.TabType.Active) ?
                (this.TabSize == UIToolbox.TabSize.Big ? TabBoundariesFactory.CreateActiveBigBoundary(new Point(0, 0), this.Width) : TabBoundariesFactory.CreateActiveLittleBoundary(new Point(0, 0), this.Width)):
                (this.TabSize == UIToolbox.TabSize.Big ? TabBoundariesFactory.CreateInactiveBigBoundary(new Point(0, 0), this.Width) : TabBoundariesFactory.CreateInactiveLittleBoundary(new Point(0, 0), this.Width)));
        }

        protected virtual void DrawTab(Graphics g)
        {
            g.DrawImage(this.Image, new Rectangle(this.ClientRectangle.X, this.ClientRectangle.Y, this.ClientRectangle.Width , this.ClientRectangle.Height));
        }

        protected virtual void DrawContentText(Graphics g)
        {
            StringFormat sf = DrawingToolbox.CreateStringFormat(StringAlignment.Far, StringAlignment.Center);
            using (SolidBrush brush = new SolidBrush((this.TabType == UIToolbox.TabType.Inactive) ? 
                this.Enabled ? Defines.OnionColor: Defines.GrapeColor  :
                Defines.CabbageColor))
            {
                if (this.isHover && this.TabType == UIToolbox.TabType.Inactive)
                    g.DrawString(this.content, Defines.NormalBoldFont, brush, new Rectangle(0, 0, this.Width, this.Height - 5), sf);
                else
                    g.DrawString(this.content, this.Font, brush, new Rectangle(0, 0, this.Width, this.Height - 5), sf);
            }
        }

        protected override void OnEnabledChanged(EventArgs e)
        {
            base.OnEnabledChanged(e);
            this.Invalidate();
        }

        public virtual void UpdateTab()
        {
            if ((this.TabType == UIToolbox.TabType.Active))
            {
                this.Image = this.TabSize == UIToolbox.TabSize.Big ? Resource1.ActiveTab : Resource1.ActiveLittleTab;
                this.Font = Defines.NormalBoldFont;
                this.Region = new Region(this.TabSize == UIToolbox.TabSize.Big ? TabBoundariesFactory.CreateActiveBigBoundary(new Point(0, 0), this.Width) : TabBoundariesFactory.CreateActiveLittleBoundary(new Point(0, 0), this.Width));
            }
            else
            {
                this.Image = this.TabSize == UIToolbox.TabSize.Big ? Resource1.InactiveTab : Resource1.InactiveLittleTab;
                this.Font = Defines.TinyBoldFont;
                this.Region = new Region(this.TabSize == UIToolbox.TabSize.Big ? TabBoundariesFactory.CreateInactiveBigBoundary(new Point(0, 0), this.Width) : TabBoundariesFactory.CreateInactiveLittleBoundary(new Point(0, 0), this.Width));
            }
        }
        #endregion
    }
}
