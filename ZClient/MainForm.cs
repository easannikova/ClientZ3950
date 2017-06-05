using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
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
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            _manager.LoadServers("database.csv");
        }

        private async void button1_Click(object sender, EventArgs e)
        {
            var query = textBox1.Text;

            _result =  await _manager.Search(query);

            foreach (var pair in _result)
            {
                listBox1.Items.Add(pair.Key);
            }
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
