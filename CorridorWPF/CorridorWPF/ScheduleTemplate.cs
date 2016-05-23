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
        /// Generates the days that will be used to extract events
        /// int ammountEvents = ammount of events that is to be added
        /// </summary>
        public void generateDays(string token)
        {

            string time = "07:00:00";
            List<Models.Schedule>[] weekSchedules = new List<Models.Schedule>[5];
            int index = 0;
            int dayOfWeek = (int)DateTime.Now.AddDays(1).DayOfWeek;

            //Makes sure it is not weekend
            if (DateTime.Now.DayOfWeek.ToString() != "Saturday" && DateTime.Now.DayOfWeek.ToString() != "Sunday")
            {
                try
                {
                    for (int ii = 1 - dayOfWeek; ii < 6 - dayOfWeek; ii++)
                    {
                        string Json = Repository.ScheduleRepository.getSchedule(time, DateTime.Now.AddDays(ii).ToString("yyy-MM-dd"), token);
                        Models.Staffs staffs = new Models.Staffs(Json);

                        // Orders the list by event start times
                        List<Models.Schedule> orderdSchedule = staffs.staffs[0].schedules.OrderBy(x => x.from).ToList();

                        weekSchedules[index++] = orderdSchedule;

                    }
                    generateEvents(weekSchedules);
                }
                catch (Exception e)
                {
                    System.Windows.MessageBox.Show("Please logg in first");
                    System.Windows.MessageBox.Show(e.ToString());
                }

            }
        }

        /// <summary>
        /// Generates events for each day ranging from monday to friday
        /// </summary>
        /// <param name="schedule"></param>
        private void generateEvents(List<Models.Schedule>[] schedule)
        {
            string newRow = "\n";
            WeekDays newDay = new WeekDays();

            for (int ii = 0; ii < countMaxEvents(schedule); ii++)
            {
                //Makes sure the schedule is not empty
                if (schedule[0].Count != 0)
                {
                    newDay.Monday = schedule[0][ii].moment + newRow + schedule[0][ii].date + newRow + schedule[0][ii].from + "-" + schedule[0][ii].to + newRow + "Unavailable";
                }

                if (schedule[1].Count != 0)
                {
                    newDay.Tuesday = schedule[1][ii].moment + newRow + schedule[1][ii].date + newRow + schedule[1][ii].from + "-" + schedule[1][ii].to + newRow + "Unavailable";
                }

                if (schedule[2].Count != 0)
                {
                    newDay.Wednesday = schedule[2][ii].moment + newRow + schedule[2][ii].date + newRow + schedule[2][ii].from + "-" + schedule[2][ii].to + newRow + "Unavailable";
                }

                if (schedule[3].Count != 0)
                {
                    newDay.Thursday = schedule[3][ii].moment + newRow + schedule[3][ii].date + newRow + schedule[3][ii].from + "-" + schedule[3][ii].to + newRow + "Unavailable";
                }

                if (schedule[4].Count != 0)
                {
                    newDay.Friday = schedule[4][ii].moment + newRow + schedule[4][ii].date + newRow + schedule[4][ii].from + "-" + schedule[4][ii].to + newRow + "Unavailable";
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
                    
        }
            
        /// <summary>
        /// Returns the maximum ammount of events in the week 
        /// </summary>
        /// <param name="schedule"></param>
        /// <returns></returns>
        private int countMaxEvents(List<Models.Schedule>[] schedule)
        {
            int maxMomentDay = 0;

            for (int ii = 0; ii < schedule.Count(); ii++)
            {
                if (schedule[ii].Count() > maxMomentDay)
                {
                    maxMomentDay = schedule[ii].Count();
                }

            }
            return maxMomentDay;
        }

        /// <summary>
        /// Clears a datagrid
        /// </summary>
        public void clearGrid()
        {
            dGrid.Items.Clear();
            dGrid.Columns.Clear();
            dGrid.ItemsSource = null;
            dGrid.Items.Refresh();
        }
    }

    /// <summary>
    /// Represents the days that are to be displayed 
    /// </summary>
    public class WeekDays
    {
        public string Monday { get; set; }
        public string Tuesday { get; set; }
        public string Wednesday { get; set; }
        public string Thursday { get; set; }
        public string Friday { get; set; }
    }
}
