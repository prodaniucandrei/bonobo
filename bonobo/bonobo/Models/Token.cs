using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace bonobo.Models
{
    //for the login system, a token is given when the user logs in
    //we don't store the token in the user class because we want to store the user in a DB
    public class Token
    {
        [PrimaryKey]
        public int Id { get; set; }//for the db
        public string AccessToken { get; set; }//token that the user gets from the server
        public string ErrorDescription { get; set; }//error from the server, if any
        public DateTime ExpireDate { get; set; }//date when token is no longer valid    
        public int ExpireIn { get; set; }    

        public Token() { } //for the DB controller
    }
}
