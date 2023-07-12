
using GardenGroupLogic;
using GardenGroupModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace GardenGroup_Application
{
    public class TicketWindowBase : Window
    {
        public Ticket ticket { get; set; }
        public TicketService ticketService { get; set; }
        public int ticketType 
        { 
            get { return (int)ticket.TicketType; } 
            set { ticket.TicketType = (TicketType)value; }
        }
        public int priority 
        {
            get { return (int)ticket.Priority; }
            set { ticket.Priority = (Priority)value; }
        }

        public int status 
        {
            get { return (int)ticket.Status; }
            set { ticket.Status = (Status)value; }
        }

        public string? titleText { get; set; }

        public TicketWindowBase()
        {
            this.ticketService = TicketService.GetInstance();
        }
        public void UpdateTicket(object sender, RoutedEventArgs e)
        {
            ticketService.UpdateTicket(this.ticket);
            this.Close();
        }

        public void CreateTicket(object sender, RoutedEventArgs e)
        {
            this.ticket.Status = Status.New;
            this.ticket.DateCreated = DateTime.Now;
            ticketService.InsertTicket(this.ticket);
            this.Close();
        }

        public void DeleteTicket(object sender, RoutedEventArgs e)
        {
            ticketService.DeleteTicket(ticket);
            this.Close();
        }

        public void CancelAction(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
