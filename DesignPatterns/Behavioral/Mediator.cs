using System;
using System.Collections.Generic;

namespace DesignPatterns.Behavioral.Mediator
{
    namespace GoFExample
    {
        abstract class DialogDirector
        {
            public abstract void WidgetChanged(Widget w);
            public abstract void ShowDialog();
            public abstract void CloseDialog();
        }

        class Widget
        {
            private DialogDirector dialogDirector = null;
            public Widget(DialogDirector dir) => dialogDirector = dir;
            public virtual void Changed() => dialogDirector.WidgetChanged(this);
        }

        class Button : Widget
        {
            private bool _isEnabled = false;
            public bool IsEnabled
            {
                get => _isEnabled;
                set
                {
                    _isEnabled = value;
                    Console.WriteLine($"Button Enabled? : {_isEnabled}");
                }
            }
            public Button(DialogDirector dir) : base(dir) { }
            public void OnClick() 
            {
                Console.WriteLine("Button Cliked");
                Changed(); 
            }
        }

        class ListBox : Widget
        {
            private string _selectedItem = "";
            public string SelectedItem
            {
                get => _selectedItem;
                set
                {
                    _selectedItem = value;
                    Console.WriteLine($"Selected Item Changed : {_selectedItem}");
                    Changed();
                }
            }

            private readonly List<string> items = new List<string>();
            public void AddItem(string item) => items.Add(item);
            public ListBox(DialogDirector dir) : base(dir) { }
        }

        class TextBox : Widget
        {
            private string _text = "";
            public string Text
            {
                get => _text;
                set
                {
                    _text = value;
                    Console.WriteLine($"Text Changed : {_text}");
                    Changed();
                }
            }
            public TextBox(DialogDirector dir) : base(dir) { }
        }

        class FontSelectorDialogDirector : DialogDirector
        {
            public Button BtnOk { get; }
            public ListBox LbFonts { get; }
            public TextBox TxtFont { get; }

            public FontSelectorDialogDirector()
            {
                BtnOk = new Button(this);
                LbFonts = new ListBox(this);
                LbFonts.AddItem("Arial");
                LbFonts.AddItem("Calibri");
                LbFonts.AddItem("Comic Sans");
                TxtFont = new TextBox(this);
            }

            public override void CloseDialog()
            {
                Console.WriteLine("Closing Dialog Box...");
            }

            public override void ShowDialog()
            {
                Console.WriteLine("Showing Dialog Box...");
            }

            public override void WidgetChanged(Widget w)
            {
                if (w == LbFonts)
                {
                    TxtFont.Text = LbFonts.SelectedItem;
                    BtnOk.IsEnabled = true;
                }
                else if (w == BtnOk)
                {
                    //Apply the font and close dialog
                    this.CloseDialog();
                }
            }
        }

        class ExampleCode
        {
            public static void Run()
            {
                var fontDialog = new FontSelectorDialogDirector();
                fontDialog.ShowDialog();
                fontDialog.LbFonts.SelectedItem = "Comic Sans";
                fontDialog.BtnOk.OnClick();
            }
        }
    }
}
