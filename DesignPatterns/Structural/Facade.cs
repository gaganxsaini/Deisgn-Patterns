using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignPatterns.Structural.Facade
{
    namespace GoFExample
    {
        class Scanner
        {
            public Scanner(string input) => Console.WriteLine("Scanning input...");
            //...
        }

        class Parser
        {
            public void Parse(Scanner s) => Console.WriteLine("Parsing...");
            //...
        }

        class CodeGenerator
        {
            public void GenerateCode(Parser p) => Console.WriteLine("Generating Code...");
            //...
        }

        class Compiler
        {
            public void Compile(string code)
            {
                Scanner s = new Scanner(code);
                Parser p = new Parser();
                p.Parse(s);
                CodeGenerator cg = new CodeGenerator();
                cg.GenerateCode(p);
            }
        }

        public static class ExampleCode
        {
            public static void Run()
            {
                string code = "Void Main(){}";
                Compiler c = new Compiler();
                c.Compile(code);
            }
        }
    }
}
