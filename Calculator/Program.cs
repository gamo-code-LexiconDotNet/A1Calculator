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

        /**
         * Menu loop
         */
        private static void Run()
        {
            bool show = true;
            while (show)
                show = Menu();
        }

        /**
         * Menu intems
         */
        private static readonly string[] menuItems =
        {
            "Add a to b (a + b)",                   //1
            "Divide a by b (a / b)",                //2
            "Subtract b from a (a - b)",            //3
            "Multiply a with b (a * b)",            //4
            "Take to power of b to a (a ^ b)",      //5
            "Take the b root of a (a ^ (1 / b))",   //6
            "Take modulo b of a (a % b)"            //7
        };

        /**
         * Menu
         */
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

        /**
         * Handles the calls fom menu
         * Read input
         * Try math operation and write output
         * Handle exceptions
         */
        private static bool HandleOperation(string message, Func<double, double, double> mathOperation)
        {
            WriteLine(message);
            double a = ReadNumber<double>("Input a", "a must be a number");
            double b = ReadNumber<double>("Input b", "b must be a number");
            
            try
            {
                WriteLine("=> " + Math.Round(mathOperation(a, b), 4).ToString());
            }
            catch (Exception ex)
            {
                if (ex is DivideByZeroException)
                    WriteLine("Cannot divide by Zero.");
                else
                    WriteLine(ex.ToString());
            }

            return HoldAndClear();
        }

        /**
         * Hold for key and clear console
         */
        private static bool HoldAndClear(string message = "\n\tPress any key to continue...")
        {
            Write(message);
            ReadKey();
            Clear();
            return true;
        }

        /**
         * Read number from input
         * Templated for different types
         * Asks for new input if input of wrong type (convert fails)
         */
        private static T ReadNumber<T>(
            string message = "Input a number", 
            string error = "You must input a number")
        {
            TypeConverter converter = TypeDescriptor.GetConverter(typeof(T));

            Write($"{message}\n> ");
            while (true)
                try 
                { 
                    return (T)converter.ConvertFromString(ReadLine()); 
                }
                catch 
                {
                    Write($"{error}\n> ");
                }
        }
    }
}
