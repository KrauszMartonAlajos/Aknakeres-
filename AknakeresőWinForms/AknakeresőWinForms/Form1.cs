using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AknakeresőWinForms
{
    public partial class Form1 : Form
    {
        public static int meret = 0;
        public static int aknaszam = 0;
        public static List<List<int>> matrix = new List<List<int>>();
        public Form1()
        {
            InitializeComponent();
            dataGridView1.CellClick += DataGridView1_CellClick;
        }

        private void numericUpDown2_ValueChanged(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
        }
        private void DataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            NyertE();
            int x = e.RowIndex;
            int y = e.ColumnIndex;
            if (matrix[x][y] == -1)
            {
                MessageBox.Show("Vesztett!");
                this.Close();
                return;
            }
            MezokFelfed(x, y);
            NyertE();
        }

        private void NyertE()
        {
            int db = 0;
            foreach (DataGridViewRow sor in dataGridView1.Rows)
            {
                foreach (DataGridViewCell cell in sor.Cells)
                {
                    if (cell.Value != null && cell.Value.ToString() == "X")
                    {
                        db++;
                    }
                }
            }
            if (aknaszam == db)
            {
                MessageBox.Show("GG");
                this.Close();
            }
        }
        public static int db = 0;
        private void MezokFelfed(int x, int y)
        {
            if (matrix[x][y] != 0)
            {
                dataGridView1.Rows[x].Cells[y].Value = matrix[x][y];
            }
            else 
            {
                dataGridView1.Rows[x].Cells[y].Value = matrix[x][y];
                int[,] iranyok = {
                { -1, -1 },
                { -1, 0 },
                { -1, 1 },
                { 0, -1 },
                { 0, 1 },
                { 1, -1 },
                { 1, 0 },
                { 1, 1 },
                { 0,0}
                };

                for (int i = 0; i < iranyok.GetLength(0); i++)
                {
                    int X = x + iranyok[i, 0];
                    int Y = y + iranyok[i, 1];
                    if (X >= 0 && X < matrix.Count && Y >= 0 && Y < matrix[X].Count) //h ne fusson ki :,)
                    {
                        if (matrix[X][Y] != -1)
                        {
                            if (dataGridView1.Rows[X].Cells[Y].Value.ToString() == "X")
                            {
                                MezokFelfed(X, Y);
                                
                            }
                            dataGridView1.Rows[X].Cells[Y].Value = matrix[X][Y];
                                                    
                        }
                    }
                }
            }
        }




        private void button1_Click(object sender, EventArgs e)
        {
            meret = Convert.ToInt32(numericUpDown1.Value);
            aknaszam = Convert.ToInt32(numericUpDown2.Value);
            MatrixFeltolt(meret, meret);
            MatrixGeneral();
            MezoCheck();
            MatrixAbrazol();
        }

        private void MatrixAbrazol()
        {
            dataGridView1.Rows.Clear();
            dataGridView1.Columns.Clear();

            for (int i = 0; i < matrix[0].Count; i++)
            {
                dataGridView1.Columns.Add($"{i}", $"{i + 1}");
            }

            for (int i = 0; i < matrix.Count; i++)
            {
                var sor = new DataGridViewRow();
                sor.CreateCells(dataGridView1);

                for (int j = 0; j < matrix[i].Count; j++)
                {
                    sor.Cells[j].Value = "X";//matrix[i][j];
                }

                dataGridView1.Rows.Add(sor);
            }
            int cellameret = 30;

            foreach (DataGridViewColumn column in dataGridView1.Columns)
            {
                column.Width = cellameret;
            }

            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                row.Height = cellameret;
            }
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

        private static void MatrixGeneral()
        {
            Random rnd = new Random();
            HashSet<(int, int)> lerakottAknak = new HashSet<(int, int)>();

            int aknaszam_temp = 0;

            while (aknaszam_temp < aknaszam)
            {

                int x = rnd.Next(0, meret);
                int y = rnd.Next(0, meret);

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

            for (int i = 0; i < meret; i++)
            {
                for (int j = 0; j < meret; j++)
                {
                    if (matrix[i][j] != -1)
                    {
                        matrix[i][j] = AknaSzamolas(i, j);
                    }
                }
            }
        }
        private static int AknaSzamolas(int i, int j)
        {
            int n = 0;

            for (int x = -1; x <= 1; x++)  // x: bal jobb
            {
                for (int y = -1; y <= 1; y++)  // y: fel le
                {
                    if (x == 0 && y == 0)
                    {
                        continue;
                    }
                    int X = i + x;
                    int Y = j + y;

                    if (X >= 0 && X < meret && Y >= 0 && Y < meret) //ne fusson ki 
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
