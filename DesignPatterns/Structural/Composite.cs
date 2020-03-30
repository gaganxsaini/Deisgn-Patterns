using System;
using System.Collections.Generic;

namespace DesignPatterns.Structural.Composite
{
    namespace MenuExample
    {
        class MenuItem
        {
            public string Text { get; set; }
            public MenuItem(string text) => Text = text;
            public virtual void OnClick() => Console.WriteLine($"Clicked on {Text}.");
            public virtual void AddItem(MenuItem item) => throw new InvalidOperationException("Invalid");
            public virtual void RemoveItem(MenuItem item) => throw new InvalidOperationException("Invalid");
        }

        class CompositeMenuItem : MenuItem
        {
            readonly List<MenuItem> items = new List<MenuItem>();
            public CompositeMenuItem(string text) : base(text) { }
            public override void AddItem(MenuItem item) => items.Add(item);
            public override void RemoveItem(MenuItem item) => items.Remove(item);

            public override void OnClick()
            {
                base.OnClick();
                Console.WriteLine("Opening Submenu : ");
                foreach (var item in items)
                    Console.WriteLine($"\t{item.Text}");
            }
        }

        static class ExampleCode
        {
            public static void Run()
            {
                CompositeMenuItem FileMenu = new CompositeMenuItem("File");
                CompositeMenuItem NewSubMenu = new CompositeMenuItem("New");
                NewSubMenu.AddItem(new MenuItem("Project"));
                NewSubMenu.AddItem(new MenuItem("Repository"));
                FileMenu.AddItem(NewSubMenu);

                MenuItem open = new MenuItem("Open");
                FileMenu.AddItem(open);

                MenuItem save = new MenuItem("Save");
                FileMenu.AddItem(save);

                MenuItem close = new MenuItem("Close");
                FileMenu.AddItem(close);

                FileMenu.OnClick();
                NewSubMenu.OnClick();
                close.OnClick();
            }

        }
    }
}
