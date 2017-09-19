using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;

namespace Enceladus.UIToolbox
{
    public static class DrawingToolbox
    {
        [System.Runtime.InteropServices.DllImport("Gdi32.dll", EntryPoint = "CreateRoundRectRgn")]
        public static extern System.IntPtr CreateRoundRectRgn
        (
            int nLeftRect, // x-coordinate of upper-left corner
            int nTopRect, // y-coordinate of upper-left corner
            int nRightRect, // x-coordinate of lower-right corner
            int nBottomRect, // y-coordinate of lower-right corner
            int nWidthEllipse, // height of ellipse
            int nHeightEllipse // width of ellipse
        );

        [System.Runtime.InteropServices.DllImport("gdi32.dll", EntryPoint = "DeleteObject")]
        public static extern bool DeleteObject(System.IntPtr hObject);

        public static StringFormat CreateStringFormat(StringAlignment horizontal, StringAlignment vertical)
        {
            StringFormat sf = new StringFormat();
            sf.Alignment = vertical;
            sf.LineAlignment = horizontal;
            return sf;
        }

        public static GraphicsPath GetRoundedRectanglePath(Rectangle rectangle, int cornerRadius)
        {
            Point[] points;
            Rectangle cornerRectangle;
            GraphicsPath roundedRectanglePath;
            try
            {
                points = new Point[4];
                cornerRectangle = new Rectangle();
                roundedRectanglePath = new GraphicsPath();

                points[0] = new Point(rectangle.Left, rectangle.Top);
                points[1] = new Point(rectangle.Right, rectangle.Top);
                points[2] = new Point(rectangle.Right, rectangle.Bottom);
                points[3] = new Point(rectangle.Left, rectangle.Bottom);

                // upper left

                cornerRectangle.Location = points[0];
                cornerRectangle.Size = new Size(cornerRadius * 2, cornerRadius * 2);
                roundedRectanglePath.AddArc(cornerRectangle, 180, 90);

                // line in top.
                roundedRectanglePath.AddLine(
                    new Point(points[0].X + cornerRadius, points[0].Y),
                    new Point(points[1].X - cornerRadius, points[1].Y));

                // upper right
                cornerRectangle.Size = new Size(cornerRadius * 2, cornerRadius * 2);
                cornerRectangle.Location = new Point(points[1].X - cornerRectangle.Width, points[1].Y);
                roundedRectanglePath.AddArc(cornerRectangle, 270, 90);

                // line in right side.
                roundedRectanglePath.AddLine(
                    new Point(points[1].X, points[1].Y + cornerRadius),
                    new Point(points[2].X, points[2].Y - cornerRadius));

                // lower right
                cornerRectangle.Size = new Size(cornerRadius * 2, cornerRadius * 2);
                cornerRectangle.Location = new Point(points[2].X - cornerRectangle.Width, points[2].Y - cornerRectangle.Height);
                roundedRectanglePath.AddArc(cornerRectangle, 0, 90);

                // line in bottom.
                roundedRectanglePath.AddLine(
                    new Point(points[2].X - cornerRadius, points[2].Y),
                    new Point(points[3].X + cornerRadius, points[3].Y));

                // lower left
                cornerRectangle.Size = new Size(cornerRadius * 2, cornerRadius * 2);
                cornerRectangle.Location = new Point(points[3].X, points[3].Y - cornerRectangle.Height);
                roundedRectanglePath.AddArc(cornerRectangle, 90, 90);

                // line in left side.
                roundedRectanglePath.AddLine(
                    new Point(points[3].X, points[3].Y - cornerRadius),
                    new Point(points[0].X, points[0].Y + cornerRadius));
            }
            catch (Exception)
            {
                throw;
            }

            return roundedRectanglePath;
        }

        public static Rectangle Trim(Rectangle rect, Size controlSize)
        {
            if (rect.X < 0)
            {
                rect.Width -= 0 - rect.X;
                rect.X = 0;
            }
            if (rect.Y < 0)
            {
                rect.Height -= 0 - rect.Y;
                rect.Y = 0;
            }
            if ((rect.X + rect.Width) > controlSize.Width)
                rect.Width -= (rect.Width + rect.X + 1) - controlSize.Width;
            if ((rect.Y + rect.Height) > controlSize.Height)
                rect.Height -= (rect.Height + rect.Y + 1) - controlSize.Height;

            return rect;
        }

        public static Rectangle BuildRectangleUponTwoPoints(Point p1, Point p2)
        {
            Rectangle rect = new Rectangle();
            rect.X = (p1.X > p2.X) ? p2.X : p1.X;
            rect.Y = (p1.Y > p2.Y) ? p2.Y : p1.Y;
            rect.Width = Math.Abs(p1.X - p2.X);
            rect.Height = Math.Abs(p1.Y - p2.Y);

            return rect;
        }

        public static PointF[] DetermineOutermostPoints(RectangleF rect)
        {
            PointF[] points = new PointF[4];
            points[0] = new PointF(rect.X, rect.Y);
            points[1] = new PointF(rect.X, rect.Y + rect.Height);
            points[2] = new PointF(rect.X + rect.Width, rect.Y);
            points[3] = new PointF(rect.X + rect.Width, rect.Y + rect.Height);

            return points;
        }

        public static Image SetImageOpacity(Image imgPic, float imgOpac)
        {
            Bitmap bmpPic = new Bitmap(imgPic.Width, imgPic.Height);
            Graphics gfxPic = Graphics.FromImage(bmpPic);
            ColorMatrix cmxPic = new ColorMatrix();
            cmxPic.Matrix33 = imgOpac;

            ImageAttributes iaPic = new ImageAttributes();
            iaPic.SetColorMatrix(cmxPic, ColorMatrixFlag.Default, ColorAdjustType.Bitmap);
            gfxPic.DrawImage(imgPic, new Rectangle(0, 0, bmpPic.Width, bmpPic.Height), 0, 0, imgPic.Width, imgPic.Height, GraphicsUnit.Pixel, iaPic);
            gfxPic.Dispose();

            return bmpPic;
        }
    }
}
