using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace bonobo.ViewModels
{
    public class LoginView
    {
        public string Email { get; set; }
        public string Password { get; set; }

        public LoginView(string Email, string Password)
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
