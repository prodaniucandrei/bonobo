using System;
using System.Collections.Generic;
using System.Text;

namespace bonobo.ViewModels
{
    public class CreateActivityViewModel
    {
        public string ActivityName { get; set; }
        public string Category { get; set; }
        public string ShortDescription { get; set; }
        public int NoPlaces { get; set; }
        public string Where { get; set; }
        public DateTime When { get; set; }
    }
}
