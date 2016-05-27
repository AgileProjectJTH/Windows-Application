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
    /// <summary>
    /// Interaction logic for AddNewAccount.xaml
    /// </summary>
    public partial class AddNewAccount : Window
    {
        string token = "";
        public AddNewAccount(string _token)
        {
            token = _token;
            InitializeComponent();
        }

        private void btn_AddUser_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (txt_AddPassword.ToString() != txt_AddConfirmPassword.ToString())
                {
                    System.Windows.MessageBox.Show("Password confirmation is invalid!");
                }
                else
                {
                    //Repository.StaffRepository.AddNewUser(txt_AddFirstName.ToString(), txt_AddLastName.ToString(), txt_AddUsername.ToString(), txt_AddPassword.ToString()
                    //                                        , txt_AddEmail.ToString(), txt_AddMobileNumber.ToString(), txt_AddRoomNumber.ToString(), chk_IsAdmin.ToString(), token);

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
                    

                    //string returnMessage = Repository.StaffRepository.AddNewUser(user, token); //Adds new user

                    string data = cb_chooseCorridor.Text.ToString();
                    int index = data.LastIndexOf("ID:") + "ID:".Length; //plucks out the ID        
                    string corridorId = data.Substring(index);
                    Repository.CorridorRepository.MoveUserToCorridor(user.UserName, corridorId, token); //Moves the user to the chosen corridor
                }


                Close(); //Closes the window
            }
            catch(Exception ex)
            {
                System.Windows.MessageBox.Show(ex.ToString());
            }
        }

        private void cb_chooseCorridor_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            btn_AddUser.IsEnabled = true;
        }
    }
}
