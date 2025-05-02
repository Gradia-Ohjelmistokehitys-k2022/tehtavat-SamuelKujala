using LoppuHomma.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoppuHomma.Controller
{
    public class TaskC(TextBox textbox1, TextBox textbox2, ComboBox comboBox, DataGridView datagrid)
    {
        AllButtonsController? controller;

        private readonly TextBox textbox1 = textbox1;
        private readonly TextBox textbox2 = textbox2;
        private readonly ComboBox comboBox = comboBox;
        private readonly DataGridView datagrid = datagrid;

        public void CreateRowToTaskC()
        {
            datagrid.Columns.Add("Tyyppi", "Tyyppi");
            datagrid.Columns.Add("Pituus", "Pituus");
            datagrid.Columns.Add("Aloitus", "Aloituspäivä");
            datagrid.Columns.Add("Loppu", "Loppupäivä");
        }

        public void PisinLasku(BitcoinData data)
        {
            controller = new AllButtonsController(textbox1, textbox2, comboBox, datagrid);

            DateTime? Current_StartingDate = null;
            double? Current_volume = null;
            int Current_streak = 0;

            int Top_Streak = 0;
            DateTime? Top_StartingDate = null;
            DateTime? Top_EndingDate = null;


            foreach (var item in data.Prices)
            {
                DateTime timestamp = controller.ParseUnixToTimeController(item[0]);
                double volume = Convert.ToDouble(item[1]);

                if (Current_volume == null)
                {
                    Current_StartingDate = timestamp;
                    Current_volume = volume;
                }
                else if (Current_volume.HasValue)
                {
                    if (Current_volume > volume)
                    {
                        if (Top_Streak < Current_streak)
                        {
                            Top_Streak = Current_streak;
                            Top_StartingDate = Current_StartingDate;
                            Top_EndingDate = timestamp;
                        }
                        Current_volume = volume;
                        Current_streak = 0;
                        Current_StartingDate = timestamp;
                    }
                    else if (Current_volume < volume)
                    {
                        Current_streak += 1;
                        Current_volume = volume;
                    }
                }
            }
            if (Current_streak > Top_Streak)
            {
                Top_Streak = Current_streak;
                Top_StartingDate = Current_StartingDate;
                Top_EndingDate = controller.ParseUnixToTimeController(data.Prices.Last()[0]);
            }

            datagrid.Rows.Add("Hinta lasku:", Top_Streak.ToString(), Top_StartingDate?.ToString("yyyy-MM-dd"), Top_EndingDate?.ToString("yyyy-MM-dd"));
        }

        public void PisinNousu(BitcoinData data)
        {
            controller = new AllButtonsController(textbox1, textbox2, comboBox, datagrid);

            DateTime? Current_StartingDate = null;
            double? Current_volume = null;
            int Current_streak = 0;

            int Top_Streak = 0;
            DateTime? Top_StartingDate = null;
            DateTime? Top_EndingDate = null;


            foreach (var item in data.Prices)
            {
                DateTime timestamp = controller.ParseUnixToTimeController(item[0]);
                double volume = Convert.ToDouble(item[1]);

                if (Current_volume == null)
                {
                    Current_StartingDate = timestamp;
                    Current_volume = volume;
                }
                else if (Current_volume.HasValue)
                {
                    if (Current_volume < volume)
                    {
                        if (Top_Streak > Current_streak)
                        {
                            Top_Streak = Current_streak;
                            Top_StartingDate = Current_StartingDate;
                            Top_EndingDate = timestamp;
                        }
                        Current_volume = volume;
                        Current_streak = 0;
                        Current_StartingDate = timestamp;
                    }
                    else if (Current_volume > volume)
                    {
                        Current_streak += 1;
                        Current_volume = volume;
                    }
                }
            }
            if (Current_streak > Top_Streak)
            {
                Top_Streak = Current_streak;
                Top_StartingDate = Current_StartingDate;
                Top_EndingDate = controller.ParseUnixToTimeController(data.Prices.Last()[0]);
            }

            datagrid.Rows.Add("Hinta nousu:", Top_Streak.ToString(), Top_StartingDate?.ToString("yyyy-MM-dd"), Top_EndingDate?.ToString("yyyy-MM-dd"));
        }
    }
}

