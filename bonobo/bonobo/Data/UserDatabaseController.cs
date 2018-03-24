using bonobo.Models;
using bonobo.ViewModels;
using SQLite;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace bonobo.Data
{
    //we make the connection with the local DB and create the table for the user objects
    public class UserDatabaseController
    {
        static object locker = new object();

        SQLiteConnection database;

        public UserDatabaseController()
        {
            database = DependencyService.Get<ISQLite>().GetConnection(); //use platform specific code
            database.CreateTable<UserLogin>();
        }

        public UserLogin GetUser()
        {
            lock (locker)
            {
                if(database.Table<UserLogin>().Count() == 0)
                {
                    return null;
                }
                else
                {
                    return database.Table<UserLogin>().First();
                }
            }
        }

        public int SaveUser(UserLogin user)
        {
            lock (locker)
            {
                if(user.Id != 0)
                {
                    database.Update(user);
                    return user.Id;
                }
                else
                {
                    return database.Insert(user);
                }
            }
        }

        public int DeleteUser(int Id)
        {
            lock (locker)
            {
                return database.Delete<UserLogin>(Id);
            }

        }
    }
}
