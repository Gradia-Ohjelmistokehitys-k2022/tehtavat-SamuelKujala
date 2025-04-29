using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoppuHomma.Controller
{
    public class ButtonController 
    {
        public ButtonController()
        {
            

        }

        public string FindButton(DateTimeParse datetime, ApiController apiController)
        {
            DateTime dateTime = datetime.ParseStartDayTxTBox();
            DateTime dateTime2 = datetime.ParseEndDayTxTBox();

            string unixTime1 = datetime.DateTimeToUnix(dateTime);
            string unixTime2 = datetime.DateTimeToUnix(dateTime2);
            return unixTime1 + "," + unixTime2;
        }
    }
}
