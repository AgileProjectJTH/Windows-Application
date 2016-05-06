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



        private void btn_supdateStaffTv_Click(object sender, RoutedEventArgs e)
        {

            TvViewStaff tvViewStf = new TvViewStaff(dGrid_staff);

            tvViewStf.createHeaders();
            for (int ii = 0; ii < 2; ii++)
            {
                tvViewStf.addStaff("Göran Andersson", true);

                tvViewStf.addStaff("Anna Skog", false);

                tvViewStf.addStaff("Johan Carlsson", false);

                tvViewStf.addStaff("Margareta Andersson", true);
            }

            TvViewStaffNotes tvViewStfNote = new TvViewStaffNotes(dGrid_staffNotes);

            tvViewStfNote.createHeader();

            tvViewStfNote.addNote("In his office");
            //for (int ii = 0; ii < 2; ii++)
            //{
            //    tvViewStfNote.addNote("In his office");

            //    //tvViewStfNote.addNote("Anna Skog", "Away for lunch");

            //    //tvViewStfNote.addNote("Johan Carlsson", "Home for sick child");

            //    //tvViewStfNote.addNote("Margareta Andersson", "");
            //}

        }

        private void btn_updateStudentTv_Click(object sender, RoutedEventArgs e)
        {
            TvViewStudents tvViewStud = new TvViewStudents(dGrid_Student);

            tvViewStud.createHeaders();
            for (int ii = 0; ii < 2; ii++)
            {
                tvViewStud.addStaff("Göran Andersson", true);

                tvViewStud.addStaff("Anna Skog", false);

                tvViewStud.addStaff("Johan Carlsson", false);

                tvViewStud.addStaff("Margareta Andersson", true);
            }


        }
    }

    

}
