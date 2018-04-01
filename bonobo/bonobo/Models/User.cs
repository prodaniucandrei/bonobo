using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace bonobo.Models
{
    public class User
    {
        [PrimaryKey]
        public int Id { get; set; }
        public string RemoteId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public DateTime BirthDate { get; set; }
        public string Gender { get; set; }

        public User() { } //for the DB controller
    }
}
