using System;
using System.Collections;

namespace TrapperSimpsonGausse
{
    internal class Program
    {
        #region Function and Derivative
        static double F(double x)
        {
            double output = Math.Sqrt(x) * Math.Exp(-x);
            return output;
        }
        static double P1(double x)
        {
            double output = -((2 * x - 1) * Math.Exp(-x)) / (2 * Math.Sqrt(x));
            return output;
        }

        static double P2(double x)
        {
            double output = ((4 * Math.Pow(x, 2) - 4 * x - 1) * Math.Exp(-x)) / (4 * Math.Pow(x, (1.5)));
            return output;
        }

        static double P3(double x)
        {
            double output = -((8 * Math.Pow(x, 3) - 12 * Math.Pow(x, 2) - 6 * x - 3) * Math.Exp(-x)) / (8 * Math.Pow(x, 2.5));
            return output;
        }
        static double P4(double x)
        {
            double output = ((16 * Math.Pow(x, 4) - 32 * Math.Pow(x, 3) - 24 * Math.Pow(x, 2) - 24 * x - 15) * Math.Exp(-x)) / (16 * Math.Pow(x, (3.5)));
            return output;
        } 
        #endregion

        static void Main(string[] args)
        {
            Trapper(); Console.WriteLine("\t");
            Simpson(); Console.WriteLine("\t");
            Gausse(); Console.WriteLine("\t");
        }

        static void Trapper()
        {
            double a = 0.1;
            double b = 1.1;
            double h = 0.1;
            double maxP2 = 0;
            double sumF = 0;

            for (; a <= b; a += h)
            {
                sumF += F(a);
                if (Math.Abs(P2(a)) > maxP2)
                    maxP2 = Math.Abs(P2(a));
            }

            a = 0.1;
            sumF -= (F(a) + F(b));

            double r = (maxP2 * Math.Pow(h, 2) * Math.Abs(b - a)) / 12;

            double answer = h * ((F(b) + F(a)) / 2 + sumF);

            Console.WriteLine($"Метод Трапеций\nИнтегралл равен: {Math.Round(answer, 4)} +- {Math.Round(r, 4)}");
        }

        static void Simpson()
        {
            double a = 0.1;
            double b = 1.1;
            double h = 0.1;
            double maxP4 = 0;
            double evenSum = 0;
            double unevenSum = 0;

            for (int i = 0; a <= b; a += h, i++)
            {
                if (Math.Abs(P4(a)) > maxP4)
                    maxP4 = Math.Abs(P4(a));

                if (i % 2 == 0 && i != 0 && F(a) != F(b))
                    evenSum += F(a);

                if (i % 2 != 0 && a != b)
                    unevenSum += F(a);

            }

            a = 0.1;
            double answer = h / 3 * (F(a) + F(b) + 4 * unevenSum + 2 * evenSum);

            double r = Math.Abs(-maxP4 * Math.Pow(h, 4) * ((F(b) - F(a)) / 2));

            Console.WriteLine($"Метод Симпсона\nИнтегралл равен: {Math.Round(answer, 5)} -+ {Math.Round(r, 5)}");
        }

        static void Gausse()
        {
            double a = 0.1;
            double b = 1.1;
            double h = 0.1;
            double maxP;
            double sumF = 0;

            ArrayList array = new ArrayList();
            for (; a < b - h; a += h)
            {
                double middleX1 = a + h / 2 - h / (2 * Math.Sqrt(3));
                double middleX2 = a + h / 2 + h / (2 * Math.Sqrt(3));
                sumF += F(middleX1);
                sumF += F(middleX2);

                array.Add(Math.Abs(P4(a)));
                array.Add(Math.Abs(P4(middleX1)));
                array.Add(Math.Abs(P4(middleX2)));
            }
            array.Sort();
            maxP = (double)array[^1];
            array.Clear();

            a = 0.1;
            double r = (maxP * Math.Pow((b - a), 7)) / 2016000;
            double answer = sumF * h / 2;
            Console.WriteLine($"Метод Гаусса\nИнтегралл равен: {Math.Round(answer, 5)} +- {Math.Round(r, 5)}");
        }

    }
}
