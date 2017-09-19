using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using Enceladus.Api;
using System.Drawing.Drawing2D;

namespace Enceladus.UIToolbox
{
    public partial class GradientIconButton : GradientButton
    {
        #region Fields and properties
        protected Image icon;
        public virtual Image Icon
        {
            get { return this.icon; }
            set
            {
                this.icon = value;
                this.Invalidate();
            }
        }

        protected readonly int SpaceBetweenImageText = 20;
        #endregion

        #region Constructors
        public GradientIconButton()
        {
            InitializeComponent();

            this.SetStyle(ControlStyles.UserPaint, true);
            this.SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            this.SetStyle(ControlStyles.DoubleBuffer, true);
            this.SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
            this.SetStyle(ControlStyles.SupportsTransparentBackColor, true);
            this.UpdateStyles();

            this.Size = new Size(190, 20);
            this.BackColor = Color.Transparent;
        }
        #endregion

        #region Methods
        protected override void OnPaint(PaintEventArgs pevent)
        {
            base.OnPaint(pevent);

            this.DrawIcon(pevent.Graphics);
        }

        protected override void DrawText(Graphics g)
        {
            StringFormat sf = DrawingToolbox.CreateStringFormat(StringAlignment.Center, StringAlignment.Near);
            using (SolidBrush brush = new SolidBrush(this.Enabled ? Defines.GarlicColor : Defines.GrapeColor))
            {
                g.DrawString(this.Text, Defines.NormalFont, brush,
                    new RectangleF(this.Margin.Left + 16 + SpaceBetweenImageText, this.Margin.Top, this.Width - this.Margin.Vertical - 16 - SpaceBetweenImageText - 1, this.Height - this.Margin.Horizontal - 1), sf);
            }
        }

        protected virtual void DrawIcon(Graphics g)
        {
            if (this.icon != null)
                g.DrawImage(this.icon, this.Margin.Left + 10, this.Margin.Top + 4, 16, 16); //this.IconSize.Width, this.IconSize.Height);
        }

        protected override void DrawBackground(Graphics g)
        {
            if (this.isClicked)
                g.DrawImage(Resource1.SmallButtonClick, 0, 0, this.Width, this.Height);
            else if (this.isHover)
                g.DrawImage(Resource1.SmallButtonHover, 0, 0, this.Width, this.Height);
            else
                g.DrawImage(Resource1.SmallButton, 0, 0, this.Width, this.Height);
        }
        #endregion
    }
}
