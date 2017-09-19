using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Windows.Forms;

namespace Enceladus.UIToolbox
{
    public class DisappearingLabel : ControlLabel
    {
        protected readonly Timer timer;
        
        public DisappearingLabel()
            :base()
        {
            this.SetStyle(ControlStyles.UserPaint, true);
            this.SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            this.SetStyle(ControlStyles.DoubleBuffer, true);
            this.SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
            this.SetStyle(ControlStyles.SupportsTransparentBackColor, true);
            this.UpdateStyles();

            this.ForeColor = Defines.WildStawberryColor;
            
            this.timer = new Timer();
            this.timer.Tick += new EventHandler(timer_Tick);
        }
        public virtual void Hide(int timeInSeconds)
        {
            this.timer.Interval = timeInSeconds * 1000;
            this.timer.Start();
        }

        protected void timer_Tick(object sender, EventArgs e)
        {
            this.Reset();
        }

        protected void Reset()
        {
            this.timer.Stop();
            this.Visible = false;
        }
    }
}
