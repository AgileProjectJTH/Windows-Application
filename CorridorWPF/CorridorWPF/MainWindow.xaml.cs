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

            
            lstv_staffTv.Items.Add("Anders Göransson");
            
           
        }
    }

    public class SubListView : ListView
    {
        protected override void
            PrepareContainerForItemOverride(DependencyObject element,
            object item)
        {
            base.PrepareContainerForItemOverride(element, item);
            if (View is GridView)
            {
                int index = ItemContainerGenerator.IndexFromContainer(element);
                ListViewItem lvi = element as ListViewItem;
                if (index % 2 == 0)
                {
                    lvi.Background = Brushes.LightBlue;
                }
                else
                {
                    lvi.Background = Brushes.Beige;
                }
            }
        }
    }


}
