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
    public partial class ControlLabel : Label, IResizableClient
    {
        public ControlLabel()
        {
            InitializeComponent();

            this.DetermineFont(this.CurrentFontSize);
            this.ForeColor = Defines.WildStawberryColor;

            this.MaximalExpectedFontSize = 5;
            this.SupportResizing = true;
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            e.Graphics.TextRenderingHint = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;
            base.OnPaint(e);
        }

        #region IResizeClient Implementation
        
        #endregion
        private int lastFontSizeChange = 0;
        public virtual void ApplyFontSize(int fontSizeChange)
        {
            if (this.SupportResizing && this.lastFontSizeChange != fontSizeChange)
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
    }
}
