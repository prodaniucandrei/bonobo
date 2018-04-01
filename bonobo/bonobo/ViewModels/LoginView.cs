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
