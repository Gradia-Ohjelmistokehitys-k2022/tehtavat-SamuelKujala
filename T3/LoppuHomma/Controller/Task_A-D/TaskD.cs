using LoppuHomma.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoppuHomma.Controller.Task_A_C
{
    public class TaskD(TextBox textbox1, TextBox textbox2, ComboBox comboBox, DataGridView datagrid)
    {
        AllButtonsController? controller;
        private readonly TextBox textbox1 = textbox1;
        private readonly TextBox textbox2 = textbox2;
        private readonly ComboBox comboBox = comboBox;
        private readonly DataGridView datagrid = datagrid;

        public void CreateRowToTaskD()
        {
            datagrid.Columns.Add("Tyyppi", "Tyyppi");
            datagrid.Columns.Add("Päivämäärä", "Päivämäärä");
            datagrid.Columns.Add("Hinta", "Hinta");

        }

        public void GetBestDateToBuyorSell(BitcoinData data)
        {
            controller = new AllButtonsController(textbox1, textbox2, comboBox, datagrid);

            // Järjestetään lista pienimmästä suurimpaan

            var lowestPrice_List = data.Prices.OrderBy(item => item[1]).ToList();
            double minPrice = Convert.ToDouble(lowestPrice_List[0][1]);
            long minTimestamp = Convert.ToInt64(lowestPrice_List[0][0]);

            double maxProfit = 0;
            double profit = 0;
            long buyTime = minTimestamp;
            long sellTime = minTimestamp;

            // Käydään data.prices läpi foreach silmukalla

            foreach (var item in data.Prices)
            {
                long currentTimestamp = Convert.ToInt64(item[0]);
                double currentPrice = Convert.ToDouble(item[1]);

                profit = currentPrice - minPrice;

                if (minTimestamp < currentTimestamp)
                {
                    if (profit > maxProfit)
                    {
                        maxProfit = profit;
                        sellTime = currentTimestamp;

                    }

                    if (currentPrice < minPrice)
                    {
                        minPrice = currentPrice;
                        minTimestamp = currentTimestamp;
                    }
                }
            }

            // Muutetaan arvot datetimeksi ja talletaan uusi profit

            DateTime buyDate = controller.ParseUnixToTimeController(buyTime);
            DateTime sellDate = controller.ParseUnixToTimeController(sellTime);
            profit = maxProfit;

            // Lisätään arvot DataGridViewiin

            datagrid.Rows.Add("Paras aika ostaa", $"{buyDate:dd.MM.yyyy}", $"{minPrice:f2} €");
            datagrid.Rows.Add("Paras aika myydä", $"{sellDate:dd.MM.yyyy}", $"{profit + minPrice:f2} €");
            datagrid.Rows.Add("Voitto", "-", $"{profit:F2} €");
        }

        public void GetBestDateToSellOrBuy(BitcoinData data)
        {
            controller = new AllButtonsController(textbox1, textbox2, comboBox, datagrid);

            // Järjestetään lista suurimmasta pienimpään

            var biggestPrice_List = data.Prices.OrderByDescending(item => item[1]).ToList();
            double MaxPrice = Convert.ToDouble(biggestPrice_List[0][1]);
            long maxTimestamp = Convert.ToInt64(biggestPrice_List[0][0]);

            double maxProfit = 0;
            double profit = 0;
            long buyTime = maxTimestamp;
            long sellTime = maxTimestamp;

            // Käydään data.prices läpi foreach silmukalla

            foreach (var item in data.Prices)
            {
                long currentTimestamp = Convert.ToInt64(item[0]);
                double currentPrice = Convert.ToDouble(item[1]);

                profit = MaxPrice - currentPrice;

                if (maxTimestamp < currentTimestamp)
                {
                    if (profit > maxProfit)
                    {
                        maxProfit = profit;
                        buyTime = currentTimestamp;

                    }

                    if (currentPrice < MaxPrice)
                    {
                        MaxPrice = currentPrice;
                        maxTimestamp = currentTimestamp;
                    }   
                }
            }

            // Muutetaan arvot datetimeksi ja talletaan uusi profit
            DateTime buyDate = controller.ParseUnixToTimeController(buyTime);
            DateTime sellDate = controller.ParseUnixToTimeController(sellTime);
            profit = maxProfit;

            // Lisätään arvot DataGridViewiin

            datagrid.Rows.Add("Paras aika myydä", $"{sellDate:dd.MM.yyyy}", $"{MaxPrice:f2} €");
            datagrid.Rows.Add("Paras aika ostaa", $"{buyDate:dd.MM.yyyy}", $"{profit:f2} €");
            datagrid.Rows.Add("Voitto", "-", $"{MaxPrice - profit:F2} €");
         
        }
    }
}
