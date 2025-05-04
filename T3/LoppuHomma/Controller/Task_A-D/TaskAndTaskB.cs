using LoppuHomma.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoppuHomma.Controller
{
    public class TaskAndTaskB(TextBox textbox1, TextBox textbox2, ComboBox comboBox, DataGridView datagrid)
    {
        AllButtonsController? controller;
        private readonly TextBox textbox1 = textbox1;
        private readonly TextBox textbox2 = textbox2;
        private readonly ComboBox comboBox = comboBox;
        private readonly DataGridView datagrid = datagrid;

        public void CreateRowToTaskAAndB()
        {
            datagrid.Columns.Add("Tyyppi", "Tyyppi");
            datagrid.Columns.Add("Päivämäärä", "Päivämäärä");
            datagrid.Columns.Add("Hinta", "Hinta");
        }

        // Tehtävä A
        public void GetLowestAndBiggestPrice(BitcoinData data)
        {
            controller = new AllButtonsController(textbox1, textbox2, comboBox, datagrid);

            // Pienin hinta 
            var orderedPricesASC = data.Prices.OrderBy(item => item[1]).ToList();
            DateTime small_timestamp = controller.ParseUnixToTimeController(orderedPricesASC[0][0]);
            double small_price = Convert.ToDouble(orderedPricesASC[0][1]);

            // Suurin Hinta

            var orderedPricesDESC = data.Prices.OrderByDescending(item => item[1]).ToList();
            DateTime biggest_timestamp = controller.ParseUnixToTimeController(orderedPricesDESC[0][0]);
            double biggest_price = Convert.ToDouble(orderedPricesDESC[1][1]);

            // Lisätään arvot datagridviewiin

            datagrid.Rows.Add("Halvin hinta:", small_timestamp.ToString("yyyy-MM-dd"), small_price.ToString("f2"));
            datagrid.Rows.Add("Suurin hinta:", biggest_timestamp.ToString("yyyy-MM-dd"), biggest_price.ToString("f2"));
        }

        // Tehtävä B

        public void GetLowestAndBiggestVolume(BitcoinData data)
        {
            controller = new AllButtonsController(textbox1, textbox2, comboBox, datagrid);

            // Pienin volyymi
            var orderedPricesASC = data.Total_volumes.OrderBy(item => item[1]).ToList();
            DateTime small_timestamp = controller.ParseUnixToTimeController(orderedPricesASC[0][0]);
            double small_volume = Convert.ToDouble(orderedPricesASC[0][1]);

            // Suurin volyymi

            var orderedPricesDESC = data.Total_volumes.OrderByDescending(item => item[1]).ToList();
            DateTime biggest_timestamp = controller.ParseUnixToTimeController(orderedPricesDESC[0][0]);
            double biggest_volume = Convert.ToDouble(orderedPricesDESC[1][1]);

            // Lisätään arvot datagridviewiin
            datagrid.Rows.Add("Pienin volyymi:", small_timestamp.ToString("yyyy-MM-dd"), small_volume.ToString("f2"));
            datagrid.Rows.Add("Suurin volyymi:", biggest_timestamp.ToString("yyyy-MM-dd"), biggest_volume.ToString("f2"));
        }
    }
}
