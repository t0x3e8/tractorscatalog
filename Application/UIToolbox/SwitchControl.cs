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
    public partial class SwitchControl : UserControl, IToggler, IResizableClient
    {
        #region Fields and Properties
        private bool showHintText = true;
        public virtual bool ShowHintText
        {
            get { return showHintText; }
            set
            {
                showHintText = value;
                this.Invalidate();
            }
        }

        private Font hintFont = Defines.TinyFont;
        public virtual Font HintFont
        {
            get { return hintFont; }
            set
            {
                hintFont = value;
                this.Invalidate();
            }
        }
        
        private Color hintFontColor = Defines.GrapeColor;
        public virtual Color HintFontColor
        {
            get { return hintFontColor; }
            set
            {
                hintFontColor = value;
                this.Invalidate();
            }
        }
        
        private int textWidth = 100;
        public virtual int TextWidth
        {
            get { return textWidth; }
            set
            {
                textWidth = value;
                this.Invalidate();
            }
        }

        private string hint = "Off/On";
        public virtual string Hint
        {
            get { return hint; }
            set
            {
                hint = value;
                this.Invalidate();
            }
        }

        private string caption = "demonstration caption";
        public virtual string Caption
        {
            get { return caption; }
            set
            {
                caption = value;
                this.Invalidate();
            }
        }

        private bool state = true;
        public bool State
        {
            get { return state; }
            set
            {
                state = value;
                this.Invalidate();
            }
        }

        public event EventHandler SelectAll;
        public event EventHandler DeselectAll;

        protected readonly Size ballSize = new Size(11, 11);
        protected Size clickableAreaSize = new Size(26, 15);
        #endregion

        #region Constructor
        public SwitchControl()
        {
            InitializeComponent();

            this.SetStyle(ControlStyles.UserPaint, true);
            this.SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            this.SetStyle(ControlStyles.DoubleBuffer, true);
            this.SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
            this.SetStyle(ControlStyles.SupportsTransparentBackColor, true);
            this.UpdateStyles();

            this.Size = new Size(150, 15);
            this.Font = Defines.TinyFont;
            this.ForeColor = Defines.CabbageColor;

            this.MaximalExpectedFontSize = 5;
            this.SupportResizing = true;
        }
        #endregion

        #region Methods
        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
            e.Graphics.TextRenderingHint = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;
            
            //this.PaintParentBackground(e);

            int width = 0;
            this.DrawCaption(e.Graphics, ref width);
            this.DrawBackground(e.Graphics, ref width);
            this.DrawStateIndicator(e.Graphics, width);

            if (this.showHintText)
                this.DrawHint(e.Graphics, width);
        }

        protected void PaintParentBackground(PaintEventArgs e)
        {
            if (Parent != null)
            {
                Rectangle rect = new Rectangle(Left, Top, Width, Height);
                e.Graphics.TranslateTransform(-rect.X, -rect.Y);

                try
                {
                    using (PaintEventArgs pea = new PaintEventArgs(e.Graphics, rect))
                    {
                        pea.Graphics.SetClip(rect);
                        InvokePaintBackground(Parent, pea);
                        InvokePaint(Parent, pea);
                    }
                }
                finally
                {
                    e.Graphics.TranslateTransform(rect.X, rect.Y);
                }
            }
            else
                e.Graphics.FillRectangle(Brushes.Transparent, ClientRectangle);
        }
        
        protected virtual void DrawStateIndicator(Graphics g, int width)
        {
            float y = 8.5f - (this.ballSize.Height / 2); // preserve this value 8.5f as hard coded if you want to have top-alignment. For middle-alignment put (this.Height / 2)
            if (this.state)
            {
                float x = width - Resource1.SwitchBack.Width + (Resource1.SwitchBack.Width / 2) + 6;
                g.DrawImage(Resource1.ball_red11x11, x, y, 11, 11);
            }
            else
            {
                float x = width - Resource1.SwitchBack.Width + (Resource1.SwitchBack.Width / 2) - 6;
                g.DrawImage(Resource1.ball_gray11x11, x, y, 11, 11);
            }
        }

        protected virtual void DrawCaption(Graphics g, ref int width)
        {
            using (Brush fontBrush = new SolidBrush(this.ForeColor))
            {
                width = this.textWidth;
                StringFormat sf = DrawingToolbox.CreateStringFormat(StringAlignment.Near, StringAlignment.Far);
                g.DrawString(this.caption, this.DetermineFont(this.CurrentFontSize), fontBrush, new Rectangle(0, 1, width, this.Height), sf);
            }
        }

        protected virtual void DrawHint(Graphics g, int width)
        {
            using (Brush fontBrush = new SolidBrush(this.hintFontColor))
            {
                width += 6; // padding
                StringFormat sf = DrawingToolbox.CreateStringFormat(StringAlignment.Near, StringAlignment.Near);
                Rectangle rect = new Rectangle(width, 2, this.Width - width, this.Height);
                
                g.DrawString(this.hint, this.hintFont, fontBrush, rect, sf);
            }
        }

        protected virtual void DrawBackground(Graphics g, ref int width)
        {
            Image backgroundImage = Resource1.SwitchBack;
            int x = width + 6; // padding
            int y = 8 - (backgroundImage.Height / 2); // preserve this value 8 as hard coded if you want to have top-alignment. For middle-alignment put (this.Height / 2)

            g.DrawImage(backgroundImage, x, y);
            width += backgroundImage.Width;
        }
        
        protected override void OnMouseDown(MouseEventArgs mevent)
        {
            base.OnMouseDown(mevent);
            if (mevent.Button == System.Windows.Forms.MouseButtons.Left)
            {                
                Rectangle clickableRegion = new Rectangle(this.textWidth, 0, this.clickableAreaSize.Width, this.clickableAreaSize.Height);
                if (clickableRegion.Contains(mevent.Location))
                {
                    this.RaiseEvents();
                }
            }
        }

        protected void RaiseEvents()
        {
            if (state == false)
            {
                if (this.SelectAll != null)
                    this.SelectAll(this, new EventArgs());
            }
            else
            {
                if (this.DeselectAll != null)
                    this.DeselectAll(this, new EventArgs());
            }
        }
        #endregion

        #region IResizeClient implementation
        int lastFontSizeChange = 0;
        public void ApplyFontSize(int fontSizeChange)
        {
            if (this.lastFontSizeChange != fontSizeChange)
            {
                lastFontSizeChange = fontSizeChange;
                this.CurrentFontSize = FontSize.Tiny + fontSizeChange - 1;

                this.Invalidate();
            }
        }

        public FontSize CurrentFontSize { get; set; }

        protected int maximalExpectedFontSize;
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

        public Font DetermineFont(FontSize fontSize)
        {
            switch (fontSize)
            {
                case FontSize.Tiny: return Defines.TinyFont;
                case FontSize.Small: return Defines.TinyFont;
                case FontSize.Normal: return Defines.TinyFont;
                case FontSize.Big: return Defines.SmallFont;
                case FontSize.Huge: return Defines.NormalFont;
                default: return Defines.SmallFont;
            }
        }

        public PokeDelegate InformResizer { get; set; }
        public bool SupportResizing { get; set; }
        #endregion
    }
}
