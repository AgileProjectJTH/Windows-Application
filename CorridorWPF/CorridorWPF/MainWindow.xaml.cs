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
            string[] weekDays = new string[5] { "Monday", "Tuesday", "Wednesday", "Thursday", "Friday" };


            for (int ii = 0; ii < 10; ii++)
            {
                string föreläsning = "Objektorienterad piss";
                string sal = "E5540";
                string newRow = "\n";
                string beginTime = (ii + 5).ToString();
                string endTime = ((ii + 5) + 2).ToString();

                DataGridTextColumn c1 = new DataGridTextColumn();
                c1.Header = weekDays[ii%5];
                c1.Binding = new Binding(weekDays[ii%5]);
                c1.Width = (dGrid_teacherSchedule.Width/5);
                dGrid_teacherSchedule.Columns.Add(c1);


                
                dGrid_teacherSchedule.Items.Add(new WeekDays() { Monday = föreläsning + newRow + sal + newRow + beginTime + "-" + endTime,
                                                             Tuesday = föreläsning + newRow + sal + newRow + beginTime + "-" + endTime,
                                                             Wednesday = föreläsning + newRow + sal + newRow + beginTime + "-" + endTime,
                                                             Thursday = föreläsning + newRow + sal + newRow + beginTime + "-" + endTime,
                                                             Friday = föreläsning + newRow + sal + newRow + beginTime + "-" + endTime});
                
                

              
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
}
