using LoppuHomma.Model;
using Microsoft.VisualBasic.ApplicationServices;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing.Design;
using System.Linq;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.Marshalling;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace LoppuHomma.Controller
{
    public class ApiController
    {
        TextBox textbox1 = new TextBox();
        TextBox textbox2 = new TextBox();
        ComboBox comboBox = new ComboBox();
        DataGridView datagrid = new DataGridView();

        AllButtonsController controller;


        private static readonly HttpClient client = new HttpClient();

        public ApiController(TextBox textbox1, TextBox textbox2, ComboBox comboBox, DataGridView datagrid)
        {
            this.textbox1 = textbox1;
            this.textbox2 = textbox2;
            this.comboBox = comboBox;
            this.datagrid = datagrid;
        }

        public async Task<BitcoinData?> ApiValues(string UnixTime1, string UnixTime2)
        {
            controller = new AllButtonsController(textbox1, textbox2, comboBox, datagrid);

            string url = $"https://api.coingecko.com/api/v3/coins/bitcoin/market_chart/range?vs_currency=eur&from={UnixTime1}&to={UnixTime2}";
            HttpResponseMessage response = await client.GetAsync(url);

            string responseBody = await response.Content.ReadAsStringAsync();

            BitcoinData? data = JsonConvert.DeserializeObject<BitcoinData?>(responseBody);

            BitcoinData? bitcoinData = null;

            if (data != null)
            {
                bitcoinData = GetDataOnlyFromMidnight(data);
            }
            return bitcoinData;
        }

        public BitcoinData GetDataOnlyFromMidnight(BitcoinData? data)
        {
            var sortedPrices = data.prices.OrderBy(row => Convert.ToInt64(row[0])).ToList();
            var sortedMarket_caps = data.market_caps.OrderBy(row => Convert.ToInt64(row[0])).ToList();
            var sortedVolumes = data.total_volumes.OrderBy(row => Convert.ToInt64(row[0])).ToList();

            var closestMidnight = SortDataToMidnight(sortedPrices);

            BitcoinData bitcoin = new BitcoinData()
            {
                market_caps = SortDataToMidnight(sortedMarket_caps),
                prices = SortDataToMidnight(sortedPrices),
                total_volumes = SortDataToMidnight(sortedVolumes) 
            };

            return bitcoin;
        }

        public List<List<Object>> SortDataToMidnight(List<List<Object>> data)
        {
            controller = new AllButtonsController(textbox1, textbox2, comboBox, datagrid);

            List<List<object>> ReturnData = new List<List<object>>();

            DateTime? previousDate = null;
            List<object> closestPrice = null;

            foreach (var item in data)
            {
                DateTime timestamp = controller.ParseUnixToTimeController(item[0]);
                double price = Convert.ToDouble(item[1]);

                if (previousDate == null || timestamp.Date != previousDate.Value.Date)
                {
                    if (closestPrice != null)
                    {
                        ReturnData.Add(closestPrice);
                    }

                    closestPrice = item;
                    previousDate = timestamp.Date;
                }
                else
                {
                    if (Math.Abs((timestamp - previousDate.Value).TotalMinutes) < Math.Abs((controller.ParseUnixToTimeController(closestPrice[0]) - previousDate.Value).TotalMinutes))
                    {
                        closestPrice = item;
                    }
                }

            }

            if (closestPrice != null)
            {
                ReturnData.Add(closestPrice);
            }

            return ReturnData;
        }

        public void GetLowestAndBiggestVolume(BitcoinData data)
        {
           
           
        }



        public void GetLowestAndBiggestMarketCap(BitcoinData data)
        {
           
        }

    }
}
