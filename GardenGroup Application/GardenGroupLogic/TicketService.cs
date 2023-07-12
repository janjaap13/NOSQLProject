using GardenGroupDAL;
using GardenGroupModel;
using MongoDB.Bson;
using System;
using System.Collections.Generic;

namespace GardenGroupLogic
{
    public class TicketService
    {
        private static TicketService instance;
        private TicketDao ticketDao;

        private TicketService()
        {
            this.ticketDao = new TicketDao();
        }

        public static TicketService GetInstance()
        {
            if (instance == null)
            {
                instance = new TicketService();
            }
            return instance;
        }
        public void InsertTicket(Ticket ticket)
        {
            ticketDao.InsertTicket(ticket);
        }
        public List<Ticket> ReadEmployeeTickets()
        {
            return ticketDao.ReadEmployeeTickets();
        }

        public List<Ticket> ReadUserTickets(User user)
        {
            return ticketDao.ReadUserTickets(user);
        }

        public Ticket ReadTicket(ObjectId id)
        {
            return ticketDao.ReadTicket(id);
        }

        public void UpdateTicket(Ticket ticket)
        {
            ticketDao.UpdateTicket(ticket);
        }

        public void DeleteTicket(Ticket ticket)
        {
            ticketDao.DeleteTicket(ticket);
        }
    }
}
