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

            ParameterBekeres();

            MatrixFeltolt(szel,mag);
            MatrixGeneral();
            MatrixAbrazol();
            Console.ReadKey();
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
            List<int> temp = new List<int>();
            for (int i = 0; i < szel; i++)
            {
                temp.Add(0);
            }
            for (int i = 0; i < mag; i++)
            { 
                matrix.Add(temp);
            }
        }

        private static void MatrixAbrazol()
        {
            for (int i = 0; i < matrix.Count(); i++)
            {
                for (int j = 0; j < matrix[0].Count(); j++)
                {
                    Console.Write(matrix[i][j] +"|");
                }
                Console.WriteLine();
            }
        }

        private static void MatrixGeneral()
        {
            Random rnd = new Random();
            for (int i = 0; i <= aknaszam; i++)
            {
                
                int x = rnd.Next(1, szel);
                int y = rnd.Next(1, mag);

                matrix[x][y] = -1;
            }
        }
    }
}
