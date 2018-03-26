using System;
using System.Collections.Generic;
using System.Text;

namespace bonobo.Models
{
    public class UserProfileView
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Gender { get; set; }
        public DateTime Birthdate { get; set; }
        public string HeaderImage { get; set; }
        public string ProfileImage { get; set; }
        public int Reviews { get; set; }
        public int Hosted { get; set; }
        public int Joined { get; set; }
    }
}
