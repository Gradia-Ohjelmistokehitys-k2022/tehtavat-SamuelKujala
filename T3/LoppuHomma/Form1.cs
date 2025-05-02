using System;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Windows.Forms;

using LoppuHomma.Model;
using LoppuHomma.Controller;

namespace LoppuHomma
{
    public partial class Form1 : Form
    {
        AllButtonsController? allButtonsController;
        public Form1()
        {
            InitializeComponent();
            ComboBoxAppendItems();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
        }


        private async void BtnFind_Click(object sender, EventArgs e)
        {
            int index = comboBox1.SelectedIndex;

            // Tarkistetaan, onko p‰iv‰m‰‰r‰t annettu
            if (string.IsNullOrWhiteSpace(textBox1.Text) || string.IsNullOrWhiteSpace(textBox2.Text))
            {
                MessageBox.Show("Lis‰‰ p‰iv‰m‰‰r‰t tekstilaatikoihin");
                return;
            }

            // Tarkistetaan, onko kelvollinen valinta
            if (index < 1 || index > 4)
            {
                MessageBox.Show("Valitse jokin vaihtoehto ComboBoxista");
                return;
            }

            // Tarkistetaan, ettei haeta liian vanhaa dataa
            if (DateTime.Parse(textBox1.Text) <= DateTime.Now.AddDays(-362))
            {
                MessageBox.Show("Et voi hakea noin vanhaa dataa");
                textBox1.Clear();
                textBox2.Clear();
                return;
            }

            if (dataGridView1.Rows.Count > 0)
            {
                dataGridView1.Rows.Clear();
                dataGridView1.Columns.Clear();

            }
            allButtonsController = new AllButtonsController(textBox1, textBox2, comboBox1, dataGridView1);
            await allButtonsController.FindButtonController();
        }

        public void ComboBoxAppendItems()
        {
            comboBox1.Items.Insert(0, "-- Valitse --");
            comboBox1.Items.Insert(1, "Teht‰v‰ A");
            comboBox1.Items.Insert(2, "Teht‰v‰ B");
            comboBox1.Items.Insert(3, "Teht‰v‰ C");
            comboBox1.Items.Insert(4, "Teht‰v‰ D");

            comboBox1.SelectedIndex = 0;
        }
    }
}

