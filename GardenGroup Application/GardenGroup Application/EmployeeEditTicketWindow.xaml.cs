using GardenGroupLogic;
using GardenGroupModel;
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
    /// Interaction logic for EmployeeEditTicket.xaml
    /// </summary>
    public partial class EmployeeEditTicket : TicketWindowBase
    {
        public EmployeeEditTicket(Ticket ticket) : base()
        {
            InitializeComponent();
            this.ticket = ticket;
            this.titleText = "Edit ticket";
        }

        private void DeleteTicketButton_Click(object sender, RoutedEventArgs e)
        {
            this.ticketService.DeleteTicket(ticket);
            this.Close();
        }
    }
}
