using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using Enceladus.Api.UI;

namespace Enceladus.UIToolbox
{
    public class InputBoxLabel : UserControl
    {
        #region Fields and Properties
        protected string label;
        public virtual string Label
        {
            get { return this.label; }
            set
            {
                this.label = value;
                this.Invalidate();
            }                
        }

        public virtual string Pattern
        {
            get;
            set;
        }

        public virtual StringAlignment HorizontalAligment
        {
            get;
            set;
        }
        #endregion

        #region Constructors
        public InputBoxLabel()
        {
            this.SetStyle(ControlStyles.UserPaint, true);
            this.SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            this.SetStyle(ControlStyles.DoubleBuffer, true);
            this.SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
            this.SetStyle(ControlStyles.SupportsTransparentBackColor, true);
            this.UpdateStyles();

            this.Font = Defines.TinyBoldFont;
            this.ForeColor = Defines.CarrotColor;
            this.HorizontalAligment = StringAlignment.Near;
        }
        #endregion

        #region Methods
        protected override void OnPaint(PaintEventArgs e)
        {
            e.Graphics.TextRenderingHint = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;

            StringFormat sf = new StringFormat();
            sf.LineAlignment = StringAlignment.Center;
            sf.Alignment = this.HorizontalAligment;

            e.Graphics.DrawString(this.label, this.Font, new SolidBrush(this.ForeColor), new Rectangle(0, 0, this.Width, this.Height), sf);
        }
        #endregion

        private void InitializeComponent()
        {
            this.SuspendLayout();
            // 
            // InputBoxLabel
            // 
            this.Name = "InputBoxLabel";
            this.Size = new System.Drawing.Size(48, 20);
            this.ResumeLayout(false);

        }
    }
}
