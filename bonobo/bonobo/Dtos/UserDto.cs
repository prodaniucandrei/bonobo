using System;
using System.Collections.Generic;
using System.Text;

namespace bonobo.Dtos
{
    public class UserDto
    {
        public string RemoteId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public DateTime BirthDate { get; set; }
        public string Gender { get; set; }
    }
}
