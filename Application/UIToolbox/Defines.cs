using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Drawing.Text;
using System.IO;

namespace Enceladus.UIToolbox
{
    public static class Defines
    {
        #region Fields
        private static FontFamily fontFamily = FontFamily.GenericSansSerif;
        public static FontFamily DefaultFontFamily
        {
            get { return Defines.fontFamily; }
            set { Defines.fontFamily = value; }
        }
        #endregion

        #region Properties
        public static Font TinyBoldFont { get { return new Font(Defines.fontFamily, 10, FontStyle.Bold, GraphicsUnit.Pixel); } }
        public static Font SmallBoldFont { get { return new Font(Defines.fontFamily, 11, FontStyle.Bold, GraphicsUnit.Pixel); } }
        public static Font NormalBoldFont { get { return new Font(Defines.fontFamily, 12, FontStyle.Bold, GraphicsUnit.Pixel); } }
        public static Font BigBoldFont { get { return new Font(Defines.fontFamily, 13, FontStyle.Bold, GraphicsUnit.Pixel); } }
        public static Font HugeBoldFont { get { return new Font(Defines.fontFamily, 14, FontStyle.Bold, GraphicsUnit.Pixel); } }

        public static Font TinyItalicFont { get { return new Font(Defines.fontFamily, 10, FontStyle.Bold | FontStyle.Italic | FontStyle.Underline, GraphicsUnit.Pixel); } }
        public static Font SmallItalicFont { get { return new Font(Defines.fontFamily, 11, FontStyle.Bold | FontStyle.Italic | FontStyle.Underline, GraphicsUnit.Pixel); } }
        public static Font NormalItalicFont { get { return new Font(Defines.fontFamily, 12, FontStyle.Bold | FontStyle.Italic | FontStyle.Underline, GraphicsUnit.Pixel); } }
        public static Font BigItalicFont { get { return new Font(Defines.fontFamily, 13, FontStyle.Regular | FontStyle.Italic | FontStyle.Underline, GraphicsUnit.Pixel); } }
        public static Font HugeItalicFont { get { return new Font(Defines.fontFamily, 14, FontStyle.Regular | FontStyle.Italic | FontStyle.Underline, GraphicsUnit.Pixel); } }

        public static Font TinyFont { get { return new Font(Defines.fontFamily, 10, FontStyle.Regular, GraphicsUnit.Pixel); } }
        public static Font SmallFont { get { return new Font(Defines.fontFamily, 11, FontStyle.Regular, GraphicsUnit.Pixel); } }
        public static Font NormalFont { get { return new Font(Defines.fontFamily, 12, FontStyle.Regular, GraphicsUnit.Pixel); } }
        public static Font BigFont { get { return new Font(Defines.fontFamily, 13, FontStyle.Regular, GraphicsUnit.Pixel); } }
        public static Font HugeFont { get { return new Font(Defines.fontFamily, 14, FontStyle.Regular, GraphicsUnit.Pixel); } }
        public static Font GiantFont { get { return new Font(Defines.fontFamily, 20, FontStyle.Regular, GraphicsUnit.Pixel); } }


        public static Font TinyBaseFont { get { return new Font("Trebuchet MS", 10, FontStyle.Regular, GraphicsUnit.Pixel); } }
        public static Font TinyUnderlineFont { get { return new Font(Defines.fontFamily, 10, FontStyle.Bold | FontStyle.Underline, GraphicsUnit.Pixel); } }
        public static Font SmallUnderlineFont { get { return new Font(Defines.fontFamily, 11, FontStyle.Underline, GraphicsUnit.Pixel); } }
        public static Font NormalUnderlineFont { get { return new Font(Defines.fontFamily, 12, FontStyle.Underline, GraphicsUnit.Pixel); } } 
        public static Font BigUnderlineFont { get { return new Font(Defines.fontFamily, 13, FontStyle.Bold | FontStyle.Underline, GraphicsUnit.Pixel); } }
        public static Font EnormousFont { get { return new Font(Defines.fontFamily, 15, FontStyle.Regular, GraphicsUnit.Pixel); } }
       
        public static Font GiantBoldFont { get { return new Font(Defines.fontFamily, 20, FontStyle.Bold, GraphicsUnit.Pixel); } }
        public static Font XGiantBoldFont { get { return new Font(Defines.fontFamily, 24, FontStyle.Bold, GraphicsUnit.Pixel); } }

        public static Color GarlicColor { get { return Color.FromArgb(0, 0, 0); } }
        public static Color CabbageColor { get { return Color.FromArgb(41, 41, 41); } }
        public static Color CarrotColor { get { return Color.FromArgb(71, 71, 71); } }
        public static Color PepperColor { get { return Color.FromArgb(237, 29, 36); } }
        public static Color GrapeColor { get { return Color.FromArgb(111, 111, 111); } }
        public static Color MangoColor { get { return Color.FromArgb(176, 176, 176); } }
        public static Color ParsnipColor { get { return Color.FromArgb(240, 240, 240); } }
        public static Color MilkColor { get { return Color.FromArgb(255, 255, 255); } }
        public static Color RadishColor { get { return Color.FromArgb(20, Color.Red); } }
        public static Color CherryColor { get { return Color.FromArgb(70, Color.Red); } }
        public static Color WildStawberryColor { get { return Color.FromArgb(200, 95, 95); } }
        public static Color LeekColor { get { return Color.FromArgb(140, 140, 130); } }
        public static Color OnionColor { get { return Color.FromArgb(92, 92, 74); } }
        #endregion
    }
}
