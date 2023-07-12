using GardenGroupModel;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GardenGroupDAL
{
    public class TicketDao : BaseDao
    {
        private const string tableName = "Ticket";
        public void InsertTicket(Ticket ticket)
        {
            InsertRecord(tableName, ticket);
        }

        public List<Ticket> ReadEmployeeTickets()
        {
            return LoadRecords<Ticket>(tableName);
        }
        public List<Ticket> ReadUserTickets(User user)
        {
            return LoadRecordsByUserId<Ticket>(tableName, user.UserId);
        }

        public Ticket ReadTicket(ObjectId id)
        {
            return LoadRecordById<Ticket>(tableName, id);
        }

        public void UpdateTicket(Ticket ticket)
        {
            UpdateRecordWithFilter(tableName, ObjectId.Parse(ticket.Id), ticket);
        }

        public void DeleteTicket(Ticket ticket)
        {
            DeleteRecord<Ticket>(tableName, ObjectId.Parse(ticket.Id));
        }
    }
}

