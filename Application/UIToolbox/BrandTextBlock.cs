using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using Enceladus.Api;
using Enceladus.Api.UI;
using Enceladus.StringLibrary;

namespace Enceladus.UIToolbox
{
    public partial class BrandTextBlock : UserControl, IResizableClient
    {
        #region Fields & properties
        protected Brand brandEntity;
        public Brand BrandEntity
        {
            get { return this.brandEntity; }
            set
            {
                this.brandEntity = value;
                this.Invalidate();
            }
        }

        protected bool showBorder;
        public bool ShowBorder
        {
            get { return this.showBorder; }
            set
            {
                this.showBorder = value;
                this.Invalidate();
            }
        }
        #endregion

        #region Constructors
        public BrandTextBlock()
        {
            InitializeComponent();

            this.SetStyle(ControlStyles.UserPaint, true);
            this.SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            this.SetStyle(ControlStyles.DoubleBuffer, true);
            this.SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
            this.SetStyle(ControlStyles.SupportsTransparentBackColor, true);
            this.UpdateStyles();

            this.SupportResizing = true;
        }
        #endregion

        #region Methods
        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            e.Graphics.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAlias;

            float x = 10;
            float y = 0;
            float lineHeight = 18;

            if (BrandEntity != null)
            {
                SolidBrush brush = new SolidBrush(Defines.CabbageColor);
                
                StringFormat sf = new StringFormat();
                sf.FormatFlags = StringFormatFlags.LineLimit;

                Font headerFont = this.DetermineHeaderFont(this.CurrentFontSize);
                Font regularFont = this.DetermineFont(this.CurrentFontSize);

                if (!string.IsNullOrEmpty(BrandEntity.Producer))
                    e.Graphics.DrawString(BrandEntity.Producer, headerFont, brush, new PointF(x, this.MoveCarret(ref y, lineHeight)));
                
                this.MoveCarret(ref y, lineHeight);
                if (!string.IsNullOrEmpty(BrandEntity.Company))
                {
                    SizeF textSize = e.Graphics.MeasureString(BrandEntity.Company, Defines.NormalFont);
                    if (textSize.Width > this.Width - x)
                        lineHeight *= 2;
                    e.Graphics.DrawString(BrandEntity.Company, regularFont, brush, new RectangleF(x, this.MoveCarret(ref y, lineHeight), this.Width - x, lineHeight), sf);
                }

                if (!string.IsNullOrEmpty(BrandEntity.Address))
                    e.Graphics.DrawString(BrandEntity.Address, regularFont, brush, new RectangleF(x, this.MoveCarret(ref y, lineHeight), this.Width - x, lineHeight), sf);

                if (!string.IsNullOrEmpty(BrandEntity.CodeAndCity))
                    e.Graphics.DrawString(BrandEntity.CodeAndCity, regularFont, brush, new RectangleF(x, this.MoveCarret(ref y, lineHeight), this.Width - x, lineHeight), sf);

                if (!string.IsNullOrEmpty(BrandEntity.Phone))
                    e.Graphics.DrawString(BrandEntity.Phone, regularFont, brush, new RectangleF(x, this.MoveCarret(ref y, lineHeight), this.Width - x, lineHeight), sf);

                if (!string.IsNullOrEmpty(BrandEntity.Fax))
                    e.Graphics.DrawString(BrandEntity.Fax, regularFont, brush, new RectangleF(x, this.MoveCarret(ref y, lineHeight), this.Width - x, lineHeight), sf);

                if (!string.IsNullOrEmpty(BrandEntity.Internet))
                    e.Graphics.DrawString(BrandEntity.Internet, regularFont, brush, new RectangleF(x, this.MoveCarret(ref y, lineHeight), this.Width - x, lineHeight), sf);

                if (!string.IsNullOrEmpty(BrandEntity.Internet2))
                    e.Graphics.DrawString(BrandEntity.Internet2, regularFont, brush, new RectangleF(x, this.MoveCarret(ref y, lineHeight), this.Width - x, lineHeight), sf);
                
                brush.Dispose();
            }

