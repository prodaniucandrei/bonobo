using bonobo.ViewModels;
using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace bonobo.Models
{
    public class Activity
    {
        [PrimaryKey]
        public int Id { get; set; }
        public string ActivityName { get; set; }
        public string Category { get; set; }
        public string ShortDescription { get; set; }
        public int NoPlaces { get; set; }
        public string Where { get; set; }
        public DateTime When { get; set; }
        public string Image { get; set; }
        public List<string> JoinedUsersListIds { get; set; }
        public string HostUserId { get; set; }

        public Activity() { } //for the DB controller
    }
}
