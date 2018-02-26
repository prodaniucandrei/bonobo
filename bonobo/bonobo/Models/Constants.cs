using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace bonobo.Models
{
    //stores all the static data
    public class Constants
    {
        public static bool isDev = true; //indicates if we are developing or using release code

        public static Color BackgroundColor = Color.FromRgb(224, 204, 190);
        public static Color MainTextColor = Color.FromRgb(146, 100, 93);

        public static int LoginIconHeight = 120; //for phone

        //----------------Login------------------------------
        public static string LoginURL = "http://10.0.2.2:6780/token";

        //----------------Internet Connection----------------
        public static string NoInternetText = "No Internet.";
    }
}
