using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ZClient.Library.USMarc.Bib1Attributes;
using ZClient.Library.USMarc.Z3950;
using ZClient.Manage;

namespace ZClient
{
    public partial class MainForm : Form
    {
        private readonly Manager _manager = new Manager();
        private IDictionary<Server, IEnumerable<string>> _result;

        public MainForm()
        {
            InitializeComponent();
            comboBox1.SelectedIndex = 0;
            WindowState = FormWindowState.Maximized;
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            _manager.LoadServers("database.csv");
        }

        private async void button1_Click(object sender, EventArgs e)
        {
            toolStripStatusLabel1.Text = @"Идет поиск...";

            var query = textBox1.Text;

            _result =  await _manager.Search(query, GetEnums(comboBox1.SelectedIndex));

            foreach (var pair in _result)
            {
                listBox1.Items.Add(pair.Key);
            }
            toolStripStatusLabel1.Text = @"Готово";
        }


        private Bib1Attr GetEnums(int field)
        {
            switch (field)
            {
                case 0: 
                    return Bib1Attr.ISBN;
                case 1:
                    return Bib1Attr.Author;
                case 2: 
                    return Bib1Attr.Title;
            }

            return Bib1Attr.Title;
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            foreach (var pair in _result)
            {
                if (pair.Key.Equals(listBox1.SelectedItem))
                {
                    richTextBox1.Text = pair.Value.FirstOrDefault();
                }
            }
        }
    }
}
