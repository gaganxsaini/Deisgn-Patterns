using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignPatterns.Behavioral.Interpreter
{

    namespace IntergerToWords
    {
        public class Context
        {
            public int Input { get; }
            public bool CanProceed { get; set; } = true;
            public string Output { get; set; }
            public Context(int input) => Input = input;
        }

        //Abstract class to provide default implemenatation
        abstract class InputExpression
        {
            public virtual void Interpret(Context context)
            {
                if (context.CanProceed && (context.Input < 100 || context.Input > 999))
                    context.CanProceed = false;
            }
        }

        class HundredExpression : InputExpression
        {
            public override void Interpret(Context context)
            {
                base.Interpret(context);

                if (context.CanProceed)
                {
                    int hundreds = context.Input / 100;
                    context.Output += hundreds switch
                    {
                        1 => "One Hundred",
                        2 => "Two Hundred",
                        3 => "Three Hundred",
                        4 => "Four Hundred",
                        5 => "Five Hundred",
                        6 => "Six Hundred",
                        7 => "Seven Hundred",
                        8 => "Eight Hundred",
                        9 => "Nine Hundred",
                        _ => String.Empty
                    };

                    context.Output += " ";
                }
            }
        }

        class TensExpression : InputExpression
        {
            public override void Interpret(Context context)
            {
                base.Interpret(context);

                if (context.CanProceed)
                {
                    int tens = context.Input % 100;
                    tens /= 10;
                    context.Output += tens switch
                    {
                        1 => "One Ten and",
                        2 => "Twenty",
                        3 => "Thirty",
                        4 => "Forty",
                        5 => "Fifty",
                        6 => "Sixty",
                        7 => "Seventy",
                        8 => "Eighty",
                        9 => "Ninety",
                        _ => String.Empty,
                    };

                    context.Output += " ";
                }
            }
        }
        class UnitExpression : InputExpression
        {
            public override void Interpret(Context context)
            {
                base.Interpret(context);

                if (context.CanProceed)
                {
                    int units = context.Input % 100;
                    units %= 10;
                    context.Output += units switch
                    {
                        1 => "One",
                        2 => "Two",
                        3 => "Three",
                        4 => "Four",
                        5 => "Five",
                        6 => "Six",
                        7 => "Seven",
                        8 => "Eight",
                        9 => "Nine",
                        _ => String.Empty,
                    };
                }
            }
        }

        public static class ExampleCode
        {
            public static void Run()
            {
                Console.WriteLine("Enter a 3 digit number only (i.e. 100 to 999)");

                string str = "";
                
                do
                {
                    str = Console.ReadLine();
                    if (String.IsNullOrWhiteSpace(str))
                        continue;

                    int.TryParse(str, out int x);
                    Context context = new Context(x);

                    // Build the 'parse tree'
                    List<InputExpression> expTree = new List<InputExpression>();
                    expTree.Add(new HundredExpression());
                    expTree.Add(new TensExpression());
                    expTree.Add(new UnitExpression());

                    foreach (InputExpression inputExp in expTree)
                        inputExp.Interpret(context);

                    Console.WriteLine($"{context.Input} : {context.Output}");
                }
                while (str != "0");
            }
        }
    }
}