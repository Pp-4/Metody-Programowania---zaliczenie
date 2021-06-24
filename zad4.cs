using System;
namespace Metody_Programowania_2021_zad4
{
    class Program
    {
        public static void Main()
        {
            Console.WriteLine("Podawaj kolejene ciągi tekstowe \naby zakończyć podawanie, podaj pusty ciag.");
            string temp, str = "";
            int count = 0;
            char c = (char)1;
            do
            {
                count++;
                Console.Write("Podaj ciąg {0} : ", count);
                temp = Console.ReadLine();
                str += temp + c;
            } while (temp.Length != 0);
            string[] strtab = str.Split(c);
            string[] wynik = new string[2];
            if (strtab.Length < 3) wynik[0] = wynik[1] = strtab[0];
            else wynik = MinMaxFunc(strtab, 0, strtab.Length - 3);
            Console.WriteLine("Najdłuższy ciąg : {0}, Najkrótszy ciąg : {1}",wynik[0], wynik[1]);
            Console.ReadKey();
        }
        static string[] MinMaxFunc(string[] arr, int start, int end)
        {
            string max, min;
            if (start == end) max = min = arr[start];
            else if (start + 1 == end)
            {
                if (arr[start].Length < arr[end].Length)
                {
                    max = arr[end];
                    min = arr[start];
                }
                else
                {
                    max = arr[start];
                    min = arr[end];
                }
            }
            else
            {
                int mid = start + (end - start) / 2;
                string[] left = MinMaxFunc(arr, start, mid);
                string[] right = MinMaxFunc(arr, mid + 1, end);
                if (left[0].Length > right[0].Length) max = left[0];
                else max = right[0];
                if (left[1].Length < right[1].Length) min = left[1];
                else min = right[1];
            }
            string[] wynik = { max, min };
            return wynik;
        }
    }
}