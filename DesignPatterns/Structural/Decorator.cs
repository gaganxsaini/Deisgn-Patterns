using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignPatterns.Structural.Decorator
{
    namespace GoFExample
    {
        class VisualComponent
        {
            public virtual void Draw() { }
            //...
        }

        class Decorator : VisualComponent
        {
            protected VisualComponent component;
            public Decorator(VisualComponent comp)
            {
                component = comp;
            }
        }

        class TextView : VisualComponent
        {
            public override void Draw() => Console.WriteLine("Drawing TextView.");
        }

        class BorderDecorator : Decorator
        {
            int borderWidth = 0;
            public BorderDecorator(VisualComponent c, int width) : base(c) => borderWidth = width;

            public override void Draw()
            {
                Console.WriteLine($"Drawing Border of Width : {borderWidth}.");
                component.Draw();
            }
        }


        public static class ExmapleCode
        {
            public static void Run()
            {
                var tv = new TextView();
                var border = new BorderDecorator(tv, 1);
                border = new BorderDecorator(border, 2);

                border.Draw();
            }
            
        }

    }

}
