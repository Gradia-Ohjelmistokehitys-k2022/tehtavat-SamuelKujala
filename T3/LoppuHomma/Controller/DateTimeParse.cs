using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoppuHomma.Controller
{
    public class DateTimeParse
    {
        TextBox textbox1 = new TextBox();
        TextBox textbox2 = new TextBox();   
        public DateTimeParse(TextBox textbox1, TextBox textbox2) 
        {
            this.textbox1 = textbox1;
            this.textbox2 = textbox2;
        }
        public DateTime ParseStartDayTxTBox()
        {
            DateTime first = DateTime.Parse(textbox1.Text);

            return first;
        }

        public DateTime ParseEndDayTxTBox()
        {
            DateTime second = DateTime.Parse(textbox2.Text);

            return second;
        }

        public string DateTimeToUnix(DateTime dateTime)
        {
            DateTimeOffset date = new DateTimeOffset(dateTime);
            return date.ToUnixTimeSeconds().ToString();
        }

        public DateTime ParseUnixToTime(double unixtime)
        {
            long unix = (long)unixtime;

            DateTime date = DateTimeOffset.FromUnixTimeMilliseconds(unix).DateTime;
            return date;
        }
    }
}
