using System;

namespace Lexicon
{
    class Calculator
    {
        public static double Add(double a, double b)
        {
            return a + b;
        }

        public static double Subtract(double a, double b)
        {
            return a - b;
        }

        public static double Multiply(double a, double b)
        {
            return a * b;
        }

        public static double Divide(double a, double b)
        {
            if (b == 0)
                throw new DivideByZeroException();
            return a / b;
        }

        public static double Modulo(double a, double b)
        {
            return a % b;
        }

        public static double Power(double a, double b)
        {
            return Math.Pow(a, b);
        }

        public static double Root(double a, double b)
        {
            if (b == 0)
                throw new DivideByZeroException();
            return Math.Pow(a, 1 / b);
        }
    }
}
