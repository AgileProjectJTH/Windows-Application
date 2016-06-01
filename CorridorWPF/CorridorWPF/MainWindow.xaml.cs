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
        public bool isAvailable = true;
        public bool fullScreen = false;
        public double currentPosition = 0;

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
            if (btn_toggleAvailable.Background == Brushes.LightGreen)
            {
                // Set Toggle Available button color and change content
                btn_toggleAvailable.Background = Brushes.Salmon;
                btn_toggleAvailable.Content = "Unavailable";

                // Set "Set time" button color
                btn_setTime.Background = Brushes.LightGreen;

                // Set background color
                bdr_availability.Background = Brushes.LightGreen;

                // Content of set time button
                btn_setTime.Content = "Set Time Available";

                // Sets the global variable to true
                isAvailable = true;

            }
            else
            {
                // Set Toggle Available button color and change content
                btn_toggleAvailable.Background = Brushes.LightGreen;
                btn_toggleAvailable.Content = "Available";

                // Set "Set time" button color
                btn_setTime.Background = Brushes.Salmon;

                // Set background color
                bdr_availability.Background = Brushes.Salmon;

                // Content of set time button
                btn_setTime.Content = "Set Time Unavailable";

                // Sets the global variable to false
                isAvailable = false;
            }
        }


        private void btn_setTime_Click(object sender, RoutedEventArgs e) //When the setTime button is clicked
        {
            btn_setTime.IsEnabled = false; //setTime button is disabled
            DateTime dateAndTime = DateTime.Now; //Date and time right now
            DateTime dateAndTimeExtendedTo = dateAndTime.Add(TimeSpan.Parse(cb_selectTime.Text.ToString())); //Creates new date and time extended with the time selected

            Models.ScheduleModel scheduleModel = new Models.ScheduleModel(); //New model to send for the selected time a availablity
            scheduleModel.available = isAvailable; 
            scheduleModel.fromDateAndTime = dateAndTime.ToString("yyy-MM-dd HH:mm:ss"); //Todays date
            scheduleModel.toDateAndTime = dateAndTimeExtendedTo.ToString("yyy-MM-dd HH:mm:ss"); //Time when select time has passed

            Repository.StaffRepository.setTeacherAvailability(scheduleModel, token);
            
        }

        private void cb_selectTime_DropDownClosed(object sender, EventArgs e) //When the combobox closes AKA an item has been chosen from the time list
        {
            btn_setTime.IsEnabled = true; //setTime button is enabled
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
            Repository.ScheduleRepository.LoadTvView(dGrid_StudentTv,cb_studentCorridors, token);
        }

        private void btn_token_Click(object sender, RoutedEventArgs e)
        {
            //token = Repository.ScheduleRepository.getToken("Boris", "password");
            Repository.CorridorRepository.addCorridor("FredrikTest", token);
        }

        private void btn_Login_Click(object sender, RoutedEventArgs e)
        {
            token = Repository.ScheduleRepository.getToken(txt_Username.Text.ToString(), txt_Password.Text.ToString());

            if (Repository.StaffRepository.GetTeacherAvailability(txt_Username.Text.ToString(), token) == "true")
            {
                // Set Toggle Available button color and change content
                btn_toggleAvailable.Background = Brushes.Salmon;
                btn_toggleAvailable.Content = "Unavailable";

                // Set "Set time" button color
                btn_setTime.Background = Brushes.LightGreen;

                // Set background color
                bdr_availability.Background = Brushes.LightGreen;

                // Content of set time button
                btn_setTime.Content = "Set Time Available";

                // Sets the global variable to true
                isAvailable = true;
            }
            else
            {
                // Set Toggle Available button color and change content
                btn_toggleAvailable.Background = Brushes.LightGreen;
                btn_toggleAvailable.Content = "Available";

                // Set "Set time" button color
                btn_setTime.Background = Brushes.Salmon;

                // Set background color
                bdr_availability.Background = Brushes.Salmon;

                // Content of set time button
                btn_setTime.Content = "Set Time Unavailable";

                // Sets the global variable to false
                isAvailable = false;
            }
        }

        private void btn_AddCorridor_Click(object sender, RoutedEventArgs e)
        {
            Repository.CorridorRepository.addCorridor(txt_AddCorridor.Text.ToString(), token);
            txt_AddCorridor.Clear();
        }


        private void cb_staffCorridors_DropDownOpened(object sender, EventArgs e)
        {
            cb_staffCorridors.Items.Clear();
            updateCorridor(cb_staffCorridors);
        }





        private void updateCorridor(ComboBox box)
        {
            try
            {
                box.Items.Clear();

                string json = Repository.CorridorRepository.getAllCorridors(token);
                var j = JArray.Parse(json);


                for (int i = 0; i < j.Count(); i++)
                {
                    Models.Corridor corridor = JsonConvert.DeserializeObject<Models.Corridor>(j[i].ToString());

                    box.Items.Add(corridor.corridorName.ToString() + "  ID:" + corridor.corridorId.ToString());        
                }
            }
            catch { }
        }

        private void getTeachersCombobox(ComboBox CorridorBox, ComboBox TeacherBox)
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

        
        private void getTeachersList(ComboBox box,ListBox list)
        {
            try
            {
                string data = box.Text.ToString();

                int index = data.LastIndexOf("ID:") + "ID:".Length;

                string corridorId = data.Substring(index);

                string json = Repository.StaffRepository.GetCorridorTeachers(corridorId, token);
                Models.Staffs staffs = new Models.Staffs(json);
                for (int i = 0; i < staffs.staffs.Count; i++)
                {
                    list.Items.Add(staffs.staffs[i].firstname.ToString() + " " + staffs.staffs[i].lastname.ToString() + " Username: " + staffs.staffs[i].username.ToString());
                }

            }
            catch (Exception e)
            {
                System.Windows.MessageBox.Show(e.ToString());
            }
        }


        private void btn_loadOtherCorridorTeacherSchedule_Click(object sender, RoutedEventArgs e)
        {
            if (cb_otherTeachers.Text == "")
            {
                System.Windows.MessageBox.Show("Please choose a staffmember");
            }
            else
            {
                ScheduleTemplate sch = new ScheduleTemplate(dGrid_otherSchedule);
                sch.generateHeader();
                sch.generateDays(token, cb_otherTeachers.Text.ToString());
            }
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

            Repository.ScheduleRepository.LoadTvView(dGrid_staffTv, cb_staffCorridors, token);

        }

        private void btn_updateStudentTvList_Click(object sender, RoutedEventArgs e)
        {
            cb_studentCorridors.Items.Clear();
            updateCorridor(cb_studentCorridors);
        }

        private void btn_updateUsers_Click(object sender, RoutedEventArgs e)
        {
            updateCorridor(cb_teacherList);
        }


        private void btn_updateTeacherList_Click(object sender, RoutedEventArgs e)
        {
            list_teachersInCorridor.Items.Clear();
            getTeachersList(cb_teacherList, list_teachersInCorridor);
        }


        private void cb_otherCorridors_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            cb_otherTeachers.Items.Clear();
        }

        private void btn_AddUser_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (txt_AddPassword.ToString() != txt_AddConfirmPassword.ToString())
                {
                    System.Windows.MessageBox.Show("Password confirmation is invalid!");
                }
                else if(cb_chooseCorridor.Text.ToString() == "")
                {
                    System.Windows.MessageBox.Show("Please choose a corridor");
                }
                else
                {

                    Models.User user = new Models.User();
                    user.firstname = txt_AddFirstName.Text.ToString();
                    user.lastname = txt_AddLastName.Text.ToString();
                    user.UserName = txt_AddUsername.Text.ToString();
                    user.email = txt_AddEmail.Text.ToString();
                    user.roomNr = txt_AddRoomNumber.Text.ToString();
                    user.Password = txt_AddPassword.Text.ToString();
                    user.ConfirmPassword = txt_AddConfirmPassword.Text.ToString();
                    user.mobile = txt_AddMobileNumber.Text.ToString();
                    user.isAdmin = chk_IsAdmin.IsChecked.Value;


                    string returnMessage = Repository.StaffRepository.AddNewUser(user, token); //Adds new user

                    string data = cb_chooseCorridor.Text.ToString();
                    int index = data.LastIndexOf("ID:") + "ID:".Length; //plucks out the ID        
                    string corridorId = data.Substring(index);
                    Repository.CorridorRepository.MoveUserToCorridor(user.UserName, corridorId, token); //Moves the user to the chosen corridor

                    txt_AddConfirmPassword.Text = null;
                    txt_AddCorridor.Text = null;
                    txt_AddEmail.Text = null;
                    txt_AddFirstName.Text = null;
                    txt_AddLastName.Text = null;
                    txt_AddMobileNumber.Text = null;
                    txt_AddPassword.Text = null;
                    txt_AddRoomNumber.Text = null;
                    txt_AddUsername.Text = null;
                    txt_Password.Text = null;
                    txt_Username.Text = null;
                    cb_chooseCorridor.Items.Clear();
                }


               
            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show(ex.ToString());
            }
        }

        private void cb_chooseCorridor_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            btn_AddUser.IsEnabled = true;
        }

        private void cb_chooseCorridor_DropDownOpened(object sender, EventArgs e)
        {
            updateCorridor(cb_chooseCorridor);
        }

        private void cb_teacherList_DropDownOpened(object sender, EventArgs e)
        {
            updateCorridor(cb_teacherList);
            list_teachersInCorridor.Items.Clear();
        }

        private void cb_studentCorridors_DropDownOpened(object sender, EventArgs e)
        {
            cb_studentCorridors.Items.Clear();
            updateCorridor(cb_studentCorridors);
        }

        private void btn_deleteSelectedUser_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult messageBoxResult = System.Windows.MessageBox.Show("Are you sure you want to delete this user?", "Delete Confirmation", System.Windows.MessageBoxButton.YesNo);
            if (messageBoxResult == MessageBoxResult.Yes)
            {
                string user = list_teachersInCorridor.SelectedValue.ToString();

                int index = user.LastIndexOf("Username: ") + "Username: ".Length;

                string username = user.Substring(index);

                Repository.StaffRepository.deleteUser(username, token);

                list_teachersInCorridor.Items.Clear();
                getTeachersList(cb_teacherList, list_teachersInCorridor);
            }
        }

        private void cb_teacherListDelete_DropDownOpened(object sender, EventArgs e)
        {
            updateCorridor(cb_corridorListDelete);
        }

        private void btn_deleteCorridor_Click(object sender, RoutedEventArgs e)
        {
            if (cb_corridorListDelete.Text.ToString() != "")
            {
                MessageBoxResult messageBoxResult = System.Windows.MessageBox.Show("Are you sure you want to delete this corridor? \n All users in it will also be deleted", "Delete Confirmation", System.Windows.MessageBoxButton.YesNo);
                if (messageBoxResult == MessageBoxResult.Yes)
                {
                    List<string> staff = new List<string>();

                    staff = Repository.CorridorRepository.getStaffInCorridor(token, cb_corridorListDelete);

                    for (int i = 0; i < staff.Count; i++)
                    {
                        Repository.StaffRepository.deleteUser(staff[i], token);
                    }


                    string data = cb_corridorListDelete.Text.ToString();
                    int index = data.LastIndexOf("ID:") + "ID:".Length; //plucks out the ID        
                    string corridorId = data.Substring(index);
                    Repository.CorridorRepository.deleteCorridor(corridorId, token);
                    cb_corridorListDelete.Items.Clear();
                }
            }

        }

        private void cb_otherCorridors_DropDownOpened(object sender, EventArgs e)
        {
            cb_otherCorridors.Items.Clear();
            updateCorridor(cb_otherCorridors);
            cb_otherTeachers.IsEnabled = false;
        }

        private void cb_otherTeachers_DropDownOpened(object sender, EventArgs e)
        {
            cb_otherTeachers.Items.Clear();
            getTeachersCombobox(cb_otherCorridors, cb_otherTeachers);
            if(cb_otherTeachers.Items.Count == 0)
            {
                cb_otherTeachers.IsEnabled = false;
            }
        }

        private void cb_otherCorridors_DropDownClosed(object sender, EventArgs e)
        {
            cb_otherTeachers.Items.Clear();

        }

        private void cb_otherCorridors_MouseLeave(object sender, MouseEventArgs e)
        {
            if (cb_otherCorridors.Text == "")
            {
                cb_otherTeachers.IsEnabled = false;
            }
            else
            {
                cb_otherTeachers.IsEnabled = true;
            }
        }

        private void dGrid_StudentTv_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if(!fullScreen)
            {
                //DependencyObject parent = VisualTreeHelper.GetParent(dGrid_StudentTv);
                studentCanvas.Children.Remove(dGrid_StudentTv);
                this.WindowStyle = WindowStyle.None;
                this.Content = dGrid_StudentTv;           
                this.WindowState = WindowState.Maximized;
                

            }
            else
            {
                this.Content = null;
                studentCanvas.Children.Add(dGrid_StudentTv);
                this.WindowStyle = WindowStyle.SingleBorderWindow;
                this.WindowState = WindowState.Normal;
            }
            fullScreen = !fullScreen;
        }

        private void btn_studentTvFullscreen_Click(object sender, RoutedEventArgs e)
        {
            StudentTvFullscreen StudentFullWindow = new StudentTvFullscreen(cb_studentCorridors,token);
            StudentFullWindow.Show();
            StudentFullWindow.intialScheduleLoad();
        }

        private void btn_addNote_Click(object sender, RoutedEventArgs e)
        {
            TVStaffNote rawNote = new TVStaffNote();
            TVStaffNote note = new TVStaffNote();
            var builder = new StringBuilder();
            TvViewStaffNotes staffNotes = new TvViewStaffNotes(dGrid_staffTvNotes);
            staffNotes.createHeader();


            rawNote.Notes = txtBox_notes.Text.ToString();

            for (int i = 0; i < txtBox_notes.LineCount; i++)
            {
                builder.Append(txtBox_notes.GetLineText(i));
                builder.Append(System.Environment.NewLine);
            }


            note.Notes = builder.ToString();

            if (note.Notes != "")
            {       
                staffNotes.addNote(note.Notes);
                txtBox_notes.Clear();
            }
            

        }

        private void btn_clearNotes_Click(object sender, RoutedEventArgs e)
        {


            TvViewStaffNotes staffNotes = new TvViewStaffNotes(dGrid_staffTvNotes);

            staffNotes.clearGrid();
        }
    }

    

}
