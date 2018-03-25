using System;
using System.Collections.Generic;
using System.Text;

namespace bonobo.Models
{
    public class UserProfileView
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string HeaderImage { get; set; }
        public string ProfileImage { get; set; }

        public UserProfileView(string firstname, string lastname, string himage, string pimage)
        {
            this.FirstName = firstname;
            this.LastName = lastname;
            this.HeaderImage = himage;
            this.ProfileImage = pimage;
        }
    }
}
