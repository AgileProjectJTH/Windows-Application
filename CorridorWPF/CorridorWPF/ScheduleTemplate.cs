using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace CorridorWPF
{
    public class ScheduleTemplate
    {
        DataGrid dGrid;

        public ScheduleTemplate(DataGrid _dGrid)
        {
            dGrid = _dGrid;
        }

        /// <summary>
        /// Generate headers for a data grid, only the five first days are generated
        /// </summary>
        public void generateHeader()
        {
            //Mockup
            //-----------------------
            //string roomNr = "E2420";
            //string date = "2016-04-29";
            //Models.Staffs staffs = new Models.Staffs(Repository.ScheduleRepository.getSchedule(roomNr, date));
            //string Json = Repository.ScheduleRepository.getSchedule(roomNr, date);
            //Models.Staffs staffs = new Models.Staffs(Json);

            //List<Models.Schedule> ordnatSchedule = staffs.staffs[0].schedules.OrderBy(x=>x.from).ToList();

            //Models.Schedule theSchedule = new Models.Schedule(Json);

            

            //-----------------------
            
           

            clearGrid();

            string[] weekDays = new string[5] { "Monday", "Tuesday", "Wednesday", "Thursday", "Friday" };

            for (int ii = 0; ii < weekDays.Length; ii++)
            {
                DataGridTextColumn gridColumn = new DataGridTextColumn();
                gridColumn.Header = weekDays[ii];
                gridColumn.Binding = new Binding(weekDays[ii]);
                gridColumn.Width = (dGrid.Width / 5);
                dGrid.Columns.Add(gridColumn);
            }


        }

        /// <summary>
        /// Generate events in the first five days of the week
        /// int ammountEvents = ammount of events that is to be added
        /// </summary>
        public void generateDays()
        {
            // TEST!
            string roomNumber = "E2420";
            string date = "2016-04-28";

            string newRow = "\n";

            WeekDays newDay = new WeekDays();

            string Json = Repository.ScheduleRepository.getSchedule(roomNumber, date);
            Models.Staffs staffs = new Models.Staffs(Json);

            // Orders the list by event start times
            List<Models.Schedule> orderdSchedule = staffs.staffs[0].schedules.OrderBy(x => x.from).ToList();


            if (orderdSchedule != null)
            {
                for (int ii = 0; ii < orderdSchedule.Count; ii++)
                {
                    newDay.Monday = orderdSchedule[ii].moment + newRow + orderdSchedule[ii].date + newRow + orderdSchedule[ii].from + "-" + orderdSchedule[ii].to + newRow + "Unavailable";
                    newDay.Tuesday = orderdSchedule[ii].moment + newRow + orderdSchedule[ii].date + newRow + orderdSchedule[ii].from + "-" + orderdSchedule[ii].to + newRow + "Unavailable";
                    newDay.Wednesday = orderdSchedule[ii].moment + newRow + orderdSchedule[ii].date + newRow + orderdSchedule[ii].from + "-" + orderdSchedule[ii].to + newRow + "Unavailable";
                    newDay.Thursday = orderdSchedule[ii].moment + newRow + orderdSchedule[ii].date + newRow + orderdSchedule[ii].from + "-" + orderdSchedule[ii].to + newRow + "Unavailable";
                    newDay.Friday = orderdSchedule[ii].moment + newRow + orderdSchedule[ii].date + newRow + orderdSchedule[ii].from + "-" + orderdSchedule[ii].to + newRow + "Unavailable";



                    dGrid.Items.Add(new WeekDays
                    {
                        Monday = newDay.Monday,
                        Tuesday = newDay.Tuesday,
                        Wednesday = newDay.Wednesday,
                        Thursday = newDay.Thursday,
                        Friday = newDay.Friday
                    }); //Adds all the row days data
                }
            }





        }

        public void clearGrid()
        {
            dGrid.Items.Clear();
            dGrid.Columns.Clear();
            dGrid.ItemsSource = null;
            dGrid.Items.Refresh();
        }




    }

    public class WeekDays
    {
        public string Monday { get; set; }
        public string Tuesday { get; set; }
        public string Wednesday { get; set; }
        public string Thursday { get; set; }
        public string Friday { get; set; }
    }
}
