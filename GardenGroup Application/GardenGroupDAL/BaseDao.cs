using System;
using System.Collections.Generic;
using System.Text;
using GardenGroupModel;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson.Serialization.Conventions;
using MongoDB.Driver;

namespace GardenGroupDAL
{
    public abstract class BaseDao
    {
        private IMongoDatabase database { get; }

        public BaseDao()
        {
            database = Database();
            var pack = new ConventionPack
            {
                new EnumRepresentationConvention(BsonType.String)
            };

            ConventionRegistry.Register("EnumStringConvention", pack, t => true);
        }

        private IMongoDatabase Database()
        {
            string connectionString = "mongodb+srv://Admin:admin1234@projectgroup2.s3iimmt.mongodb.net/test";
            MongoClient dbClient = new MongoClient(connectionString);
            IMongoDatabase database = dbClient.GetDatabase("Project");
            return database;
        }

        // Insert statement
        public void InsertRecord<T>(string table, T record)
        {
            var collection = database.GetCollection<T>(table);
            collection.InsertOne(record);
        }

        // Read statement
        public List<T> LoadRecords<T>(string table)
        {
            var collection = database.GetCollection<T>(table);
            return collection.AsQueryable().ToList();
        }

        //Exclude reading specific fields
        public List<User> LoadWithoutPass<User>(string table)
        {
            var collection = database.GetCollection<User>(table);
            var definition = Builders<User>.Projection
                .Exclude("HashedPassword")
                .Exclude("Salt");
            return collection.Find(Builders<User>.Filter.Empty)
                .Project<User>(definition)
                .ToList();
        }

        // Read statement by id
        public T LoadRecordById<T>(string table, ObjectId id)
        {
            try
            {
                var collection = database.GetCollection<T>(table);
                var filter = Builders<T>.Filter.Eq("_id", id);
                return collection.Find(filter).First();
            }
            catch(Exception ex)
            {
                throw new Exception($"No record found! {ex.Message}");
            }
        }

        //Read all by id
        public List<T> LoadRecordsByUserId<T>(string table, int id)
        {
            try
            {
                var collection = database.GetCollection<T>(table);
                var filter = Builders<T>.Filter.Eq("UserId", id);
                return collection.Find(filter).ToList();
            }
            catch (Exception ex)
            {
                throw new Exception($"No record found! {ex.Message}");
            }
        }

        // Update statement
        public void UpdateRecord<T>(string table, ObjectId id, T record)
        {
            var collection = database.GetCollection<T>(table);
            var result = collection.ReplaceOne(
                new BsonDocument("_id", id),
                record,
                new UpdateOptions { IsUpsert = true });
        }

        // Update statement
        public void UpdateRecordWithFilter<T>(string table, ObjectId id, T record)
        {
            var collection = database.GetCollection<T>(table);
            var result = collection.ReplaceOne(
                new BsonDocument("_id", id),
                record,
                new UpdateOptions { IsUpsert = true });
        }

        // Delete statement
        public void DeleteRecord<T>(string table, ObjectId id)
        {
            var collection = database.GetCollection<T>(table);
            var filter = Builders<T>.Filter.Eq("_id", id);
            collection.DeleteOne(filter);
        }

        public T GetEmployeeByEmailaddress<T>(string emailaddress)
        {
            var collection = database.GetCollection<T>("LogIn");
            var filter = Builders<T>.Filter.Eq("Email", emailaddress);
            return collection.Find(filter).FirstOrDefault(); 
        }

        //Retrieves password and salt from the database
        public User GetPasswordAndSalt<T>(User user)
        {
            var collection = database.GetCollection<User>("LogIn");
            var filter = Builders<User>.Filter.Eq("HashedPassword", user.HashedPassword.ToString());
            var salt = Builders<User>.Filter.Eq("Salt", user.Salt.ToString());
            collection.Find(filter).FirstOrDefault();
            collection.Find(salt).FirstOrDefault();
            user.AddSaltAndPassword(Encoding.ASCII.GetBytes(filter.ToString()), Encoding.ASCII.GetBytes(salt.ToString()));
            return user;
        }

   /*     public User GetLastRecord<T>() 
        {
            var collection = database.GetCollection<User>("LogIn");
            var filter = Builders<User>.Filter;
           // return collection.Find().limit(1).sort({$natural: -1})
            collection.findOne({$query: { }, $orderby: {$natural: -1} })
        }*/

 
        public T GetRecordByKeyValue<T>(string table, string key, string value)
        {
            var collection = database.GetCollection<T>(table);
            var filter = Builders<T>.Filter.Eq(key, value);

            return collection.Find(filter).FirstOrDefault();
        }
    }
}
