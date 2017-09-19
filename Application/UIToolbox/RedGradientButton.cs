using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

namespace Enceladus.UIToolbox
{
    public class RedGradientButton : GradientButton
    {
        protected override void DrawBackground(System.Drawing.Graphics g)
        {
            if (this.isClicked)
                g.DrawImage(Resource1.Button_Clicked, new Point(0, 0));
            else if (this.isHover)
                g.DrawImage(Resource1.Button_Hover, new Point(0, 0));
            else
                g.DrawImage(Resource1.Button_Red, new Point(0, 0));
        }
    }
}