            if (this.showBorder)
                e.Graphics.DrawRectangle(Pens.Black, new Rectangle(0, 0, this.Width - 1, this.Height - 1));
        }

        protected float MoveCarret(ref float yPos, float lineHeight)
        {
            yPos += lineHeight;
            return yPos;
        }

        protected override void OnSizeChanged(EventArgs e)
        {
            base.OnSizeChanged(e);

            this.CalculateMaximalExpectedFontSize();
        }

        Size lastSize = Size.Empty;
        private void CalculateMaximalExpectedFontSize()
        {
            if (this.lastSize == Size.Empty)
            {
                this.lastSize = this.Size;
            }
            else
            {
                for (int i = 0; i < 5; i++)
                {
                    Font enlargeFont = this.DetermineHeaderFont(FontSize.Tiny + i);
                    Size textSize  = TextRenderer.MeasureText(this.BrandEntity.Producer, enlargeFont);
                    Size cellSize = new Size(this.Width - 50, enlargeFont.Height);

                    bool isTextBigger = (textSize.Height > cellSize.Height) && (textSize.Width > cellSize.Width);

                    if (!isTextBigger)
                    {
                        this.MaximalExpectedFontSize = (i == 0) ? 1 : i + 1;
                        break;
                    }
                }
            }
        }

        protected override void OnMouseEnter(EventArgs e)
        {
            base.OnMouseEnter(e);

            if (this.BrandEntity != null && (!string.IsNullOrEmpty(this.BrandEntity.Internet) || !string.IsNullOrEmpty(this.BrandEntity.Internet2)))
            {
                this.Cursor = Cursors.Hand;
            }
        }

        protected override void OnMouseLeave(EventArgs e)
        {
            base.OnMouseLeave(e);

            this.Cursor = Cursors.Default;
        }


        protected override void OnMouseClick(MouseEventArgs e)
        {
            base.OnMouseClick(e);

            if (this.BrandEntity != null && (!string.IsNullOrEmpty(this.BrandEntity.Internet) || !string.IsNullOrEmpty(this.BrandEntity.Internet2)))
            {
                try
                {
                    string link = this.brandEntity.Internet;
                    if (string.IsNullOrEmpty(link))
                        link = this.brandEntity.Internet2;
                    System.Diagnostics.Process.Start(link);
                }
                catch { }
            }
        }
        #endregion

        #region IResizeClient implementation
        private int previousFontSize = 0;
        public void ApplyFontSize(int fontSize)
        {
            if (this.previousFontSize != fontSize)
            {
                this.previousFontSize = fontSize;
                this.CurrentFontSize = FontSize.Tiny + fontSize;

                this.Invalidate();
            }
        }

        public FontSize CurrentFontSize { get; protected set; }

        private int maximalExpectedFontSize;
        public int MaximalExpectedFontSize
        {
            get { return this.maximalExpectedFontSize; }
            set
            {
                if (this.maximalExpectedFontSize != value)
                {
                    this.maximalExpectedFontSize = value;
                    if (this.InformResizer != null)
                    {
                        this.InformResizer.Invoke();
                    }
                }
            }
        }

        public Font DetermineFont(FontSize fontSize)
        {
            switch (fontSize)
            {
                //case FontSize.Tiny: return Defines.TinyFont;
                //case FontSize.Small: return Defines.SmallFont;
                //case FontSize.Normal: return Defines.NormalFont;
                //case FontSize.Big: return Defines.BigFont;
                //case FontSize.Huge: return Defines.HugeFont;
                default: return Defines.HugeFont;
            }
        }

        private System.Drawing.Font DetermineHeaderFont(FontSize fontSize)
        {
            switch (fontSize)
            {
                //case FontSize.Tiny:
                //case FontSize.Small:
                //case FontSize.Normal:
                //case FontSize.Big: return Defines.GiantBoldFont;
                //case FontSize.Huge: return Defines.XGiantBoldFont;
                default: return Defines.XGiantBoldFont;
            }
        }

        public PokeDelegate InformResizer { get; set; }
        public bool SupportResizing { get; set; }
        #endregion
    }
}
