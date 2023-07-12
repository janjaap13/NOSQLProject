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
using System.Windows.Controls.Primitives;
using MongoDB.Bson;

namespace GardenGroup_Application
{
    /// <summary>
    /// Interaction logic for EmployeeDashboard.xaml
    /// </summary>
    public partial class EmployeeDashboard : Window
    {
        static TicketService ticketService = TicketService.GetInstance();
        static UserService userService = UserService.GetInstance();
        private List<Ticket> allTickets { get; set; }
        private List<User> allUsers { get; set; }
    
        public EmployeeDashboard()
        {
            InitializeComponent();
            allTickets = ticketService.ReadEmployeeTickets();
            allUsers = userService.LoadWithoutPass();
            ShowDashboard();
        }

        private void ShowDashboard()
        {
            HideGrid();
            titleLabel.Content = "Dashboard";
            CalculateStatistics();
        }

        private void CalculateStatistics()
        {
            int unsolvedTickets = 0;
            int pastDeadline = 0;
            foreach (Ticket ticket in allTickets)
            {
                if (ticket.Status == Status.Closed)
                    unsolvedTickets++;
                
                if(ticket.Deadline < 0)
                    pastDeadline++;
            }
            unsolvedButton.Content = $"{unsolvedTickets} / {allTickets.Count}";
            deadlineButton.Content = $"{pastDeadline}";
        }

        private void userButton_Click(object sender, RoutedEventArgs e)
        {
            //Loads in all users after clicking the 'User Management' button
            HideDashboard();
            titleLabel.Content = "User Management";
            dgEmployee.ItemsSource = allUsers;
        }
        private void ticketButton_Click(object sender, RoutedEventArgs e)
        {
            LoadTickets();
        }
        private void LoadTickets()
        {
            //Loads in all tickets 
            HideDashboard();
            newButton.Visibility = Visibility.Hidden;
            titleLabel.Content = "Ticket Overview";
            dgEmployee.ItemsSource = allTickets;
        }
        public void HideGrid()
        {
            newButton.Visibility = Visibility.Hidden;
            editButton.Visibility = Visibility.Hidden;
            dgEmployee.Visibility = Visibility.Hidden;
            deadlineButton.Visibility = Visibility.Visible;
            unsolvedButton.Visibility = Visibility.Visible;
            unsolvedLabel.Visibility = Visibility.Visible;
            deadlineLabel.Visibility = Visibility.Visible;
        }

        public void HideDashboard()
        {
            deadlineButton.Visibility = Visibility.Hidden;
            unsolvedButton.Visibility = Visibility.Hidden;
            unsolvedLabel.Visibility = Visibility.Hidden;
            deadlineLabel.Visibility = Visibility.Hidden;
            newButton.Visibility = Visibility.Visible;
            editButton.Visibility = Visibility.Visible;
            dgEmployee.Visibility = Visibility.Visible;
        }

        private void editButton_Click(object sender, RoutedEventArgs e)
        {
            //Reads selected row

            var selection = dgEmployee.SelectedItem;

            if (dgEmployee.SelectedItem == null)
                popup.IsOpen = true;
            if (selection is Ticket)
            {
                Ticket selectedTicket = (Ticket)selection;
                EmployeeEditTicket employeeEditTicketWindow = new EmployeeEditTicket(selectedTicket);
                this.Hide();
                employeeEditTicketWindow.ShowDialog();
                allTickets = ticketService.ReadEmployeeTickets();
                this.Show();
            }
            else if (selection is User)
            {
                User selectedUser = (User)selection;
                EditEmployee editEmployeeWindow = new EditEmployee(selectedUser);
                this.Hide();
                editEmployeeWindow.ShowDialog();
                allUsers = userService.LoadWithoutPass();
                this.Show();
            }
        }
        private void closeButton_Click(object sender, RoutedEventArgs e)
        {
            popup.IsOpen = false;
        }

        private void OnAutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {
            //Hides unnecessary and empty fields
            PropertyDescriptor propertyDescriptor = (PropertyDescriptor)e.PropertyDescriptor;
            e.Column.Header = propertyDescriptor.DisplayName;
            if (propertyDescriptor.DisplayName == "Id")
                e.Cancel = true;
            if (propertyDescriptor.DisplayName == "HashedPassword")
                e.Cancel = true;
            if (propertyDescriptor.DisplayName == "Salt")
                e.Cancel = true;
        }

        private void unsolvedButton_Click(object sender, RoutedEventArgs e)
        {
            LoadTickets();
        }

        private void deadlineButton_Click(object sender, RoutedEventArgs e)
        {
            LoadTickets();
        }

        private void dashButton_Click(object sender, RoutedEventArgs e)
        {
            ShowDashboard();
        }

        private void backButton_Click(object sender, RoutedEventArgs e)
        {
            LoginWindow login = new LoginWindow();
            login.Show();
            this.Close();
        }

        private void newButton_Click(object sender, RoutedEventArgs e)
        {
            new RegisterNewUserWindow().Show();
            this.Close();  
        }
    }
}
