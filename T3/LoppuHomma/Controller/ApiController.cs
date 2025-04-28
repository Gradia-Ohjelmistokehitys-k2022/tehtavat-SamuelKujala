using LoppuHomma.Model;
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

            
            var lista = JsonConvert.DeserializeObject<BitcoinData>(responseBody);
            GetvaluesFromMiddleNight(lista.prices);
        }

        public void GetvaluesFromMiddleNight(List<List<double>> values)
        {
            List<DateTime> dates = new List<DateTime>();

            foreach (var value in values)
            {
                // Deserialisoidaan timestamp ja value
                DateTimeOffset dateTimeOffset = DateTimeOffset.FromUnixTimeMilliseconds((long)value[0]);
                DateTime dateTime2 = dateTimeOffset.LocalDateTime;

                dates.Add(dateTime2);
            }

            dates.Sort();

            List<DateTime> date2 = new List<DateTime>();
            DateTime? dateTime = null;

            foreach (DateTime date in dates)
            {
                if (dateTime == null)
                {
                    dateTime = date;
                    date2.Add(date);
                }
                else if (dateTime.Value.Date == date.Date)
                {
                    date2.Add(date);
                }
                else
                {
                    // Process date2
                    date2.Sort();
                    

                    var closestDate = date2.OrderBy(x => Math.Abs((x - x.Date).TotalSeconds)).First();
                    MessageBox.Show(closestDate.ToString());

                    date2.Clear();
                    dateTime = date;
                    date2.Add(date);
                }
            }

            // Final sorting and closest date processing
            date2.Sort();
            foreach (var item in date2)
            {
                MessageBox.Show(item.ToString());
            }

            var finalClosestDate = date2.OrderBy(x => Math.Abs((x - x.Date).TotalSeconds)).First();
            MessageBox.Show(finalClosestDate.ToString());
        }





        public void GetLowestAndBiggestVolume(List<List<double>> volumes)
        {
            var LowestVolumeEntry = volumes.OrderBy(v => v[1]).First();
            double lowestVolume = LowestVolumeEntry[1];
            long LowestVolumeTimestamp = (long)LowestVolumeEntry[0];
            var biggestVolumeEntry = volumes.OrderByDescending(v => v[1]).First();


            double biggestVolume = biggestVolumeEntry[1];
            long biggestVolumeTimestamp = (long)biggestVolumeEntry[0];

            string formattedBiggestVolume = biggestVolume.ToString("N2");
            string formattedLowestVolume = lowestVolume.ToString("N2");
            DateTime biggestVolumeDateTime = DateTimeOffset.FromUnixTimeMilliseconds(biggestVolumeTimestamp).DateTime;
            DateTime lowestVolumeDateTime = DateTimeOffset.FromUnixTimeMilliseconds(LowestVolumeTimestamp).DateTime;

            MessageBox.Show($"Isoin Volumyymi: {formattedBiggestVolume} Päivämäärä: {biggestVolumeDateTime} ja Pienin volume: {formattedLowestVolume} Päivämäärä {lowestVolumeDateTime}");
        }
    }
}
