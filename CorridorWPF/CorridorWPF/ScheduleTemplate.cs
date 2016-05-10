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
            string roomNr = "E2420";
            string date = "2016-04-25";
            //Models.Staffs staffs = new Models.Staffs(Repository.ScheduleRepository.getSchedule(roomNr, date));
            string Json = Repository.ScheduleRepository.getSchedule(roomNr, date);
            Models.Staffs staffs = new Models.Staffs(Json);
            //-----------------------



            clearGrid();

            string[] weekDays = new string[2] { "Monday", "Tuesday" /*"Wednesday", "Thursday", "Friday"*/ };

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
            string föreläsning = "Objektorienterad piss";
            string sal = "E5540";
            string nyRad = "\n";
            string beginTime = (5).ToString();
            string endTime = ((5) + 2).ToString();

            DataGridCell newCell = new DataGridCell();
            DataGridRow newRow = new DataGridRow();
            WeekDays newDay = new WeekDays();


            newDay.Monday = föreläsning + nyRad + sal + nyRad + beginTime + ":" + endTime;
            newDay.Tuesday = "Tisdag data";

            //newCell.Content = newDay.Monday;//newDay.Monday;
            //newCell.Background = Brushes.Red;

            //newRow.Item = newCell;
          
            //dGrid.Items.Add(newRow);

            dGrid.Items.Add(new WeekDays { Monday = newDay.Monday, Tuesday = newDay.Tuesday}); //Adds all the row days data


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
        //public string Wednesday { get; set; }
        //public string Thursday { get; set; }
        //public string Friday { get; set; }
    }
}
