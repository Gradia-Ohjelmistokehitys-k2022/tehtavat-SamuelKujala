using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoppuHomma.Controller
{
    public class DateTimeParse(TextBox textbox1, TextBox textbox2)
    {
        private readonly TextBox textbox1 = textbox1;
        private readonly TextBox textbox2 = textbox2;

        public DateTime ParseStartDayTxTBox()
        {
            // Muutetaan textbox1 datetimeksi

            DateTime first = DateTime.Parse(textbox1.Text);

            return first;
        }

        public DateTime ParseEndDayTxTBox()
        {
            // Muutetaan textbox2 datetimeksi
            
            DateTime second = DateTime.Parse(textbox2.Text);

            return second;
        }

        public string DateTimeToUnix(DateTime dateTime)
        {
            // Muutetaan Datetime -> Unix time 
            DateTimeOffset date = new(dateTime);
            return date.ToUnixTimeSeconds().ToString();
        }

        public DateTime ParseUnixToTime(object unixtime)
        {
            // Muutetaan unix time -> Datime
            long unix = (long)unixtime;

            DateTime date = DateTimeOffset.FromUnixTimeMilliseconds(unix).DateTime;
            return date;
        }
    }
}
