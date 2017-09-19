using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

namespace Enceladus.UIToolbox
{
    internal static class PenSetConstructor
    {
        #region Methods
        public static List<Pen> GradiendPenSet(Color fColor, Color sColor, int dispSticks, int thickNess)
        {
            List<Pen> penSet = new List<Pen>();

            int alpha = byte.MaxValue / dispSticks * 2;
            int half = (dispSticks / 2) + 1;
            for (int i = 0; i < half; i++)
            {
                int rs = fColor.R;
                int gs = fColor.G;
                int bs = fColor.B;
                int re = sColor.R;
                int ge = sColor.G;
                int be = sColor.B;

                int r = rs + i * ((re - rs) / half);
                int g = gs + i * ((ge - gs) / half);
                int b = bs + i * ((be - bs) / half);

                Color shadeColor = Color.FromArgb(r, g, b);
                Pen pen = new Pen(new SolidBrush(shadeColor), thickNess);
                penSet.Add(pen);
            }

            for (int i = 0; i < half; i++)
                penSet.Add(penSet[half - 1 - i]);

            return penSet;
        }

        public static List<Pen> MixPenSet(Color fColor, Color sColor, int dispSticks, int thickNess)
        {
            List<Pen> penSet = new List<Pen>();

            int alpha = byte.MaxValue / dispSticks * 2;
            int half = dispSticks / 2;

            for (int i = 0; i < half; i++)
            {
                Color shadeColor = Color.FromArgb(alpha * i, fColor);
                Pen pen = new Pen(new SolidBrush(shadeColor), thickNess);
                penSet.Add(pen);
            }

            for (int i = 0; i < dispSticks - half; i++)
            {
                Color shadeColor = Color.FromArgb(alpha * i, sColor);
                Pen pen = new Pen(new SolidBrush(shadeColor), thickNess);
                penSet.Add(pen);
            }

            return penSet;
        }

        public static List<Pen> AlphaPenSet(Color fColor, int dispSticks, int thickNess)
        {
            List<Pen> penSet = new List<Pen>();

            int alpha = byte.MaxValue / dispSticks;

            for (int i = 0; i < dispSticks; i++)
            {
                Color shadeColor = Color.FromArgb(alpha * i, fColor);
                Pen pen = new Pen(new SolidBrush(shadeColor), thickNess);
                penSet.Add(pen);
            }

            return penSet;
        }
        #endregion
    }
}
