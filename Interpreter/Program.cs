using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Numerics;

namespace Interpreter
{
    class Program
    {

        static void Main(string[] args)
        {


            while (true)
            {
                Console.Write("> ");
                string input = Console.ReadLine();
                try
                {
                    Lexer lexer = new Lexer(input);
                    foreach (var token in lexer.Tokens)
                    {
                        Console.WriteLine(token);
                    }

                    var parser = new Parser(lexer.Tokens);
                    Node root = parser.GetAST();
                    Console.WriteLine($"Parsed expression is: <{root.ToString()}>");

                    Interpreter interpreter = new Interpreter(root);
                    Console.WriteLine($"Evaluation of '{root.ToString()}'= {interpreter.Eval()}");

                }
                catch (Exception ex)
                {
                    Console.ForegroundColor = ConsoleColor.DarkRed;
                    Console.WriteLine($"ERROR: {ex.Message}");
                    Console.ForegroundColor = ConsoleColor.White;
                }
                
                
            }

        }

    }
}
