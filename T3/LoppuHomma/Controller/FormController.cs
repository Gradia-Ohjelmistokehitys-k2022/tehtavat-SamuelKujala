using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LoppuHomma.Controller
{
    public class FormController
    {
        TextBox textBox1 { get; set; }
        TextBox textBox2 { get; set; }
        DataGridView DataGrid { get; set; }
        ComboBox ComboBox { get; set; }
        public FormController(TextBox textbox1, TextBox textbox2, ComboBox comboBox, DataGridView datagrid) 
        { 
            textBox1 = textbox1;
            textBox2 = textbox2;
            ComboBox = comboBox;
            DataGrid = datagrid;
        }

        public void ComboBoxAppendItems()
        {
            // Lisätään ComboBoxiin itemit
            ComboBox.Items.Insert(0, "-- Valitse --");
            ComboBox.Items.Insert(1, "Tehtävä A");
            ComboBox.Items.Insert(2, "Tehtävä B");
            ComboBox.Items.Insert(3, "Tehtävä C");
            ComboBox.Items.Insert(4, "Tehtävä D");

            ComboBox.SelectedIndex = 0;
        }

        public bool CheckInserts()
        {
            int index = ComboBox.SelectedIndex;

            // Tarkistetaan, onko päivämäärät annettu
            if (!string.IsNullOrWhiteSpace(textBox1.Text) || !string.IsNullOrWhiteSpace(textBox2.Text))
            {
                if (CheckIfDatetime() == true)
                {
                    if (index >= 1 && index <= 4)
                    {
                        if (DateTime.Parse(textBox1.Text) >= DateTime.Now.AddDays(-362))
                        {
                            if (DataGrid.Rows.Count > 0)
                            {
                                DataGrid.Rows.Clear();
                                DataGrid.Columns.Clear();

                            }

                            return true;
                        }
                        MessageBox.Show("Et voi hakea noin vanhaa dataa");
                        textBox1.Clear();
                        textBox2.Clear();
                        return false;   
                    }
                    MessageBox.Show("Valitse jokin vaihtoehto ComboBoxista");
                    return false;
                }
                MessageBox.Show("Tekstikenttien täytyy olla määrritelty päivämäärät. (dd.mm.year)");
                return false;
            }
            MessageBox.Show("Lisää päivämäärät tekstilaatikoihin");
            return false;
        }

        public string FindButton(DateTimeParse datetime)
        {
            DateTime dateTime = datetime.ParseStartDayTxTBox();
            DateTime dateTime2 = datetime.ParseEndDayTxTBox();

            string unixTime1 = datetime.DateTimeToUnix(dateTime);
            string unixTime2 = datetime.DateTimeToUnix(dateTime2);
            return unixTime1 + "," + unixTime2;
        }

        public bool CheckIfDatetime()
        {
            if(DateTime.TryParse(textBox1.Text, out DateTime dateTime))
            {
                return true;
            }
            return false;
        }
    }
}
