using GardenGroupDAL;
using GardenGroupModel;
using MongoDB.Bson;
using System;
using System.Collections.Generic;

namespace GardenGroupLogic
{
    public class UserService
    {
        private UserDao userDao;
        private static UserService instance;

        private UserService()
        {
            this.userDao = new UserDao();
        }

        public static UserService GetInstance()
        {
            if (instance == null)
            {
                instance = new UserService();
            }
            return instance;
        }

        public void AddUser(string table, User user)
        {
            // Insert a single record from the person model
            userDao.AddUser(table, user);
        }

        public List<User> ReadUsers()
        {
            return userDao.ReadUsers();
        }

        public List<User> LoadWithoutPass()
        {
            return userDao.LoadWithoutPass();
        }

        public User ReadUser(ObjectId objectId)
        {
            return userDao.ReadUser(objectId);
        }

        public void UpdateUser(User user)
        {
            userDao.UpdateUser(user, user.Id);
        }

        public void DeleteUser(ObjectId objectId)
        {
            userDao.DeleteUser(objectId);
        }

        //Retrieves employeeNr and rights from the database
        public User ReadUserByEmail(string emailaddress)
        {
            return userDao.GetEmployeeEmailaddress(emailaddress);
        }

        //Retrieves password and salt from the database
        public User GetPasswordAndSalt(User user)
        {
            return userDao.RetrievePasswordAndSalt(user);
        }
    }
}
