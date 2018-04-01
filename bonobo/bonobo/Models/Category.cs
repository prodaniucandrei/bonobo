using System;
using System.Collections.Generic;
using System.Text;

namespace bonobo.Models
{
    public class Category
    {
        public string Name { get; set; }
        public string ShortDescription { get; set; }
        public string Image { get; set; }

        public Category() { } //for the DB controller
    }
}
