using GardenGroupLogic;
using GardenGroupModel;
using Lucene.Net.Support;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
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
    /// Interaction logic for ForgotPasswordWindow.xaml
    /// </summary>
    public partial class ForgotPasswordWindow : Window
    {
        private User user;
        private LoginWindow loginWindow;
        private UserService userService;
        public ForgotPasswordWindow(LoginWindow loginWindow)
        {
            this.loginWindow = loginWindow;
            user = new User("");
            userService = UserService.GetInstance();
            InitializeComponent();
        }

        private void resetPasswordButton_Click(object sender, RoutedEventArgs e)
        {
            if (!GetUser())
            {
                return;
            }

            if (CheckUser())
            {
                ResetPassword();
            }            
        }
        
        private bool GetUser()
        {

            if (enterEmailTextBox.Text == string.Empty || newPassword.Password == string.Empty)
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

        private bool CheckUser()
        {
            if (user == null)
            {
                errorMessageLabel.Content = ($"Invalid emailaddress or password.");
                RemoveStoredValues();
                return false;
            }
            return true;

        }
                
        private void ResetPassword() 
        {
            if (newPassword.Password != String.Empty)
            {
                byte[] salt = Security.GenerateSalt();

                user.HashedPassword = Security.HashFunction(Encoding.ASCII.GetBytes(newPassword.Password), salt);

                userService.UpdateUser(user);
                MessageBox.Show("User successfully updated");
                loginWindow.Show();
                this.Hide();
            }
            else
            {
                MessageBox.Show("Fields cannot be empty");
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            loginWindow.Show();
            this.Hide();
        }

        //Remove all input textboxes
        private void RemoveStoredValues()
        {
            enterEmailTextBox.Text = string.Empty;
            newPassword.Password = string.Empty;
        }
    }
}
