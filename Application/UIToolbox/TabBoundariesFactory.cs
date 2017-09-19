using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing.Drawing2D;
using System.Drawing;

namespace Enceladus.UIToolbox
{
    class TabBoundariesFactory
    {
        public static GraphicsPath CreateActiveBigBoundary(Point point, int tabWidth)
        {
            List<Point> points = new List<Point>();
            points.Add(new Point(point.X + 2, point.Y + 2));
            points.Add(new Point(point.X + ((tabWidth == -1) ? 117 : tabWidth - 3), point.Y + 2));
            points.Add(new Point(point.X + ((tabWidth == -1) ? 123 : tabWidth), point.Y + 29));
            points.Add(new Point(point.X + -1, point.Y + 29));
            
            GraphicsPath path = new GraphicsPath();
            path.AddPolygon(points.ToArray());
            path.CloseFigure();
            return path;
        }

        public static GraphicsPath CreateInactiveBigBoundary(Point point, int tabWidth)
        {
            List<Point> points = new List<Point>();
            points.Add(new Point(point.X + 5, point.Y + 7));
            points.Add(new Point(point.X + ((tabWidth == -1) ? 105 : tabWidth - 6), point.Y + 7));
            points.Add(new Point(point.X + ((tabWidth == -1) ? 111 : tabWidth), point.Y + 29));
            points.Add(new Point(point.X + 0, point.Y + 29));
            
            GraphicsPath path = new GraphicsPath();
            path.AddPolygon(points.ToArray());
            path.CloseFigure();
            return path;
        }

        public static GraphicsPath CreateActiveLittleBoundary(Point point, int tabWidth)
        {
            List<Point> points = new List<Point>();
            points.Add(new Point(point.X + 2, point.Y + 2));
            points.Add(new Point(point.X + ((tabWidth == -1) ? 44 : tabWidth - 3), point.Y + 2));
            points.Add(new Point(point.X + ((tabWidth == -1) ? 47 : tabWidth), point.Y + 29));
            points.Add(new Point(point.X + -1, point.Y + 29));

            GraphicsPath path = new GraphicsPath();
            path.AddPolygon(points.ToArray());
            path.CloseFigure();
            return path;
        }

        public static GraphicsPath CreateInactiveLittleBoundary(Point point, int tabWidth)
        {
            List<Point> points = new List<Point>();
            points.Add(new Point(point.X + 3, point.Y + 7));
            points.Add(new Point(point.X + ((tabWidth == -1) ? 30 : tabWidth - 6), point.Y + 7));
            points.Add(new Point(point.X + ((tabWidth == -1) ? 34 : tabWidth), point.Y + 29));
            points.Add(new Point(point.X + 0, point.Y + 29));

            GraphicsPath path = new GraphicsPath();
            path.AddPolygon(points.ToArray());
            path.CloseFigure();
            return path;
        }
    }
}
