using System;
using System.Collections.Generic;
using System.Text;

namespace bonobo.ViewModels
{
    public class RegisterView
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
        public DateTime BirthDate { get; set; }
        public string Gender { get; set; }

        public RegisterView(
            string firstname, 
            string lastname, 
            string email, 
            string password,
            string confirmpassword,
            DateTime birthdate,
            string gender)
        {
            this.FirstName = firstname;
            this.LastName = lastname;
            this.Email = email;
            this.Password = password;
            this.ConfirmPassword = confirmpassword;
            this.BirthDate = birthdate;
            this.Gender = gender;
        }

        //check if the variables are corectly set
        public bool CheckNullInformation()
        {
            if (this.Email == null || 
                this.Password == null || 
                this.ConfirmPassword == null || 
                this.FirstName == null || 
                this.LastName == null ||
                this.Gender == null ||
                this.BirthDate == null)
                return false;
            else
                return true;
        }
    }
   
}
