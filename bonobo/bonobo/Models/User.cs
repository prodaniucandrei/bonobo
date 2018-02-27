﻿using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace bonobo.Models
{
    public class User
    {
        [PrimaryKey]
        public int Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }

        public User() { } //for the DB controller
        public User(string Username, string Password)
        {
            this.Username = Username;
            this.Password = Password;
        }

        //check if the variables are corectly set
        public bool CheckNullInformation()
        {
            if (this.Username == null || this.Password == null)
                return false;
            else
                return true;
        }

    }
}
