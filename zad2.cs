using static System.Console;
using System;
using System.Linq;
using System.Collections.Generic;
namespace Metody_Programowania_2021_zad2
{
    class Program
    {
        public static void Main()
        {
            //Write("Podaj ciąg tekstowy do zakodowania : ");
            //string wejście = ReadLine();
            string wejście = "PUDEL";
            string wyjście = Huffman(wejście);
            WriteLine("Zakodowany tekst : {0}", wyjście);
            WriteLine("Długość przed zakodowaniem : {0} bitów ,Długość po zakodowaniu {1} bitów", wejście.Length * 8, wyjście.Length);
            ReadKey();
        }
        static string Huffman(string ciąg_znaków)
        {
            char[] tempArr = ciąg_znaków.ToCharArray();
            double[] wystąpienia = new double[tempArr.Distinct().Count()];//liczy ile różnych znaków jest w tekscie
            char[] znaki = new char[wystąpienia.Length];
            int rozmiartab = znaki.Length;
            for (int i = 0; i < rozmiartab; i++)//liczy wystąpienia poszczególnych znaków w ciągu
            {
                znaki[i] = tempArr[0];
                for (int j = 0; j < tempArr.Length; j++) if (znaki[i] == tempArr[j]) wystąpienia[i]++;
                tempArr = Array.FindAll(tempArr, x => x != znaki[i]);//pozbywa się już zaindeksowanych znaków
            }
            wystąpienia[0] = 3.01;//Modyfikacja kodów dla tekstu "PUDEL"
            wystąpienia[1] = 1.92;
            wystąpienia[2] = 3.45;
            wystąpienia[3] = 7.64;
            wystąpienia[4] = 2.09;
        Array.Sort(wystąpienia, znaki);//sortowanie względem liczby wystąpień
            List<Node> BinaryTree = new List<Node>();
            for (int i = 0; i < rozmiartab; i++) BinaryTree.Add(new Node(znaki[i], wystąpienia[i]));
            //tworzy listę zawierającą drzewa binarne z poszczególnych znaków i ich częstotliwości
            int temp = rozmiartab;
            Node tempNode;
            if (temp == 1)//zabezpieczenie, gdyby tekst składał się z tylko jednego znaku
            {
                tempNode = new Node((char)0, BinaryTree[0].wystąpienia);
                tempNode.lewo = BinaryTree[0];
                BinaryTree[0] = tempNode;
            }
            while (--temp > 0)//tworzy drzewo Huffmana z poszczególnych drzew w formie listy
            {
                tempNode = new Node((char)0, BinaryTree[0].wystąpienia + BinaryTree[1].wystąpienia);
                tempNode.lewo = BinaryTree[0];
                tempNode.prawo = BinaryTree[1];
                BinaryTree[0] = tempNode;
                BinaryTree.RemoveAt(1);
                BinaryTree.Sort((node1, node2) => node1.wystąpienia.CompareTo(node2.wystąpienia));
                //lista umożliwia sortowanie wedlug własności obiektów
            }
            string[] TablicaKodów = new string[rozmiartab];
            int poz = 0;//zmienna potrzebna do pilnowania pozycji w funkcji DDT
            DrzewoDoTablicy(BinaryTree[0], znaki, TablicaKodów, ref poz);
            return KodowanieTekstu(ciąg_znaków, znaki, TablicaKodów);
        }
        static void DrzewoDoTablicy(Node węzeł, char[] TabZnaków, string[] TabKodów, ref int poz, string code = "")
        {//Zmienia drzewo na tablicę kodów,rekurencyjnie przechodzi przez drzewo
            bool flag = węzeł.znak != (char)0;
            if (flag)
            {
                TabZnaków[poz] = węzeł.znak;
                TabKodów[poz++] = code;
                WriteLine("Znak :{0} Kod :{1}", węzeł.znak, code);
            }
            if (węzeł.lewo != null) DrzewoDoTablicy(węzeł.lewo, TabZnaków, TabKodów, ref poz, code + (!flag ? "0" : ""));
            if (węzeł.prawo != null) DrzewoDoTablicy(węzeł.prawo, TabZnaków, TabKodów, ref poz, code + (!flag ? "1" : ""));
        }
        static string KodowanieTekstu(string tekst, char[] TablicaZnaków, string[] TablicaKodów)
        {
            string wyjście = "";
            for (int i = 0; i < tekst.Length; i++) wyjście += TablicaKodów[Array.IndexOf(TablicaZnaków, tekst[i])];
            return wyjście;
        }
    }
    class Node//węzeł potrzebny do stworzenia drzewa binarnego 
    {
        public char znak;
        public double wystąpienia;
        public Node lewo, prawo;
        public Node(char zn, double wys)
        {
            znak = zn;
            wystąpienia = wys;
            lewo = prawo = null;
        }
    }
}