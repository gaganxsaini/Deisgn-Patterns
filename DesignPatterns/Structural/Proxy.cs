using System;
using System.Collections.Generic;

namespace DesignPatterns.Structural.Proxy
{
    namespace ImageProxyExample
    {
        abstract class Graphic
        {
            public string Path { get; set; }
            public Graphic(string p) => Path = p;
            public abstract void Load();
            public abstract void Draw();
        }

        class Image : Graphic
        {
            public Image(string p) : base(p) { }
            public override void Load() => Console.WriteLine($"Loading Image : {Path}.");
            public override void Draw() => Console.WriteLine($"Drawing Image : {Path}.");
        }

        class ImageProxy : Graphic
        {
            Image image = null;
            public override void Draw()
            {
                if (image == null)
                {
                    Console.WriteLine($"Image Proxy fetching the actual Image {Path}");
                    image = new Image(Path);
                    image.Load();
                    image.Draw();
                }
            }

            public override void Load() => Console.WriteLine($"Loading image proxy for {Path}.");
            public ImageProxy(string p) : base(p) { }
        }

        class ImageDocument
        {
            public List<Graphic> Images { get; set; } = new List<Graphic>();
            public void ScrollTo(int imgId)
            {
                Console.WriteLine($"\nScrolling document to {imgId}.");
                Images[imgId].Draw();
            }

            public void Load()
            {
                foreach (var img in Images)
                    img.Load();
            }
        }

        static class ExampleCode
        {
            public static void Run()
            {
                ImageDocument doc = new ImageDocument();
                doc.Images.Add(new ImageProxy(@"C:\Cat.jpg"));
                doc.Images.Add(new ImageProxy(@"C:\Dog.jpg"));
                doc.Images.Add(new ImageProxy(@"C:\Monkey.jpg"));

                doc.Load();
                doc.ScrollTo(0);
                doc.ScrollTo(2);
                doc.ScrollTo(1);
            }
        }
    }
}
