using LoppuHomma.Model;
using Microsoft.VisualBasic.ApplicationServices;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Drawing.Design;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoppuHomma.Controller
{
    public class ApiController
    {
        TextBox textbox1 = new TextBox();
        TextBox textbox2 = new TextBox();
        DateTimeParse datetime;


        private static readonly HttpClient client = new HttpClient();

        public ApiController(TextBox textbox1, TextBox textbox2)
        {
            this.textbox1 = textbox1;
            this.textbox2 = textbox2;
        }

        public async Task ApiValues(string UnixTime1, string UnixTime2)
        {
            string url = $"https://api.coingecko.com/api/v3/coins/bitcoin/market_chart/range?vs_currency=eur&from={UnixTime1}&to={UnixTime2}";
            HttpResponseMessage response = await client.GetAsync(url);

            string responseBody = await response.Content.ReadAsStringAsync();


            BitcoinData data = JsonConvert.DeserializeObject<BitcoinData>(responseBody);

            //  GetValuesFromMidnight(data);
            //  GetLowestAndBiggestVolume(data);
            // GetLowestAndBiggestPrice(data);
            GetLowestAndBiggestMarketCap(data);
        }

        public void GetValuesFromMidnight(BitcoinData data)
        {
            datetime = new DateTimeParse(textbox1, textbox2);
            foreach (var marketCap in data.market_caps)
            {
                long timestamp = (long)marketCap[0];
                double value = (long)marketCap[1];
                Console.WriteLine($"Timestamp: {timestamp}, Value: {value}");

                DateTime day = datetime.ParseUnixToTime(timestamp);

                Console.WriteLine($"Exact date: {day}, Value: {value}");
            }
        }


        public void GetLowestAndBiggestPrice(BitcoinData data)
        {
            List<List<object>> volumes = data.prices;

            var LowestPriceValue = volumes.OrderBy(v => v[1]).First();
            double lowestPrice = (double)LowestPriceValue[1];
            long LowestVolumeTimestamp = (long)LowestPriceValue[0];


            var biggestVolumeEntry = volumes.OrderByDescending(v => v[1]).First();
            double biggestVolume = (double)biggestVolumeEntry[1];
            long biggestVolumeTimestamp = (long)biggestVolumeEntry[0];

            DateTime biggestVolumeDateTime = DateTimeOffset.FromUnixTimeMilliseconds(biggestVolumeTimestamp).DateTime;
            DateTime lowestVolumeDateTime = DateTimeOffset.FromUnixTimeMilliseconds(LowestVolumeTimestamp).DateTime;

            MessageBox.Show($"Isoin Price: {lowestPrice:f2} Päivämäärä: {biggestVolumeDateTime} ja Pienin Price: {biggestVolume:f2} Päivämäärä {lowestVolumeDateTime}");
        }



        public void GetLowestAndBiggestVolume(BitcoinData data)
        {
            List<List<object>> volumes = data.prices;

            var LowestVolumeEntry = volumes.OrderBy(v => v[1]).First();
            double lowestVolume = (double)LowestVolumeEntry[1];
            long LowestVolumeTimestamp = (long)LowestVolumeEntry[0];


            var biggestVolumeEntry = volumes.OrderByDescending(v => v[1]).First();
            double biggestVolume = (double)biggestVolumeEntry[1];
            long biggestVolumeTimestamp = (long)biggestVolumeEntry[0];

            DateTime biggestVolumeDateTime = DateTimeOffset.FromUnixTimeMilliseconds(biggestVolumeTimestamp).DateTime;
            DateTime lowestVolumeDateTime = DateTimeOffset.FromUnixTimeMilliseconds(LowestVolumeTimestamp).DateTime;

            MessageBox.Show($"Isoin Volumyymi: {biggestVolume:f2} Päivämäärä: {biggestVolumeDateTime} ja Pienin volume: {lowestVolume:f2} Päivämäärä {lowestVolumeDateTime}");
        }



        public void GetLowestAndBiggestMarketCap(BitcoinData data)
        {
            datetime = DateTimeParse(textbox1, textbox2);
          
            List<List<object>> values = data.prices; // Muutetaan lista List<List<object>>:ksi, jotta voidaan säilyttää sekä päivämäärä että hinta
            
            // Sortataan arvot päivämäärän mukaan
            values.Sort((a, b) => ((long)a[0]).CompareTo((long)b[0]));

            int maxUp = 0;
            int maxDown = 0;
            int currentUp = 0;
            int currentDown = 0;

            long? startUpDate = null;
            long? startDownDate = null;

            for (int i = 1; i < values.Count; i++)
            {
                var prev = values[i - 1];
                var curr = values[i];

                var prevDate = (long)prev[0];
                var currDate = (long)curr[0];
                var prevPrice = (double)prev[1];
                var currPrice = (double)curr[1];

                // Tarkistetaan, onko nykyinen hinta suurempi kuin edellinen
                if (currPrice > prevPrice)
                {
                    currentUp++;
                    if (currentUp == 1) startUpDate = currDate; // Tallennetaan ensimmäinen nousupäivä
                    maxUp = Math.Max(maxUp, currentUp);
                    currentDown = 0;
                }
                // Tarkistetaan, onko nykyinen hinta pienempi kuin edellinen
                else if (currPrice < prevPrice)
                {
                    currentDown++;
                    if (currentDown == 1) startDownDate = currDate; // Tallennetaan ensimmäinen laskupäivä
                    maxDown = Math.Max(maxDown, currentDown);
                    currentUp = 0;
                }
                else
                {
                    currentUp = 0;
                    currentDown = 0;
                }
            }

            // Näytetään viestit, joissa näkyy sekä suurimmat nousu- että laskutrendit sekä niiden päivämäärät
            MessageBox.Show($"Longest upward trend: {maxUp} days starting from {startUpDate?.ToShortDateString()}.");
            MessageBox.Show($"Longest downward trend: {maxDown} days starting from {startDownDate?.ToShortDateString()}.");
        }
    }
}
