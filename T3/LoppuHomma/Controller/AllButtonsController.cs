using LoppuHomma.Controller.Task_A_C;
using LoppuHomma.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoppuHomma.Controller
{
    // AllbuttonsController hoitaa kaikki käsittelyt per jokainen luokka
    public class AllButtonsController(TextBox TextBox1, TextBox TextBox2, ComboBox ComboBox, DataGridView Datagrid)
    {
        DateTimeParse? datetime;
        ApiController? apiController;
        TaskAndTaskB? TaskAndTaskB;
        FormController? formcontroller;
        TaskC? task_C;
        TaskD? task_D;

        private readonly TextBox textBox1 = TextBox1;
        private readonly TextBox textBox2 = TextBox2;
        private readonly ComboBox comboBox = ComboBox;
        private readonly DataGridView datagrid = Datagrid;

        public async Task FindButtonController()
        {
            formcontroller = new FormController(textBox1, textBox2, comboBox, datagrid);
            datetime = new DateTimeParse(textBox1, textBox2);
            apiController = new ApiController(textBox1, textBox2, comboBox, datagrid);

            // Määritetään return value, eli unixtimestamp
            string returnvalue = formcontroller.FindButton(datetime);
            
            // Jaetaan 2 arvoa pilkunperusteela
            string[] strings = returnvalue.Split(',');

            // Haetaan apin data 
            BitcoinData? data = await apiController.ApiValues(strings[0], strings[1]);

            if (data == null)
            {
                MessageBox.Show("Error");
            }
            else
            {
                GetThingWhatWantToDo(data);
            }
        }

        public DateTime ParseStartDayTxTBoxController()
        {
            datetime = new DateTimeParse(textBox1, textBox2);

            return datetime.ParseStartDayTxTBox();
        }

        public DateTime ParseEndDayTxTBoxController()
        {
            datetime = new DateTimeParse(textBox1, textBox2);

            return datetime.ParseEndDayTxTBox();
        }

        public string DateTimeToUnixController(DateTime date)
        {
            datetime = new DateTimeParse(textBox1, textBox2);
            return datetime.DateTimeToUnix(date);
        }

        public DateTime ParseUnixToTimeController(object unixtime)
        {
            datetime = new DateTimeParse(textBox1, textBox2);
            return datetime.ParseUnixToTime(unixtime);
        }

        // APICONTROLLER

        public void GetThingWhatWantToDo(BitcoinData data)
        {
            // Määritetään Switch Case, joka määrittyy comboboxissa valitun itemin mukaan.
            // Suoritetaan sitten määritetty juttu
            TaskAndTaskB = new TaskAndTaskB(textBox1, textBox2, comboBox, datagrid);
            task_C = new TaskC(textBox1, textBox2, comboBox, datagrid);
            task_D = new TaskD(textBox1, textBox2, comboBox, datagrid);
            

            int index = comboBox.SelectedIndex;
            switch (index)
            {
                case 1:
                    TaskAndTaskB.CreateRowToTaskAAndB();
                    TaskAndTaskB.GetLowestAndBiggestPrice(data);
                    ClearAll();
                    break;
                case 2:
                    TaskAndTaskB.CreateRowToTaskAAndB();
                    TaskAndTaskB.GetLowestAndBiggestVolume(data);
                    ClearAll();
                    break;
                case 3:
                    task_C.CreateRowToTaskC();
                    task_C.PisinLasku(data);
                    task_C.PisinNousu(data);
                    ClearAll();
                    break;
                case 4:
                    task_D.CreateRowToTaskD();
                    task_D.GetBestDateToSellOrBuy(data);
                    task_D.GetBestDateToBuyorSell(data);
                    ClearAll();
                    break;
            }

            
        }

        public void ClearAll()
        {
            // Tyhjennetään textboxit ja comboboxin valinta
            textBox1.Text = string.Empty;
            textBox2.Text = string.Empty;
            comboBox.SelectedIndex = 0;
        }

        public void AppendItems()
        {
            formcontroller = new FormController(textBox1, textBox2, comboBox, datagrid);

            formcontroller.ComboBoxAppendItems();
        }

        public bool CheckInsertsForm()
        {
            formcontroller = new FormController(textBox1, textBox2, comboBox, datagrid);

             return formcontroller.CheckInserts();
        }

    }
}
