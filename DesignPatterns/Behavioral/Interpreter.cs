using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignPatterns.Behavioral.Interpreter
{
    namespace GoFExmaple
    {
        //Boolean Expression Evaluation

        public class Context
        {
            private readonly Dictionary<string, bool> variables = new Dictionary<string, bool>();

            public bool Lookup(string varName)
            {
                if (variables.ContainsKey(varName))
                    return variables[varName];

                return false;
            }

            public void Assign(VariableExpression var, bool val) => variables[var.Name] = val;
        }

        public interface IBooleanExpression
        {
            public bool Evaluate(Context context);
            public IBooleanExpression Replace(string name, IBooleanExpression exp);
            public IBooleanExpression Copy();
        }

        public class ConstantExpression : IBooleanExpression
        {
            public bool Value { get; }
            public ConstantExpression(bool val) => Value = val;
            public bool Evaluate(Context context) => Value;

            public IBooleanExpression Replace(string name, IBooleanExpression exp) => this;

            public IBooleanExpression Copy() => this;
        }

        public class VariableExpression : IBooleanExpression
        {
            public string Name { get; }
            public VariableExpression(string name) => Name = name;
            public bool Evaluate(Context context) => context.Lookup(Name);

            public IBooleanExpression Replace(string name, IBooleanExpression exp)
            {
                if (String.Equals(name, this.Name))
                    return exp.Copy();
                else
                    return this;
            }

            public IBooleanExpression Copy() => this;
        }

        public class AndExpression : IBooleanExpression
        {
            public IBooleanExpression Operand1 { get; }
            public IBooleanExpression Operand2 { get; }
            public AndExpression(IBooleanExpression op1, IBooleanExpression op2) => (Operand1, Operand2) = (op1, op2);
            public bool Evaluate(Context context) =>
                Operand1.Evaluate(context) && Operand2.Evaluate(context);

            public IBooleanExpression Replace(string name, IBooleanExpression exp) => new AndExpression(Operand1.Replace(name, exp), Operand2.Replace(name, exp));

            public IBooleanExpression Copy() => new AndExpression(Operand1.Copy(), Operand2.Copy());
        }

        public class OrExpression : IBooleanExpression
        {
            public IBooleanExpression Operand1 { get; }
            public IBooleanExpression Operand2 { get; }
            public OrExpression(IBooleanExpression op1, IBooleanExpression op2) => (Operand1, Operand2) = (op1, op2);
            public bool Evaluate(Context context) =>
                Operand1.Evaluate(context) || Operand2.Evaluate(context);

            public IBooleanExpression Replace(string name, IBooleanExpression exp) => new OrExpression(Operand1.Replace(name, exp), Operand2.Replace(name, exp));
            public IBooleanExpression Copy() => new OrExpression(Operand1.Copy(), Operand2.Copy());
        }

        public class NotExpression : IBooleanExpression
        {
            public IBooleanExpression Operand1 { get; }
            public NotExpression(IBooleanExpression op1) => Operand1 = op1;
            public bool Evaluate(Context context) =>
                !Operand1.Evaluate(context);

            public IBooleanExpression Replace(string name, IBooleanExpression exp) => new NotExpression(Operand1.Replace(name, exp));
            public IBooleanExpression Copy() => new NotExpression(Operand1.Copy());
        }

        static class ExampleCode
        {
            public static void Run()
            {
                IBooleanExpression exp;

                VariableExpression x = new VariableExpression("X");
                VariableExpression y = new VariableExpression("Y");

                exp = new OrExpression(new AndExpression(new ConstantExpression(true), x),
                    new AndExpression(y, new NotExpression(x)));

                Context context = new Context();
                context.Assign(x, false);
                context.Assign(y, true);

                //(true && x) || (y && (not x))
                bool result = exp.Evaluate(context);

                Console.WriteLine(result);

                VariableExpression z = new VariableExpression("Z");
                NotExpression not_z = new NotExpression(z);

                //(true && x) || (!z && (not x))
                exp = exp.Replace(y.Name, not_z);
                context.Assign(z, true);
                result = exp.Evaluate(context);
                Console.WriteLine(result);
            }
        }

    }

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