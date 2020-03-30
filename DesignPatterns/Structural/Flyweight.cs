using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignPatterns.Structural.Flyweight
{
    namespace GoFExample
    {
        class Glyph
        {
            public virtual void Draw(GlyphContext context) {}
        }

        class GlyphContext
        {
            //Contains font information for a nth character
            Dictionary<int, string> dict = new Dictionary<int, string>();

            public void SetFont(int i, string font) => dict[i] = font;
            public string GetFont()
            {
                if (dict.ContainsKey(currentIndex))
                    return dict[currentIndex];
                else
                    return "Default";
            }

            private int currentIndex = 0;

            public void Prev() => currentIndex--;
            public void Next() => currentIndex++;
            public void SetIndex(int index) => currentIndex = index;

        }

        class Character : Glyph
        {
            readonly char ch;
            public Character(char c) => ch = c;
            public override void Draw(GlyphContext context) => Console.WriteLine($"Drawing {ch} with {context.GetFont()} Font.");
        }

        class GlyphFactory
        {
            const int MaxChars = 26;
            readonly Character[] chars = new Character[MaxChars];

            public GlyphFactory()
            {
                for (int i = 0; i < MaxChars; i++)
                    chars[i] = null;
            }

            public Character GetGlyph(char c)
            {
                int i = c - 'A';
                if (chars[i] == null)
                    chars[i] = new Character(c);

                return chars[i];
            }
        }

        static class ExampleCode
        {
            public static void Run()
            {
                GlyphFactory fac = new GlyphFactory();

                //Position & character
                Dictionary<int, Character> document = new Dictionary<int, Character>();
                document.Add(1, fac.GetGlyph('A'));
                document.Add(2, fac.GetGlyph('B'));
                document.Add(3, fac.GetGlyph('A'));

                GlyphContext context = new GlyphContext();
                context.SetFont(1, "Calibri");
                context.SetFont(2, "Times New Roman");
                context.SetFont(3, "Comic Sans");

                context.SetIndex(1);

                foreach (var item in document)
                {
                    item.Value.Draw(context);
                    context.Next();
                }

            }
        }
    }
}
