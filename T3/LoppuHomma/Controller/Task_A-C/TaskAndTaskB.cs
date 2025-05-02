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

        public void GetLowestAndBiggestVolume(BitcoinData data)
        {
            controller = new AllButtonsController(textbox1, textbox2, comboBox, datagrid);

            // Smallest Volume
            var orderedPricesASC = data.Total_volumes.OrderBy(item => item[1]).ToList();
            DateTime small_timestamp = controller.ParseUnixToTimeController(orderedPricesASC[0][0]);
            double small_volume = Convert.ToDouble(orderedPricesASC[0][1]);

            // Biggest Volume

            var orderedPricesDESC = data.Total_volumes.OrderByDescending(item => item[1]).ToList();
            DateTime biggest_timestamp = controller.ParseUnixToTimeController(orderedPricesDESC[0][0]);
            double biggest_volume = Convert.ToDouble(orderedPricesDESC[1][1]);

            datagrid.Rows.Add("Volyymi pienimmillään:", small_timestamp.ToString("yyyy-MM-dd"), small_volume.ToString("f2"));
            datagrid.Rows.Add("Volyymi suurimmillaan:", biggest_timestamp.ToString("yyyy-MM-dd"), biggest_volume.ToString("f2"));
        }

        public void GetLowestAndBiggestPrice(BitcoinData data)
        {
            controller = new AllButtonsController(textbox1, textbox2, comboBox, datagrid);

            // Smallest Price
            var orderedPricesASC = data.Prices.OrderBy(item => item[1]).ToList();
            DateTime small_timestamp = controller.ParseUnixToTimeController(orderedPricesASC[0][0]);
            double small_price = Convert.ToDouble(orderedPricesASC[0][1]);

            // Biggest Price

            var orderedPricesDESC = data.Prices.OrderByDescending(item => item[1]).ToList();
            DateTime biggest_timestamp = controller.ParseUnixToTimeController(orderedPricesDESC[0][0]);
            double biggest_price = Convert.ToDouble(orderedPricesDESC[1][1]);

            datagrid.Rows.Add("Hinta pienimmillään:", small_timestamp.ToString("yyyy-MM-dd"), small_price.ToString("f2"));
            datagrid.Rows.Add("Hinta suurimmillaan:", biggest_timestamp.ToString("yyyy-MM-dd"), biggest_price.ToString("f2"));
        }
    }
}
