using GardenGroupModel;
using System;

namespace GardenGroup_Application
{
    /// <summary>
    /// Interaction logic for AddTicketWindow.xaml
    /// </summary>
    public partial class AddTicketWindow : TicketWindowBase
    {
        public AddTicketWindow(User user) : base()
        {
            InitializeComponent();
            this.titleText = "Create ticket";
            this.ticket = new Ticket();
            ticket.UserId = user.UserId;
            ticket.Name = user.FirstName;
        }
    }
}
