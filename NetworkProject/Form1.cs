using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NetworkProject
{
    public partial class Form1 : Form
    {
        public List<string[]> data = new List<string[]>();

        int[] Search(string str1, int index1, string str2, int index2, string str3, int index3, int[] arr)
        {
            index1++;
            index2++;
            index3++;

            for (int i = 0; i < data.Count; i++)
            {
                if (data[i][index1].ToString().Contains(str1) && data[i][index2].ToString().Contains(str2) && data[i][index3].ToString().Contains(str3))
                {
                    Array.Resize(ref arr, arr.Length+1);
                    arr[arr.Length-1] = i;
                }
            }

            return arr;
        }

        public int SearchIndex(List<string[]> list, string str)
        {
            //foreach()
            for (int i = 0; i < list.Count; i++)
                if (str == data[i][2])
                    return i;
            return -1;
        }

        void ReadDataBase()
        {
            SqlConnection myConnection = new SqlConnection(@"Data Source=DESKTOP-J5GHJ7T\SQLEXPRESS; Initial Catalog = Biblio; Integrated Security=SSPI;");
                myConnection.Open();

                string query = "SELECT * FROM Offers ORDER BY id";

                SqlCommand command = new SqlCommand(query, myConnection);
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    data.Add(new string[10]);

                    data[data.Count - 1][0] = reader[0].ToString();
                    data[data.Count - 1][1] = reader[1].ToString();
                    data[data.Count - 1][2] = reader[2].ToString();
                    data[data.Count - 1][3] = reader[3].ToString();
                    data[data.Count - 1][4] = reader[4].ToString();
                    data[data.Count - 1][5] = reader[5].ToString();
                    data[data.Count - 1][6] = reader[6].ToString();
                    data[data.Count - 1][7] = reader[7].ToString();
                    data[data.Count - 1][8] = reader[8].ToString();
                    data[data.Count - 1][9] = reader[9].ToString();
            }

                reader.Close();
                myConnection.Close();
        }

        public void FillDataGrid(ref DataGridView dataGrid, int index)
        {
            for(int i = 0; i < 10; i++)
                dataGrid.Rows[0].Cells[i].Value = data[index][i].ToString();
            //dataGrid.Rows[i].Cells[1].Value = data[found[i]][1].ToString();
            //dataGrid.Rows[i].Cells[2].Value = data[found[i]][2].ToString();
            //dataGrid.Rows[i].Cells[3].Value = data[found[i]][3].ToString();
            //dataGrid.Rows[i].Cells[4].Value = data[found[i]][4].ToString();
            //dataGrid.Rows[i].Cells[5].Value = data[found[i]][5].ToString();
            //dataGrid.Rows[i].Cells[6].Value = data[found[i]][6].ToString();
            //dataGrid.Rows[i].Cells[7].Value = data[found[i]][7].ToString();
            //dataGrid.Rows[i].Cells[8].Value = data[found[i]][8].ToString();
            //dataGrid.Rows[i].Cells[9].Value = data[found[i]][9].ToString();
        }

        public Form1()
        {
            InitializeComponent();
            
            dataGridView1.ColumnCount = 10;
            dataGridView1.Columns[0].Name = "ID";
            dataGridView1.Columns[1].Name = "OfferID";
            dataGridView1.Columns[2].Name = "Author";
            dataGridView1.Columns[3].Name = "Title";
            dataGridView1.Columns[4].Name = "Publisher";
            dataGridView1.Columns[5].Name = "Year";
            dataGridView1.Columns[6].Name = "ISBN";
            dataGridView1.Columns[7].Name = "Description";
            dataGridView1.Columns[8].Name = "Pages";
            dataGridView1.Columns[9].Name = "CategoryId";
            ReadDataBase();
            comboBox1.SelectedIndex = 1;
            comboBox2.SelectedIndex = 2;
            comboBox3.SelectedIndex = 4;
            textBox1.Text = "Гоголь";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            int index1 = comboBox1.SelectedIndex;
            string str1 = textBox1.Text;
            int index2 = comboBox2.SelectedIndex;
            string str2 = textBox2.Text;
            int index3 = comboBox3.SelectedIndex;
            string str3 = textBox3.Text;
            int[] found = new int[0];
            int[] arr = new int[0];
            found = Search(str1, index1, str2, index2, str3, index3, arr);
            if (found.Length == 0)
                MessageBox.Show("Books with this atribute were not found!");
            else
            {
                //dataGridView1.DataSource = new object();
                
                dataGridView1.Rows.Clear();
                dataGridView1.RowCount = found.Length;
                var list = new List<string>();
                for (int i = 0; i < found.Length; i++)
                {
                    list.Add(data[found[i]][2]);

                    dataGridView1.Rows[i].Cells[0].Value = data[found[i]][0].ToString();
                    dataGridView1.Rows[i].Cells[1].Value = data[found[i]][1].ToString();
                    dataGridView1.Rows[i].Cells[2].Value = data[found[i]][2].ToString();
                    dataGridView1.Rows[i].Cells[3].Value = data[found[i]][3].ToString();
                    dataGridView1.Rows[i].Cells[4].Value = data[found[i]][4].ToString();
                    dataGridView1.Rows[i].Cells[5].Value = data[found[i]][5].ToString();
                    dataGridView1.Rows[i].Cells[6].Value = data[found[i]][6].ToString();
                    dataGridView1.Rows[i].Cells[7].Value = data[found[i]][7].ToString();
                    dataGridView1.Rows[i].Cells[8].Value = data[found[i]][8].ToString();
                    dataGridView1.Rows[i].Cells[9].Value = data[found[i]][9].ToString();
                }

                 BestVariant bv = new BestVariant(list);
                 MessageBox.Show(bv.Choose());
                
                MessageBox.Show(data[SearchIndex(data, bv.Choose())][9]);

                //FillDataGrid(ref dataGridView1, SearchIndex(data, bv.Choose()));
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string str = "";

            OpenFileDialog openFileDialog1 = new OpenFileDialog();


            if (openFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                str = openFileDialog1.FileName;
            }

            for (int i = 2; i < 10; i++)
            {
                //массив текущих неповторяющихся элементов столбца
                //string[] arr = new string[0];
                List<string> no_repeats = new List<string>();

                //поиск неповторяющихся
                for (int j = 0; j < dataGridView1.Rows.Count; j++)
                {
                    if (no_repeats.Count == 0)
                    {
                        no_repeats.Add(dataGridView1.Rows[j].Cells[i].Value.ToString());
                    }
                    else
                    {
                        string cur_str = dataGridView1.Rows[j].Cells[i].Value.ToString();
                        bool flag = false;
                        for (int k = 0; k < no_repeats.Count; k++)
                            if (cur_str == no_repeats[k])
                            {
                                flag = true;
                                break;
                            }

                        if (!flag)
                            no_repeats.Add(cur_str);
                    }
                }

                string[] str_arr = new string[no_repeats.Count];
                str_arr = no_repeats.ToArray();

                //запись в файл заголовка и элементов
                using (StreamWriter sw = File.AppendText(str))
                {
                    sw.WriteLine(dataGridView1.Columns[i].Name+":");
                    for (int j = 0; j < str_arr.Length; j++)
                        sw.WriteLine(str_arr[j]);
                }
            }
        }
    }
}
