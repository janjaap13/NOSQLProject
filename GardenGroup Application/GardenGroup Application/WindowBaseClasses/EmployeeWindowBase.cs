using GardenGroupLogic;
using GardenGroupModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace GardenGroup_Application.WindowBaseClasses
{
    public class EmployeeWindowBase : Window
    {
        public UserService UserService;
        public User User { get; set; }
        public int location
        {
            get { return (int)User.Location; }
            set { User.Location = (Location)value; }
        }

        public int accountType
        {
            get { return (int)User.AccountType; }
            set { User.AccountType = (AccountType)value; }
        }
        public EmployeeWindowBase(User user)
        {
            this.UserService = UserService.GetInstance();
            this.User = user;
        }

        public void UpdateUser(object sender, RoutedEventArgs e)
        {
            UserService.UpdateUser(User);
            this.Close();
        }

        public void CancelAction(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
