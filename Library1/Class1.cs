using System;

namespace Library1
{
    public class Class1
    {
        public static string Mylibrary()
        {
            return "Это моя библиотека и она выполняет несколько математических опереации!";
        }
    }

    public class LeapYear
    {
        public static bool isLeap(int year)
        {
            return year % 4 == 0 && (year % 100 != 0 || year % 400 == 0);
        }
    }

    public class MathOperat
    {
        public static int Summ(int a, int b, int c)
        {
            return a + b + c;
        }

        public static double Summ(double a, double b, double c)
        {
            return a + b + c;
        }

        public static int Maxx(int a, int b, int c)
        {
            int max = b > a ? b : a;
            return max > c ? max : c;
        }
        public static double Maxx(double a, double b, double c)
        {
            double max = b > a ? b : a;
            return max > c ? max : c;
        }

        public static int Minn(int a, int b, int c)
        {
            int min = b < a ? b : a;
            return min < c ? min : c;
        }
        public static double Minn(double a, double b, double c)
        {
            double min = b < a ? b : a;
            return min < c ? min : c;
        }



        public static long Factorial(int num)
        {
            if (num == 0)
                return 1;
            else
                return num * Factorial(num - 1);
        }

        public static bool prime(int num)
        {
            for (int i = 2; i < num; i++)
            {
                if (num % i == 0)
                    return false;
            }
            return true;
        }
        public static bool even(int num)
        {
            return num % 2 == 0;
        }

        public static bool odd(int num)
        {
            return num % 2 != 0;
        }
    }
    public class Fraction
    {
        public int num, den;

        public Fraction(int numerator, int denominator)
        {
            num = numerator;
            den = denominator;
        }

        public static Fraction operator +(Fraction a, Fraction b) =>
            new Fraction(a.num * b.den + b.num * a.den, a.den * b.den);

        public static Fraction operator -(Fraction a, Fraction b)
       => new Fraction(a.num * b.den - b.num * a.den, a.den * b.den);

        public static Fraction operator /(Fraction a, Fraction b) =>
             new Fraction(a.num * b.den, a.den * b.num);

        public static Fraction operator *(Fraction a, Fraction b)
       => new Fraction(a.num * b.num, a.den * b.den);

        public override string ToString()
        {
            return $"{num}/{den}";
        }
    }


}
