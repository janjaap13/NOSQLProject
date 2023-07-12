using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;

namespace GardenGroupModel
{
    public class User
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)] // Unique objectId from database
        [BsonElement("_id")]
        public ObjectId Id { get; set; }
        [BsonElement("ID")]
        public int UserId { get; set; }
        [BsonElement("FirstName")]
        public string FirstName { get; set; }
        [BsonElement("LastName")]
        public string LastName { get; set; }
        [BsonElement("PhoneNumber")]
        public int PhoneNumber { get; set; }
        [BsonElement("Location")]
        public Location Location { get; set; }
        [BsonElement("AccountType")]
        public AccountType AccountType { get; set; }
        [BsonElement("Email")]
        public string Email { get; set; }
        [BsonElement("HashedPassword")]
        public byte[] HashedPassword { get; set; }
        [BsonElement("Salt")]
        public byte[] Salt { get; set; }



        // public User
        public User(string email)
        {
            Email = email;
        }

        public User(string email, AccountType right) : this(email)
        {
            AccountType = right;
        }

        public User(int iD, string firstName, string lastName, int phoneNumber, Location location, AccountType accountType, string email, byte[] password, byte[] salt)
        {
            // _id = id;
            this.UserId = iD;
            this.FirstName = firstName;
            this.LastName = lastName;
            this.PhoneNumber = phoneNumber;
            this.Location = location;
            this.AccountType = accountType;
            this.Email = email;
            this.HashedPassword = password;
            this.Salt = salt;
        }

        //Adds salt and password to existing user
        public void AddSaltAndPassword(byte[] password, byte[] salt)
        {
            Salt = salt;
            HashedPassword = password;
        }
    }
}
