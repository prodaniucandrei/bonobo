using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace bonobo.ViewModels
{
    public class UserLogin
    {
        [PrimaryKey]
        public int Id { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }

        public UserLogin() { } //for the DB controller
        public UserLogin(string Email, string Password)
        {
            this.Email = Email;
            this.Password = Password;
        }

        //check if the variables are corectly set
        public bool CheckNullInformation()
        {
            if (this.Email == null || this.Password == null)
                return false;
            else
                return true;
        }

    }
}
