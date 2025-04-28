using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoppuHomma.Controller
{
    public class ButtonController 
    {
        TextBox textbox1 = new TextBox();
        TextBox textbox2 = new TextBox();
        DateTimeParse? datetime;
        ApiController? apiController;
        public ButtonController(TextBox textBox1, TextBox textBox2)
        {
            this.textbox1 = textBox1;
            this.textbox2 = textBox2;

        }

        public async Task FindButton()
        {
            datetime = new DateTimeParse(textbox1, textbox2);
            apiController = new ApiController(textbox1, textbox2);

            DateTime dateTime = datetime.ParseStartDayTxTBox();
            DateTime dateTime2 = datetime.ParseEndDayTxTBox();

            string unixTime1 = datetime.DateTimeToUnix(dateTime);
            string unixTime2 = datetime.DateTimeToUnix(dateTime2);

            await apiController.ApiValues(unixTime1, unixTime2);
        }
    }
}
