using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace aknakereső
{
    internal class Program
    {
        public static List<List<int>> matrix = new List<List<int>>();
        public static int szel = 0;
        public static int mag = 0;
        public static int aknaszam = 0;

        static void Main(string[] args)
        {
            Console.Clear();
            ParameterBekeres();
            MatrixFeltolt(szel, mag);
            MatrixGeneral();
            MezoCheck();
            MatrixAbrazol();
            LepesBeker();

            Console.ReadKey();
        }

        private static void JatekVege(bool nyert)
        {
            if(nyert)
            {
                Console.Clear();
                Console.WriteLine("GG");
                Console.ReadKey();
            }
            else 
            {
                Console.Clear();
                Console.WriteLine("gg");
                Console.ReadKey();
            }
        }   

        private static void LepesBeker()
        {
            bool jatekTart = true;
            while (jatekTart)
            {
                Console.WriteLine();

                Console.Write("X?: ");
                int x = Convert.ToInt32(Console.ReadLine()) - 1;
                Console.WriteLine();
                Console.Write("Y?: ");
                int y = Convert.ToInt32(Console.ReadLine()) - 1;

                if (matrix[x][y] == -1)
                {
                    JatekVege(false);
                    jatekTart = false;
                }
                else
                {
                    xek.Add(x);
                    yok.Add(y);
                    MezokMegjelenit();
                }
                Console.WriteLine();
            }
        }

        private static List<int> xek = new List<int>();
        private static List<int> yok = new List<int>();

        private static void MezokMegjelenit()
        {

            for (int i = 0; i < xek.Count; i++)
            {
                CellaFelfed(xek[i], yok[i]);
            }
            
            

        }

        private static void CellaFelfed(int x, int y)
        {
            Console.BackgroundColor = ConsoleColor.White;
            Console.ForegroundColor = ConsoleColor.Black;
            Console.SetCursorPosition(x * 4, y); //mert 4 széles egy mező
            Console.Write($"{matrix[x][y],3} ");
            Console.ResetColor();
        }


        private static void ParameterBekeres()
        {
            Console.Write("Szélesség: ");
            szel = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine();
            Console.Write("Magasság: ");
            mag = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine();
            Console.Write("Aknaszám: ");
            aknaszam = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine();
           
        }

        private static void MatrixFeltolt(int szel, int mag)
        {
            matrix.Clear();  

            for (int i = 0; i < mag; i++)  
            {
                List<int> temp = new List<int>(); 
                for (int j = 0; j < szel; j++)  
                {
                    temp.Add(0);
                }
                matrix.Add(temp);  
            }
        }

        private static void MatrixAbrazol()
        {
            Console.Clear();
            //Console.BackgroundColor = ConsoleColor.White;
            //Console.ForegroundColor = ConsoleColor.White;
            for (int i = 0; i < matrix.Count; i++)
            {
                for (int j = 0; j < matrix[i].Count; j++)
                {
                    Console.Write($"{matrix[i][j],3} "); //ne bassza szét a -1
                }
                Console.WriteLine();
            }
            //Console.ResetColor();
        }

        private static void MatrixGeneral()
        {
            Random rnd = new Random();
            HashSet<(int, int)> lerakottAknak = new HashSet<(int, int)>();

            int aknaszam_temp = 0;

            while (aknaszam_temp < aknaszam)
            {

                int x = rnd.Next(0, szel);
                int y = rnd.Next(0, mag);

                if (matrix[x][y] != -1)
                {
                    matrix[x][y] = -1;
                    lerakottAknak.Add((x, y));
                    aknaszam_temp++;
                }
            }
        }
        private static void MezoCheck()
        {

            for (int i = 0; i < szel; i++)
            {
                for (int j = 0; j < mag; j++)
                {
                    if (matrix[i][j] != -1)  
                    {
                        matrix[i][j] = AknaSzamolas(i,j);
                    }
                }
            }
        }
        private static int AknaSzamolas(int i, int j)
        {
            int n = 0;

            for (int x = -1; x <= 1; x++)  // x: -1, 0, 1 
            {
                for (int y = -1; y <= 1; y++)  // y: -1, 0, 1 
                {
                    if (x == 0 && y == 0)
                    {
                        continue;

                    }
                    int X = i + x;
                    int Y = j + y;

                    if (X >= 0 && X < szel && Y >= 0 && Y < mag) //ne fusson ki 
                    {
                        if (matrix[X][Y] == -1)
                        {
                            n++;
                        }
                    }
                }
            }
            return n;
        }
    }
}
