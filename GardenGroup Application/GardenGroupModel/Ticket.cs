using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace GardenGroupModel
{
    public class Ticket
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)] // Unique objectId from database
        [BsonElement("_id")]
        public string Id { get; set; }
        [BsonElement("UserId")]
        public int UserId { get; set; }
        [BsonElement("Name")]
        public string Name { get; set; }
        [BsonElement("Subject")]
        public string Subject { get; set; }
        [BsonElement("Details")]
        public string Details { get; set; }
        [BsonElement("Priority")]
        public Priority Priority { get; set; }
        [BsonElement("Deadline")]
        public int Deadline { get; set; }
        [BsonElement("Type")]
        public TicketType TicketType { get; set; }
        [BsonElement("Status")]
        public Status Status { get; set; }
        [BsonElement("Date")]
        public DateTime DateCreated { get; set; }
    }
}