using System;

namespace DesignPatterns.Behavioral.ChainOfResponsibility
{
    namespace GofExample
    {
        enum Topic
        {
            NoTopic, PrintTopic, PaperOrientationTopic, ApplicationTopic
        }

        class HelpHandler
        {
            private HelpHandler _successor = null;
            private Topic _topic;

            public HelpHandler(HelpHandler hnd, Topic topic) => (_successor, _topic) = (hnd, topic);

            public virtual void HandleHelp()
            {
                _successor?.HandleHelp();
            }

            public virtual bool HasHelp()
            {
                return _topic != Topic.NoTopic;
            }

            public void SetHandler(HelpHandler hnd, Topic topic) => (_successor, _topic) = (hnd, topic);
        }

        class Widget : HelpHandler
        {
            protected Widget parent = null;
            protected Widget(Widget parent, Topic topic = Topic.NoTopic) : base(parent, topic) => this.parent = parent;
        }

        class Button : Widget
        {
            public Button(Widget parent, Topic topic) : base(parent, topic)
            {
                SetHandler(parent, topic);
            }

            public override void HandleHelp()
            {
                if (HasHelp())
                    Console.WriteLine("Handled By Button");
                else
                    parent.HandleHelp();
            }
        }

        class Dialog : Widget
        {
            public Dialog(HelpHandler parent, Topic topic) : base(null) { }

            public override void HandleHelp()
            {
                if (HasHelp())
                    Console.WriteLine("Handled By Dialog");
                else
                    parent.HandleHelp();
            }
        }

        class Application : HelpHandler
        {
            public Application(Topic topic) : base(null, topic) { }

            public override void HandleHelp()
            {
                Console.WriteLine("Handled By Application");
            }
        }

        static class ExampleCode
        {
            public static void Run()
            {
                Application app = new Application(Topic.ApplicationTopic);
                Dialog dlg = new Dialog(app, Topic.PrintTopic);
                Button btn = new Button(dlg, Topic.NoTopic);

                btn.HandleHelp();
            }
        }
    }

    namespace ErrorHandlingExample
    {
        public interface IReceiver
        {
            bool HandleMessage(string msg);
        }

        public class FaxErrorHandler : IReceiver
        {
            private IReceiver nextReceiver = null;

            public FaxErrorHandler(IReceiver n) => nextReceiver = n;
            public bool HandleMessage(string msg)
            {
                if (msg.Contains("Fax"))
                {
                    Console.WriteLine("Handled by FaxErrorHandler : " + msg);
                    return true;
                }
                else
                    nextReceiver?.HandleMessage(msg);

                return false;

            }
        }

        public class EmailErrorHandler : IReceiver
        {
            private IReceiver nextReceiver = null;

            public EmailErrorHandler(IReceiver n) => nextReceiver = n;
            public bool HandleMessage(string msg)
            {
                if (msg.Contains("Email"))
                {
                    Console.WriteLine("Handled by EmailErrorHandler : " + msg);
                    return true;
                }
                else
                    nextReceiver?.HandleMessage(msg);

                return false;
            }
        }


        static class ExampleCode
        {
            public static void Run()
            {
                var errorHandler = new FaxErrorHandler(new EmailErrorHandler(null));

                errorHandler.HandleMessage("Fax is failing");
                errorHandler.HandleMessage("Email is failing");
            }
        }
    }
}
