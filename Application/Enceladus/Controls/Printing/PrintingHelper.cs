using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using Enceladus.UIToolbox;

namespace Enceladus.Controls
{
    static class PrintingHelper
    {
        public static StringFormat GetTopLeftAligment()
        {
            StringFormat sf = new StringFormat();
            sf.Alignment = StringAlignment.Near;
            sf.LineAlignment = StringAlignment.Near;

            return sf;
        }
        
        public static StringFormat GetBottomLeftAligment()
        {
            StringFormat sf = new StringFormat();
            sf.Alignment = StringAlignment.Near;
            sf.LineAlignment = StringAlignment.Far;

            return sf;
        }
        
        public static StringFormat GetBottomRightAligment()
        {
            StringFormat sf = new StringFormat();
            sf.Alignment = StringAlignment.Far;
            sf.LineAlignment = StringAlignment.Far;

            return sf;
        }
        
        public static StringFormat GetTopRightAligment()
        {
            StringFormat sf = new StringFormat();
            sf.Alignment = StringAlignment.Far;
            sf.LineAlignment = StringAlignment.Near;

            return sf;
        }
        
        public static StringFormat GetLeftAligment()
        {
            StringFormat sf = new StringFormat();
            sf.Alignment = StringAlignment.Near;
            sf.LineAlignment = StringAlignment.Center;

            return sf;
        }
        
        public static StringFormat GetRightAligment()
        {
            StringFormat sf = new StringFormat();
            sf.Alignment = StringAlignment.Far;
            sf.LineAlignment = StringAlignment.Center;

            return sf;
        }

        public static Font BuildDefaultFont()
        {
            Font f = null;

            try
            {
                f = Defines.TinyBaseFont;
            }
            catch (ArgumentException)
            {
                f = new Font(FontFamily.GenericSerif, 10);
            }

            return f;
        }
    }
}
