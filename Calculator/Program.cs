using System;
using System.ComponentModel;
using static Lexicon.Calculator;
using static System.Console;

namespace Lexicon
{
    class Program
    {
        static void Main(string[] args)
        {
            Run();
        }

        private static void Run()
        {
            bool show = true;
            while (show)
                show = Menu();
        }

        private static string[] menuItems =
        {
            "Add a to b (a + b)",
            "Divide a by b (a / b)",
            "Subtract b from a (a - b)",
            "Multiply a with b (a * b)",
            "Take to power of b to a (a ^ b)",
            "Take the b root of a (a ^ (1 / b))",
            "Take modulo b of a (a % b)"
        };

        private static bool Menu()
        {
            WriteLine("[Lexicon C#/.Net Programming] Assignment 1 - Calculator \n");

            Write("Choose what to calculate:\n" +
                $"1)  {menuItems[0]}\n" +
                $"2)  {menuItems[1]}\n" +
                $"3)  {menuItems[2]}\n" +
                $"4)  {menuItems[3]}\n" +
                $"5)  {menuItems[4]}\n" +
                $"6)  {menuItems[5]}\n" +
                $"7)  {menuItems[6]}\n" +
                "0)  Exit\n" +
                "> "
            );

            string input = ReadLine();
            Clear();
            return input switch
            {
                "0" => false,
                "1" => HandleOperation(menuItems[0], Add),
                "2" => HandleOperation(menuItems[1], Divide),
                "3" => HandleOperation(menuItems[2], Subtract),
                "4" => HandleOperation(menuItems[3], Multiply),
                "5" => HandleOperation(menuItems[4], Power),
                "6" => HandleOperation(menuItems[5], Root),
                "7" => HandleOperation(menuItems[6], Modulo),
                _ => true,
            };
        }

        private static bool HandleOperation(string operation, Func<double, double, double> MathOp)
        {
            Console.WriteLine(operation);
            double a = ReadNumber<double>("Input a", "a must be a number");
            double b = ReadNumber<double>("Input b", "b must be a number");
            double result;
            try
            {
                result = MathOp(a, b);
                WriteLine("=> " + Math.Round(MathOp(a, b), 4).ToString());
            }
            catch (Exception ex)
            {
                if (ex is DivideByZeroException)
                {
                    WriteLine("Cannot divide by Zero.");
                } else
                {
                    WriteLine(ex.ToString());
                }
            }

            return HoldForInput();
        }

        private static bool HoldForInput(string msg = "\n\tPress any key to continue...")
        {
            Write(msg);
            ReadKey();
            Clear();
            return true;
        }

        private static T ReadNumber<T>(
            string msg = "Input a number",
            string err = "You must input a number"
            )
        {
            T num;
            string input;

            TypeConverter converter = TypeDescriptor.GetConverter(typeof(T));

            Write($"{msg}\n> ");
            while (true)
            {
                input = ReadLine();
                try
                {
                    num = (T)converter.ConvertFromString(input);
                    return num;
                }
                catch
                {
                    Write($"{err}\n> ");
                }
            }
        }
    }
}
