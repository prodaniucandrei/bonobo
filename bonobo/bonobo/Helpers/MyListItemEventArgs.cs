using bonobo.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace bonobo.Helpers
{
    //This is for accessing the passed listview item object        
    public class MyListItemEventArgs : EventArgs
    {
        public ActivityView MyItem { get; set; }
        public MyListItemEventArgs(ActivityView item)
        {
            this.MyItem = item;
        }
    }
}
