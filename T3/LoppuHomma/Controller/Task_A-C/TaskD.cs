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

        public void GetBestDateToSellOrBuy(BitcoinData data)
        {
            controller = new AllButtonsController(textbox1, textbox2, comboBox, datagrid);

            var lowestPrice_List = data.Prices.OrderBy(item => item[1]).ToList();
            double minPrice = Convert.ToDouble(lowestPrice_List[0][1]);
            long minTimestamp = Convert.ToInt64(lowestPrice_List[0][0]);

            double maxProfit = 0;
            double profit = 0;
            long buyTime = minTimestamp;
            long sellTime = minTimestamp;

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


            DateTime buyDate = controller.ParseUnixToTimeController(buyTime);
            DateTime sellDate = controller.ParseUnixToTimeController(sellTime);
            profit = maxProfit;

            MessageBox.Show($"Paras aika ostaa: {buyDate:dd.MM.yyyy} (Hinta: {minPrice:f2})\n" +
                            $"Paras aika myydä: {sellDate:dd.MM.yyyy} (Hinta: {profit + minPrice:f2})\n" +
                            $"Maksimaalinen voitto: {profit:F2} €");

        }
    }
}
