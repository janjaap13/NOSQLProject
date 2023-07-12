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
using GardenGroupModel;
using GardenGroupLogic;
using System.ComponentModel;

namespace GardenGroup_Application
{
    /// <summary>
    /// Interaction logic for UserDashboard.xaml
    /// </summary>
    public partial class UserDashboard : Window
    {
        static TicketService ticketService = TicketService.GetInstance();
        private User user;
        public UserDashboard(User user)
        {
            InitializeComponent();
            LoadGrid(user);
            this.user = user;
        }

        private void LoadGrid(User user)
        {
            List<Ticket> list = ticketService.ReadUserTickets(user);
            dgTicket.ItemsSource = list;
        }
        private void OnAutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {
            //Hides the long string of _id
            PropertyDescriptor propertyDescriptor = (PropertyDescriptor)e.PropertyDescriptor;
            e.Column.Header = propertyDescriptor.DisplayName;
            if (propertyDescriptor.DisplayName == "Id")
                e.Cancel = true;
        }

        private void createButton_Click(object sender, RoutedEventArgs e)
        {
            AddTicketWindow addTicketWindow = new AddTicketWindow(user);
            this.Hide();
            addTicketWindow.ShowDialog();
            LoadGrid(user);
            this.Show();
        }

        private void editButton_Click(object sender, RoutedEventArgs e)
        {
            if (dgTicket.SelectedItem == null)
                popup.IsOpen = true;

            else
            {
                Ticket selectedTicket = (Ticket)dgTicket.SelectedItem;
                UserEditTicketWindow userEditTicketWindow = new UserEditTicketWindow(selectedTicket);
                this.Hide();
                userEditTicketWindow.ShowDialog();
                LoadGrid(user);
                this.Show();
            }
        }
        private void closeButton_Click(object sender, RoutedEventArgs e)
        {
            popup.IsOpen = false;
        }

        private void backButton_Click(object sender, RoutedEventArgs e)
        {
            LoginWindow login = new LoginWindow();
            login.Show();
            this.Close();
        }
    }
}
