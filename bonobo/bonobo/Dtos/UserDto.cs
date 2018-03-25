using System;
using System.Collections.Generic;
using System.Text;

namespace bonobo.Dtos
{
    public class UserDto
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public DateTime BirthDate { get; set; }
        public string Gender { get; set; }

        public UserDto() { } //for the DB controller
        public UserDto(
           string firstname,
           string lastname,
           string email,
           DateTime birthdate,
           string gender)
        {
            this.FirstName = firstname;
            this.LastName = lastname;
            this.Email = email;
            this.BirthDate = birthdate;
            this.Gender = gender;
        }
    }
}
