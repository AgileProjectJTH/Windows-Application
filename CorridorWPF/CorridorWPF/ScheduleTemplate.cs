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
            // TEST! ************************
            string roomNumber = "E2420";
            List<Models.Schedule>[] weekSchedules = new List<Models.Schedule>[5];
            int index = 0;
            int dayOfWeek = (int)DateTime.Now.AddDays(1).DayOfWeek;
            //*******************************

            //Makes sure it is not weekend
            if (/*DateTime.Now.DayOfWeek.ToString() != "Saturday" && DateTime.Now.DayOfWeek.ToString() != "Sunday"*/true)
            {
                for (int ii = 1 - dayOfWeek; ii < 6 - dayOfWeek; ii++)
                {
                    string Json = Repository.ScheduleRepository.getSchedule(roomNumber, DateTime.Now.AddDays(ii).ToString("yyy-MM-dd"));
                    Models.Staffs staffs = new Models.Staffs(Json);

                    // Orders the list by event start times
                    List<Models.Schedule> orderdSchedule = staffs.staffs[0].schedules.OrderBy(x => x.from).ToList();

                    weekSchedules[index] = orderdSchedule;

                    index++;
                }
                generateEvents(weekSchedules);
            }       
        }

        private void generateEvents(List<Models.Schedule>[] schedule)
        {
            string newRow = "\n";
            WeekDays newDay = new WeekDays();

            //Makes sure the schedule is not empty
            if (schedule[0].Count != 0)
            {
                newDay.Monday = schedule[0][0].moment + newRow + schedule[0][0].date + newRow + schedule[0][0].from + "-" + schedule[0][0].to + newRow + "Unavailable";
            }
            else
            {
                newDay.Monday = "Available";
            }

            if (schedule[1].Count != 0)
            {
                newDay.Tuesday = schedule[1][0].moment + newRow + schedule[1][0].date + newRow + schedule[1][0].from + "-" + schedule[1][0].to + newRow + "Unavailable";
            }
            else
            {
                newDay.Tuesday = "Available";
            }

            if (schedule[2].Count != 0)
            {
                newDay.Wednesday = schedule[2][0].moment + newRow + schedule[2][0].date + newRow + schedule[2][0].from + "-" + schedule[2][0].to + newRow + "Unavailable";
            }
            else
            {
                newDay.Wednesday = "Available";
            }

            if (schedule[3].Count != 0)
            {
                newDay.Thursday = schedule[3][0].moment + newRow + schedule[3][0].date + newRow + schedule[3][0].from + "-" + schedule[3][0].to + newRow + "Unavailable";
            }
            else
            {
                newDay.Thursday = "Available";
            }

            if (schedule[4].Count != 0)
            {
                newDay.Friday = schedule[4][0].moment + newRow + schedule[4][0].date + newRow + schedule[4][0].from + "-" + schedule[4][0].to + newRow + "Unavailable";
            }
            else
            {
                newDay.Friday = "Available";
            }


            dGrid.Items.Add(new WeekDays
                {
                    Monday = newDay.Monday,
                    Tuesday = newDay.Tuesday,
                    Wednesday = newDay.Wednesday,
                    Thursday = newDay.Thursday,
                    Friday = newDay.Friday
                }); //Adds all the row days data

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
