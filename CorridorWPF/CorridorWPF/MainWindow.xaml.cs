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

    public class NameToBrushConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            string input = value as string;
            switch (input)
            {
                case "John":
                    return Brushes.LightGreen;
                default:
                    return Brushes.LightGreen;
                    //return DependencyProperty.UnsetValue;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotSupportedException();
        }
    }


    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        public MainWindow()
        {
            InitializeComponent();
        }

        private void dataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void listBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void btn_toggleAvailable_Click(object sender, RoutedEventArgs e)
        {
            // Check if button is green
            if (btn_toggleAvailable.Background == Brushes.LimeGreen)
            {
                // Set Toggle Available button color and change content
                btn_toggleAvailable.Background = Brushes.Red;
                btn_toggleAvailable.Content = "Unavailable";

                // Set "Set time" button color
                btn_setTime.Background = Brushes.LimeGreen;

                // Set background color
                bdr_availability.Background = Brushes.LimeGreen;

                // Change status text
                txt_Availability.Text = "You are available";

            }
            else
            {
                // Set Toggle Available button color and change content
                btn_toggleAvailable.Background = Brushes.LimeGreen;
                btn_toggleAvailable.Content = "Available";

                // Set "Set time" button color
                btn_setTime.Background = Brushes.Red;

                // Set background color
                bdr_availability.Background = Brushes.Red;

                // Change status text
                txt_Availability.Text = "You are unavailable";
            }
        }


        private void btn_setTime_Click(object sender, RoutedEventArgs e) //When the setTime button is clicked
        {
            btn_setTime.IsEnabled = false; //setTime button is disabled
        }

        private void cb_selectTime_DropDownClosed(object sender, EventArgs e) //When the combobox closes AKA an item has been chosen from the time list
        {
            btn_setTime.IsEnabled = true; //setTime button is enabled
        }

        private void btn_addNewAccount_Click(object sender, RoutedEventArgs e)
        {
            AddNewAccount AddNewAccountWindow = new AddNewAccount();
            AddNewAccountWindow.Show();
        }

        private void dGrid_teacherSchedule_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void btn_updateTeacherSchedule_Click(object sender, RoutedEventArgs e)
        {
            ScheduleTemplate schTemplate = new ScheduleTemplate(dGrid_teacherSchedule);
            schTemplate.generateHeader();

            schTemplate.generateDays(10);

          
                
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {

        }
    }

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

            for (int ii = 0; ii < 5; ii++)
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
        public void generateDays(int ammountEvents)
        {         
            string föreläsning = "Objektorienterad piss";
            string sal = "E5540";
            string newRow = "\n";
            string beginTime = (5).ToString();
            string endTime = ((5) + 2).ToString();
            for (int ii = 0; ii < ammountEvents + 1; ii++)
            {
                
                dGrid.Items.Add(new WeekDays()
                {                 
                    Monday = föreläsning + newRow + sal + newRow + beginTime + "-" + endTime,
                    Tuesday = föreläsning + newRow + sal + newRow + beginTime + "-" + endTime,
                    Wednesday = föreläsning + newRow + sal + newRow + beginTime + "-" + endTime,
                    Thursday = föreläsning + newRow + sal + newRow + beginTime + "-" + endTime,
                    Friday = föreläsning + newRow + sal + newRow + beginTime + "-" + endTime
                });
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
