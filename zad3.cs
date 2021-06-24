using System;
namespace Metody_Programowania_2021_zad3
{
    class Program
    {
        public static void Main()
        {

            double a;
            do
            {
                Console.Write("Podaj liczbę do spotęgowania : ");
                double.TryParse(Console.ReadLine(), out a);
            }
            while (!(a < 10));//sprawdza czy a spełnia warunek "a<10"
            for (int i = 2; i <= 8;)
            {
                double wynik = power(a, i);
                Console.WriteLine("{0} do potęgi {1} : {2}", a, i++, wynik);
            }
            Console.ReadKey();
        }
        static double power(double x, int n, double y = 0)
        {
            if (y == 0) y = x;
            return (n == 1) ? x : power(x * y, n - 1, y);
        }
    }
}
