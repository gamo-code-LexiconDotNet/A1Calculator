using System;
using static ReplCalculator.Calculator;
using static System.Console;

namespace ReplCalculator
{
    class Program
    {
        static void Main(string[] args)
        {
            Run();
        }

        private static void Run()
        {
            ClearScreen();

            bool show = true;
            while (show)
                show = Repl();
        }

        private static bool Repl()
        {
            Write("> ");

            string input = ReadLine();

            if (input == "q")
                return false;
            else if (input == "h")
                PrintHelpMessage();
            else if (input == "c")
                ClearScreen();
            else
                try
                {
                    WriteLine(Math.Round(EvaluateInifix(input), 4).ToString());
                } catch
                {
                    WriteLine("Could not evaluate that expresson.");
                }

            return true;
        }

        private static void ClearScreen()
        {
            Clear();
            WriteLine("[Lexicon C#/.Net Programming] Assignment 1 - Repl Calculator");
            WriteLine("(type q to quit, h for help, c for clear screen)\n");
        }

        private static void PrintHelpMessage()
        {
            WriteLine("Type in a mathematical expression using the operators:\n" +
                " ^: power of\n" +
                " *: multiply\n" +
                " /: division\n" +
                " +: addition\n" +
                " -: subtraction\n" +
                " %: modulus\n" +
                " pi: Pi\n" +
                " (,): parenthesis\n" +
                "Example:\n" +
                "> 1+9^(1/2)%3\n" +
                "1\n");
        }
    }
}
