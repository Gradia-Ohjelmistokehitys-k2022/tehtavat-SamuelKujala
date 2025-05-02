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
    public class ApiController(TextBox textbox1, TextBox textbox2, ComboBox comboBox, DataGridView datagrid)
    {
        private readonly TextBox textbox1 = textbox1;
        private readonly TextBox textbox2 = textbox2;
        private readonly ComboBox comboBox = comboBox;
        private readonly DataGridView datagrid = datagrid;

        AllButtonsController? controller;


        private static readonly HttpClient client = new();

        public async Task<BitcoinData?> ApiValues(string UnixTime1, string UnixTime2)
        {
            controller = new AllButtonsController(textbox1, textbox2, comboBox, datagrid);
            // Määritetään url

            string url = $"https://api.coingecko.com/api/v3/coins/bitcoin/market_chart/range?vs_currency=eur&from={UnixTime1}&to={UnixTime2}";

            // Haetaan tiedot apista ja odotetaan, kunnes pyyntö on saatu päätökseen onnistuneesti
            HttpResponseMessage response = await client.GetAsync(url);

            // Luetaan saatu tieto apista
            string responseBody = await response.Content.ReadAsStringAsync();

            // Muutetaan Json Objekti -> C# Objektiksi

            BitcoinData? data = JsonConvert.DeserializeObject<BitcoinData?>(responseBody);

            BitcoinData? bitcoinData = null;

            if (data != null)
            {
                // Haetaan tiedot vain keskiyöltä, eli niin sanottu päivä hinta
                bitcoinData = GetDataOnlyFromMidnight(data);
            }
            return bitcoinData;
        }

        public BitcoinData GetDataOnlyFromMidnight(BitcoinData data)
        {
            // Järjestetään jokaisen listan Timestamp samaan järjestykseen.
            var sortedPrices = data.Prices.OrderBy(row => Convert.ToInt64(row[0])).ToList();
            var sortedMarket_caps = data.Market_caps.OrderBy(row => Convert.ToInt64(row[0])).ToList();
            var sortedVolumes = data.Total_volumes.OrderBy(row => Convert.ToInt64(row[0])).ToList();

            // Luodaan uusi Bitcoin objekti ja lisätään tiedot
            BitcoinData bitcoin = new()
            {
                Market_caps = SortDataToMidnight(sortedMarket_caps),
                Prices = SortDataToMidnight(sortedPrices),
                Total_volumes = SortDataToMidnight(sortedVolumes) 
            };

            return bitcoin;
        }

        public List<List<Object>> SortDataToMidnight(List<List<object>> data)
        {
            controller = new AllButtonsController(textbox1, textbox2, comboBox, datagrid);

            List<List<object>> ReturnData = [];

            DateTime? previousDate = null;
            List<object> closestPrice = null;

            // Käydään objekti listan tiedot yksitellen läpi

            foreach (var item in data)
            {
                // Muutetaan itemin eka osa Datetimeksi ja toinen osa doubleksi objektista.
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
                    // Verrantaan 2 aika väliä ja katsotaan kumpi on pienempi
                    // Etsitään, onko nykyinen aikaleima (timestamp) lähempänä edellistä päivämäärää (previousDate)
                    // kuin "closestPrice" -arvon aikaleima. Vertailu tehdään minuutteina.

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

    }
}
