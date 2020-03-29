using System;
using System.Collections.Generic;
using System.Drawing;

namespace DesignPatterns.Structural.Adapter
{
    namespace GoFExample
    {
        interface IShape
        {
            (Point, Point) GetBoundingBox();
        }

        class Rectangle : IShape
        {
            Point bottomLeft, topRight;
            public Rectangle(Point b, Point t) => (bottomLeft, topRight) = (b, t);
            public (Point, Point) GetBoundingBox() => (bottomLeft, topRight);
        }

        class TextView
        {
            public string Text { get; set; }
            public Point Origin { get; }
            public int Width { get; }
            public int Height { get; }
            bool IsEmpty() => string.IsNullOrEmpty(Text);

            public TextView(Point origin, int w, int h) => (Origin, Width, Height) = (origin, w, h);
        }

        class TextViewShapeAdapter : IShape
        {
            readonly TextView textView;
            public TextViewShapeAdapter(TextView t) => textView = t;

            public (Point, Point) GetBoundingBox()
            {
                Point pt1 = textView.Origin;
                Point pt2 = new Point(pt1.X + textView.Width, pt1.Y + textView.Height);
                return (pt1, pt2);
            }
        }

        static class ExampleCode
        {
            public static void Run()
            {
                List<IShape> canvas = new List<IShape>();
                var rect = new Rectangle(new Point(0, 0), new Point(10, 20));
                canvas.Add(rect);

                TextView tv = new TextView(new Point(0, 0), 100, 20);
                canvas.Add(new TextViewShapeAdapter(tv));

                foreach (var shape in canvas)
                {
                    var pt = shape.GetBoundingBox();
                    Console.WriteLine($"{pt.Item1} {pt.Item2}");
                }
            }
        }
    }
}
