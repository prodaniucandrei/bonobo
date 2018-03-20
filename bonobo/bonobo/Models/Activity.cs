using System;
using System.Collections.Generic;
using System.Text;

namespace bonobo.Models
{
    public class Activity
    {
        public string ActivityName { get; set; }
        public string Category { get; set; }
        public string ShortDescription { get; set; }
        public int NoPlaces { get; set; }
        public string Where { get; set; }
        public DateTime When { get; set; }
        public string Image { get; set; }

        public Activity() { } //for the DB controller
        public Activity(
            string ActivityName, 
            string Category, 
            string ShortDescription,
            int NoPlaces,
            string Where,
            DateTime When,
            string Image)
        {
            this.ActivityName = ActivityName;
            this.Category = Category;
            this.ShortDescription = ShortDescription;
            this.NoPlaces = NoPlaces;
            this.Where = Where;
            this.When = When;
            this.Image = Image;
        }
    }
}
