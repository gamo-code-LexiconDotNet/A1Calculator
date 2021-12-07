using System;
using System.Collections.Generic;
using System.Text;

namespace ReplCalculator
{
    class Calculator
    {
        /**
         * Evaluate an inifix notation expression
         * 
         * Algorithm from https://www.geeksforgeeks.org/expression-evaluation/
         *  
         * Added support for:
         *  floating point values
         *  power of
         *  modulus
         *  pi
         */
        public static double EvaluateInifix(string expression)
        {
            Stack<double> values = new Stack<double>();
            Stack<char> ops = new Stack<char>();
            
            char[] tokens = expression.ToCharArray();

            for (int i = 0; i < tokens.Length; i++)
            {
                if (tokens[i] == ' ')
                    continue;

                if (tokens[i] == 'p' && tokens[i + 1] == 'i')
                {
                    values.Push(Math.PI);
                    i++;
                }


                if ((tokens[i] >= '0' && tokens[i] <= '9') ||
                    tokens[i] == '.')
                {
                    StringBuilder sbuf = new StringBuilder();

                    while (i < tokens.Length &&
                        ((tokens[i] >= '0' && tokens[i] <= '9') ||
                        tokens[i] == '.'))
                    {
                        sbuf.Append(tokens[i++]);
                    }
                    values.Push(double.Parse(sbuf.ToString()));

                    i--;
                }
                else if (tokens[i] == '(')
                {
                    ops.Push(tokens[i]);
                }
                else if (tokens[i] == ')')
                {
                    while (ops.Peek() != '(')
                    {
                        values.Push(ApplyOp(ops.Pop(),
                                         values.Pop(),
                                        values.Pop()));
                    }
                    ops.Pop();
                }
                else if (tokens[i] == '+' || tokens[i] == '-' ||
                    tokens[i] == '*' || tokens[i] == '/' ||
                    tokens[i] == '^' || tokens[i] == '%')
                {
                    while (ops.Count > 0 && 
                        HasPrecedence(tokens[i], 
                        ops.Peek()))
                    {
                        values.Push(ApplyOp(ops.Pop(),
                                         values.Pop(),
                                       values.Pop()));
                    }

                    ops.Push(tokens[i]);
                }
            }

            while (ops.Count > 0)
                values.Push(ApplyOp(ops.Pop(), values.Pop(), values.Pop()));

            return values.Pop();
        }

        /**
         * Included ^ and % in presidence order
         * Order: ^, [*, /], [+, -], %, [(, )]
         */
        public static bool HasPrecedence(char op1, char op2)
        {
            // handle (, )
            if (op2 == '(' || op2 == ')')
                return false;

            // handle %
            if ((op2 == '%'))
                return false;

            //handle +, -
            if ((op2 == '+' || op2 == '-') && (op1 == '*' || op1 == '/' || op1 == '^'))
                return false;

            //handle *, /
            if ((op2 == '*' || op2 == '/') && (op1 == '^'))
                return false;

            // implicit true for all other cases
            return true;
        }

        /**
         * Added support for ^ and %
         */
        public static double ApplyOp(char op, double b, double a)
        {
            switch (op)
            {
                case '+':
                    return a + b;
                case '-':
                    return a - b;
                case '*':
                    return a * b;
                case '/':
                    if (b == 0)
                    {
                        throw new
                        System.NotSupportedException(
                               "Cannot divide by zero");
                    }
                    return a / b;
                case '^':
                    return Math.Pow(a, b);
                case '%':
                    return a % b;
            }
            return 0;
        }
    }
}

