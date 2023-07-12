using GardenGroupModel;
using GardenGroupLogic;
using System;
using System.Collections.Generic;
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
using System.Windows.Shapes;

namespace GardenGroup_Application
{
    /// <summary>
    /// Interaction logic for RegisterNewUserWindow.xaml
    /// </summary>
    public partial class RegisterNewUserWindow : Window
    {
        private LoginWindow loginWindow;
        private UserService userService;
        private EmployeeDashboard employeeDashboard;

        public RegisterNewUserWindow()
        {
            loginWindow = new LoginWindow();
            employeeDashboard = new EmployeeDashboard();
            userService = UserService.GetInstance();
            InitializeComponent();
        }

        private void AddEmployee_Click(object sender, RoutedEventArgs e)
        {
            AccountType accountType = (AccountType)functionComboBox.SelectedIndex;
            Location location = (Location)locationComboBox.SelectedIndex;
            try
            {
                if (!enterEmail1.Text.Contains("@"))
                {
                    errorMsg.Content = "Invalid input, please use an '@' in your email";
                    return;
                }
                if (userService.ReadUserByEmail(enterEmail1.Text) != null)
                {
                    errorMsg.Content = ("Emailaddress already exists.");
                    return;
                }
                if (!ComparePasswords())
                {
                    errorMsg.Content = "Passwords do not match.";
                    return;
                }
                AddUserFields(accountType, location);
                MessageBox.Show("User created successfully");
                loginWindow.Show();
                this.Close();
                employeeDashboard.Show();
            }
            catch (Exception)
            {

            }
        }

        private bool ComparePasswords()

        {
            if (pwdBox.Password == pwdBoxRepeat.Password)
                return true;
            else
                return false;
        }

        private void AddUserFields(AccountType accountType, Location location)
        {
            List<User> allUsers = userService.LoadWithoutPass();
            int newUserID = allUsers.Count + 1;
            byte[] salt = Security.GenerateSalt();
            userService.AddUser("LogIn", new User
                 (
                  newUserID,
                  enterFirstnameTextBox.Text,
                  enterLastnameTextBox.Text,
                  int.Parse(enterphonenr_Copy.Text),
                  location,
                  accountType,
                  enterEmail1.Text,
                  Security.HashFunction(Encoding.ASCII.GetBytes(pwdBox.Password), salt),
                  salt
                 )
                );
        }

        private void CancelUserRegistrationButton_Click(object sender, RoutedEventArgs e)
        {
            employeeDashboard.Show();
            this.Hide();
        }
    }
}
