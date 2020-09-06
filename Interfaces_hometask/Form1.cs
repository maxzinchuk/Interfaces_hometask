using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Interfaces_hometask
{
    public partial class Form1 : Form
    {
        ArrayList clients;
        int selectedRow;

        public int getByID(int id)
        {
            for (int i = 0; i < clients.Count; i++)
            {
                if (((Person)clients[i]).Id == id)
                    return i;
            }

            return -1;
        }

        public Form1()
        {
            InitializeComponent();
            clients = new ArrayList();
        }

        private void HScrollBar1_ValueChanged(object sender, EventArgs e)
        {
            this.Text = hScrollBar1.Value.ToString();
        }
        private void save(string path)
        {
            StreamWriter sw = new StreamWriter(path);
            sw.Flush();
            for (int i = 0; i < dataGridView1.Rows.Count; i++)
            {
                for (int j = 0; j < dataGridView1.Rows[i].Cells.Count; j++)
                {
                    sw.WriteLine(dataGridView1.Rows[i].Cells[j].Value);
                }
            }
            sw.Close();
        }
        private void load(string path)
        {
            StreamReader sr = new StreamReader(path, true);
            string line;
            int length = 0;
            dataGridView1.Rows.Clear();
            while ((line = sr.ReadLine()) != null) {
                if (length % 3 == 0)
                {
                    dataGridView1.Rows.Add(1);

                }

                dataGridView1.Rows[(int)(length / 3)].Cells[length % 3].Value = line;

                length++;
            }
            //dataGridView1.Rows.RemoveAt(dataGridView1.Rows.Count-1);

            sr.Close();
        }
        private void Button1_Click(object sender, EventArgs e)
        {
            string banned = "1234567890!@#$%^&*()_+|-=?/.,[]{}: ~` ";
            try
            {
                bool ban = false;
                foreach (char item in banned)
                {
                    if (textBox1.Text.Contains(item)) ban = true;
                }
                if (ban)
                {
                    textBox1.Text = "name";
                    return;
                }
            }
            catch (Exception)
            {
                return;
            }

            clients.Add(new Person(clients.Count, textBox1.Text, hScrollBar1.Value));
            dataGridView1.Rows.Add();
            dataGridView1.Rows[clients.Count-1].Cells[0].Value = ((Person)clients[clients.Count - 1]).Id.ToString();
            dataGridView1.Rows[clients.Count-1].Cells[1].Value = ((Person)clients[clients.Count - 1]).Name.ToString();
            dataGridView1.Rows[clients.Count-1].Cells[2].Value = ((Person)clients[clients.Count - 1]).Age.ToString();
        }

        private void DataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
      

        }

        private void DataGridView1_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            
        }

        private void DataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            // MessageBox.Show(e.RowIndex + ", " + e.ColumnIndex);
            try
            {
                int index = getByID(Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells[0].Value));
                selectedRow = e.RowIndex;
                textBox1.Text = ((Person)clients[index]).Name;
                hScrollBar1.Value = ((Person)clients[index]).Age;
            }
            catch (Exception)
            {

            }
            

        }

        private void Button2_Click(object sender, EventArgs e)
        {
            try
            {
                int index = getByID(Convert.ToInt32(dataGridView1.Rows[selectedRow].Cells[0].Value));
                ((Person)(clients[index])).Name = textBox1.Text;
                ((Person)(clients[index])).Age = hScrollBar1.Value;
                Refresh();
            }
            catch (Exception)
            {

            }

        }

        public void Refresh()
        {
            for (int i = 0; i < clients.Count; i++)
            {
                dataGridView1.Rows[i].Cells[0].Value = ((Person)clients[i]).Id.ToString();
                dataGridView1.Rows[i].Cells[1].Value = ((Person)clients[i]).Name.ToString();
                dataGridView1.Rows[i].Cells[2].Value = ((Person)clients[i]).Age.ToString();
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            save("base.txt");
        }

        private void button4_Click(object sender, EventArgs e)
        {
            load("base.txt");
        }
    }
}
