using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignPatterns.Behavioral.Template
{
    namespace DocEditorExample
    {
        abstract class Document
        {
            public string Name { get; set; }
            public abstract void DoSave();


            public void Open() => Console.WriteLine("Doc Opened");

            //Template Method
            public void Close()
            {
                Console.WriteLine("Do you want to save (Y/N)?");
                string ans = Console.ReadLine();

                if (ans.ToUpper() == "Y")
                    DoSave();
            }

            public abstract void DoRead();
        }

        abstract class DocumentEditor
        {
            List<Document> docs = new List<Document>();

            //Template Method
            public Document OpenDocument(string name)
            {
                if (!CanOpen(name))
                    throw new InvalidOperationException("Cannot open document.");

                Document doc = DoCreateDocument(name);

                if (doc != null)
                {
                    docs.Add(doc);
                    AboutToOpenDocument();
                    doc.Open();
                    doc.DoRead();
                }

                return doc;
            }
            public void CloseDocument(Document d)
            {
                docs.Remove(d);
                d.Close();
            }

            public abstract Document DoCreateDocument(string name);

            public virtual void AboutToOpenDocument()//A Hook. Client may/may not override this.
            {
            }

            public abstract bool CanOpen(string path);
        }

        class ExcelDocument : Document
        {
            public override void DoRead() => Console.WriteLine("Reading Excel Document");
            public override void DoSave() => Console.WriteLine("Saving Excel Document");
        }

        class ExcelEditor : DocumentEditor
        {
            public override bool CanOpen(string path) => path.EndsWith(".xls") || path.EndsWith("xlsx");
            public override Document DoCreateDocument(string name) => new ExcelDocument() { Name = name };
        }

        public static class ExampleCode
        {
            public static void Run()
            {
                try
                {
                    DocumentEditor editor = new ExcelEditor();
                    var doc = editor.OpenDocument("abc.xls");
                    doc.Close();

                    doc = editor.OpenDocument("abc.xlx");
                }
                catch (InvalidOperationException e)
                {
                    Console.WriteLine(e.Message);
                }
            }
        }
    }

}
