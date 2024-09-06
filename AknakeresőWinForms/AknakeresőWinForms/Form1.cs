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
            this.Resize += new EventHandler(Form_Resize);
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
            palyaSzinez();
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
            palyaSzinez();
            this.dataGridView1.ClearSelection();
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
            if (Convert.ToInt32(numericUpDown1.Value) * Convert.ToInt32(numericUpDown1.Value) < Convert.ToInt32(numericUpDown2.Value))
            {
                MessageBox.Show("Túl sok akna és túl kicsi pálya!");
            }
            else if (Convert.ToInt32(numericUpDown1.Value) == 0 || Convert.ToInt32(numericUpDown2.Value) == 0)
            {
                MessageBox.Show("Adjon meg értékeket!");
            }
            else
            {
                meret = Convert.ToInt32(numericUpDown1.Value);
                aknaszam = Convert.ToInt32(numericUpDown2.Value);
                MatrixFeltolt(meret, meret);
                MatrixGeneral();
                MezoCheck();
                MatrixAbrazol();
                palyaSzinez();
            }            
        }

        private void palyaSzinez()
        {
            for (int i = 0; i < dataGridView1.Rows.Count; i++)
            {
                for (int j = 0; j < dataGridView1.Columns.Count; j++)
                {
                    var hehe = dataGridView1.Rows[i].Cells[j];
                    if (hehe.Value != null && hehe.Value.ToString() == "X")
                    {
                        hehe.Style.BackColor = Color.Green;
                    }
                    else if (hehe.Value != null && hehe.Value.ToString() == "0")
                    {
                        hehe.Style.BackColor = Color.SandyBrown;
                    }
                    else if (hehe.Value != null && Convert.ToInt32(hehe.Value) > 0)
                    {
                        hehe.Style.BackColor = Color.Brown;
                    }
                }
            }
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

        private void Form1_Load(object sender, EventArgs e)
        {
            CenterPanel();
        }
        private void CenterPanel()
        {
            int szel = panel1.Width;
            int mag = panel1.Height;

            int x = (this.ClientSize.Width - szel) / 2;
            int y = (this.ClientSize.Height - mag) / 2;

            panel1.Location = new System.Drawing.Point(x, y);
        }
        private void Form_Resize(object sender, EventArgs e)
        {
            CenterPanel();
        }
    }
}
