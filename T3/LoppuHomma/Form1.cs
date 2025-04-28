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
        ButtonController? buttonController;
        public Form1()
        {
            InitializeComponent();

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }


        private async void btnFind_Click(object sender, EventArgs e)
        {
            buttonController = new ButtonController(textBox1, textBox2);

            if (DateTime.Parse(textBox1.Text) >  DateTime.Now.AddDays(-362))
            {
                await buttonController.FindButton();
            }
            MessageBox.Show("Et voi hakea noin vanhaa dataa");
            textBox1.Clear();
            textBox2.Clear();
        }     
    }
}

