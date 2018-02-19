using System;
using System.Collections.Generic;
using System.Text;

namespace bonobo.Models
{
    //for the login system, a token is given when the user logs in
    //we don't store the token in the user class because we want to store the user in a DB
    class Token
    {
        public int Id { get; set; }//for the db
        public string access_token { get; set; }//token that the user gets from the server
        public string error_description { get; set; }//error from the server, if any
        public DateTime expire_date { get; set; }//date when token is no longer valid    

        public Token() { } //for the DB controller
    }
}
