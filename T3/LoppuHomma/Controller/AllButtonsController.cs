using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoppuHomma.Controller
{
    public class AllButtonsController
    {
        DateTimeParse? datetime;
        ApiController? apiController;
        ButtonController? ButtonController;

        TextBox textBox1 = new TextBox();
        TextBox textBox2 = new TextBox();

        public AllButtonsController(TextBox textBox1, TextBox textBox2)
        {
            this.textBox1 = textBox1;
            this.textBox2 = textBox2;
        }

        public async Task FindButtonController()
        {
            ButtonController = new ButtonController();
            datetime = new DateTimeParse(textBox1, textBox2);
            apiController = new ApiController(textBox1, textBox2);


            string returnvalue = ButtonController.FindButton(datetime, apiController);

            string[] strings = returnvalue.Split(',');

            await apiController.ApiValues(strings[0], strings[1]);
        }

        public DateTime ParseStartDayTxTBoxController()
        {
            datetime = new DateTimeParse(textBox1, textBox2);

            return datetime.ParseStartDayTxTBox();
        }

        public DateTime ParseEndDayTxTBoxController()
        {
            datetime = new DateTimeParse(textBox1, textBox2);

            return datetime.ParseEndDayTxTBox();
        }

        public string DateTimeToUnixController(DateTime date)
        {
            datetime = new DateTimeParse(textBox1, textBox2);
            return datetime.DateTimeToUnix(date);
        }

        public DateTime ParseUnixToTimeController(double unixtime)
        {
            datetime = new DateTimeParse(textBox1, textBox2);
            return datetime.ParseUnixToTime(unixtime);
        }

        // APICONTROLLER


    }
}
