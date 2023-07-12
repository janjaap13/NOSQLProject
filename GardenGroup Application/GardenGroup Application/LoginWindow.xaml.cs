using GardenGroupLogic;
using GardenGroupModel;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Runtime.InteropServices;
using System.Security;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace GardenGroup_Application
{
    /// <summary>
    /// Interaction logic for LoginWindow.xaml
    /// </summary>
    public partial class LoginWindow : Window
    {
        private User user;
        private UserService userService;
        public LoginWindow()
        {
            InitializeComponent();
            user = new User("");
            userService = UserService.GetInstance();
        }

        private void loginButton_Click(object sender, RoutedEventArgs e)
        {
            if (!GetUser())
            {
                return;
            }

            if (CheckUser())
            {
                if (user.AccountType == AccountType.Employee)
                {
                    UserDashboard userDashboard = new UserDashboard(user);
                    userDashboard.Show();
                }
                else
                {
                    EmployeeDashboard employeeDashboard = new EmployeeDashboard();
                    employeeDashboard.Show();
                }
                this.Close();
                RemoveStoredValues();
            }
        }

        //Checks if login textboxes are empty and tries to retrieve user from database
        private bool GetUser()
        {

            if (enterEmailTextBox.Text == string.Empty || pwdBox.Password == string.Empty)
            {
                errorMessageLabel.Content = ("Please enter credentials!");
                return false;
            }

            try
            {
                user = userService.ReadUserByEmail(enterEmailTextBox.Text);
                return true;
            }
            catch (Exception)
            {
                errorMessageLabel.Content = ("Invalid employeenumber or password!");
                RemoveStoredValues();
                return false;
            }
        }

        //Checks if passwords match and if user rights are valid
        private bool CheckUser()
        {
            if (user == null)
            {

                errorMessageLabel.Content = ($"Invalid emailaddress or password.");
                RemoveStoredValues();
                return false;
            }
                    
            if (user.AccountType == AccountType.Servicedesk || user.AccountType == AccountType.Employee)
            {

                if (BitConverter.ToString(user.HashedPassword) != BitConverter.ToString(Security.HashFunction(Encoding.ASCII.GetBytes(pwdBox.Password), user.Salt)))
                {
                    errorMessageLabel.Content = ($"Invalid emailaddress or password.");
                    RemoveStoredValues();
                    return false;
                }

               return true;
            }       
            return false;

        }

        //Remove all input textboxes
        private void RemoveStoredValues()
        {
            enterEmailTextBox.Text = string.Empty;
            pwdBox.Password = string.Empty;
        }
        private void resetPasswordButton_Click(object sender, RoutedEventArgs e)
        {
            new ForgotPasswordWindow(this).Show();
            this.Hide();
        }
    }
}