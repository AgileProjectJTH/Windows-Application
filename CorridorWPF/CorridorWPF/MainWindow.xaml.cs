using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
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
        public string token = "";

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


        private void btn_updateTeacherSchedule_Click(object sender, RoutedEventArgs e)
        {
            ScheduleTemplate schTemplate = new ScheduleTemplate(dGrid_teacherSchedule);
            schTemplate.generateHeader();    
            schTemplate.generateDays(token, null);

        }

        private void button_Click(object sender, RoutedEventArgs e)
        {

        }




        private void btn_updateStudentTv_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                TvViewStaff tvViewStd = new TvViewStaff(dGrid_StudentTv);
                List<string> listName = new List<string>();
                string data = cb_studentCorridors.Text.ToString();

                int index = data.LastIndexOf("ID:") + "ID:".Length;

                string corridorId = data.Substring(index);

                string json = Repository.StaffRepository.GetCorridorTeachers(corridorId, token);

                Models.Staffs staffs = new Models.Staffs(json);

                tvViewStd.createHeaders();
                for (int i = 0; i < staffs.staffs.Count; i++)
                {
                    listName.Add(staffs.staffs[i].username.ToString());
                }

                for (int i = 0; i < listName.Count; i++)
                {
                    string jsonn = Repository.StaffRepository.GetTeacherAvailability(listName[i], token);
                    if (jsonn == "true")
                    {
                        tvViewStd.addStaff(listName[i].ToString(), true);
                    }
                    else
                    {
                        tvViewStd.addStaff(listName[i].ToString(), false);
                    }

                }


            }
            catch(Exception ee)
            {
                System.Windows.MessageBox.Show(ee.ToString());
            }
        }

        private void btn_token_Click(object sender, RoutedEventArgs e)
        {
            //token = Repository.ScheduleRepository.getToken("Boris", "password");
            Repository.CorridorRepository.addCorridor("FredrikTest", token);
        }

        private void btn_Login_Click(object sender, RoutedEventArgs e)
        {
            token = Repository.ScheduleRepository.getToken(txt_Username.Text.ToString(), txt_Password.Text.ToString());
        }

        private void btn_AddCorridor_Click(object sender, RoutedEventArgs e)
        {
            Repository.CorridorRepository.addCorridor(txt_AddCorridor.Text.ToString(), token);
        }


        private void cb_staffCorridors_DropDownOpened(object sender, EventArgs e)
        {
            //List<Models.Corridor> list = new List<Models.Corridor>();
           
            //string json = Repository.CorridorRepository.getCorridor(token);
            //var j = JArray.Parse(json);
            //int x = 0;

            //for (int i = 0; i < j.Count(); i++)
            //{
            //    Models.Corridor corridor = JsonConvert.DeserializeObject<Models.Corridor>(j[i].ToString());
            //    cb_staffCorridors.Items.Add(corridor.corridorName.ToString());
            //}
            

        }



        private void btn_updateOtherCorridors_Click(object sender, RoutedEventArgs e)
        {
            cb_otherCorridors.Items.Clear();
            updateCorridor(cb_otherCorridors);
        }

        private void updateCorridor(ComboBox box)
        {
            try
            {
                box.Items.Clear();

                string json = Repository.CorridorRepository.getCorridor(token);
                var j = JArray.Parse(json);


                for (int i = 0; i < j.Count(); i++)
                {
                    Models.Corridor corridor = JsonConvert.DeserializeObject<Models.Corridor>(j[i].ToString());

                    box.Items.Add(corridor.corridorName.ToString() + "  ID:" + corridor.corridorId.ToString());        
                }
            }
            catch { }
        }

        private void getTeachers(ComboBox CorridorBox, ComboBox TeacherBox)
        {
            try
            {
                string data = CorridorBox.Text.ToString();

                int index = data.LastIndexOf("ID:") + "ID:".Length;

                string corridorId = data.Substring(index);

                string json = Repository.StaffRepository.GetCorridorTeachers(corridorId, token);
                Models.Staffs staffs = new Models.Staffs(json);
                for (int i = 0; i < staffs.staffs.Count; i++)
                {
                    TeacherBox.Items.Add(staffs.staffs[i].username.ToString());
                }

            }
            catch(Exception e)
            {
                System.Windows.MessageBox.Show(e.ToString());

            }

        }


        private void btn_updateOtherTeachers_Click(object sender, RoutedEventArgs e)
        {
            cb_otherTeachers.Items.Clear();
            getTeachers(cb_otherCorridors,cb_otherTeachers);
        }

        private void btn_loadOtherCorridorTeacherSchedule_Click(object sender, RoutedEventArgs e)
        {
            ScheduleTemplate sch = new ScheduleTemplate(dGrid_otherSchedule);
            sch.generateHeader();
            sch.generateDays(token, cb_otherTeachers.Text.ToString());
        }

        private void Cell_DoubleClick(object sender, MouseButtonEventArgs e)
        {
            dGrid_teacherSchedule.UnselectAll();
        }

        private void btn_updateStaffTvList_Click(object sender, RoutedEventArgs e)
        {
            cb_staffCorridors.Items.Clear();
            updateCorridor(cb_staffCorridors);
        }

        private void btn_updateStaffTv_Click(object sender, RoutedEventArgs e)
        {

            try
            {
                TvViewStaff tvViewStf = new TvViewStaff(dGrid_staffTv);
                List<string> listName = new List<string>();
                string data = cb_staffCorridors.Text.ToString();

                int index = data.LastIndexOf("ID:") + "ID:".Length;

                string corridorId = data.Substring(index);

                string json = Repository.StaffRepository.GetCorridorTeachers(corridorId, token);

                Models.Staffs staffs = new Models.Staffs(json);

                tvViewStf.createHeaders();
                for (int i = 0; i < staffs.staffs.Count; i++)
                {
                    listName.Add(staffs.staffs[i].username.ToString());
                }

                for (int i = 0; i < listName.Count; i++)
                {
                    string jsonn = Repository.StaffRepository.GetTeacherAvailability(listName[i], token);
                    if (jsonn == "true")
                    {
                        tvViewStf.addStaff(listName[i].ToString(), true);
                    }
                    else
                    {
                        tvViewStf.addStaff(listName[i].ToString(), false);
                    }
                    
                }

            }
            catch (Exception ee)
            {
                System.Windows.MessageBox.Show(ee.ToString());

            }

        }

        private void btn_updateStudentTvList_Click(object sender, RoutedEventArgs e)
        {
            cb_studentCorridors.Items.Clear();
            updateCorridor(cb_studentCorridors);
        }
    }

    

}
