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
        AllButtonsController? controller;
        public Form1()
        {
            InitializeComponent();

            // Lis‰t‰‰n Comboboxiin halutut itemit, jotka haetaan toisesta luokasta
            controller = new AllButtonsController(textBox1, textBox2, comboBox1, dataGridView1);
            controller.AppendItems();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
        }


        private async void BtnFind_Click(object sender, EventArgs e)
        {
            // M‰‰ritet‰‰n, jos tarkistukset on true niin suorita ohjelma.
            controller = new AllButtonsController(textBox1, textBox2, comboBox1, dataGridView1);
            if (controller.CheckInsertsForm() == true)
            {
                await controller.FindButtonController();
            }
        }
    }
}

