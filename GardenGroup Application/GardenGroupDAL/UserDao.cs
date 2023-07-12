using GardenGroupModel;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GardenGroupDAL
{
    public class UserDao : BaseDao
    {
        private const string tableName = "LogIn";
        public void AddUser(string table, User user)
        {
            InsertRecord(table, user); 
        }

        public List<User> ReadUsers()
        {
            return LoadRecords<User>(tableName);
        }

        public User ReadUser(ObjectId objectId)
        {
            return LoadRecordById<User>(tableName, objectId);
        }

        public void UpdateUser(User user, ObjectId objectId)
        {
            UpdateRecord(tableName, objectId, user);
        }

        public void DeleteUser(ObjectId objectId)
        {
            DeleteRecord<User>(tableName, objectId);
        }

        public List<User> LoadWithoutPass()
        {
            return LoadWithoutPass<User>(tableName);
        }

        //Retrieves employeeNr and rights from the database
        public User GetEmployeeEmailaddress(string emailaddress)
        {
           // User user = new User(GetEmployeeEmailaddress(emailaddress));
            return GetEmployeeByEmailaddress<User>(emailaddress);
        }

        //Retrieves password and salt from the database
        public User RetrievePasswordAndSalt(User user)
        {
            return GetPasswordAndSalt<User>(user);
        }
    }
}
