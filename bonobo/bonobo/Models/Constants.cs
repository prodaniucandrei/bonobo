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


        //----------------API-Account-CALLS---------------------------------------------------
        public static string LoginURL = "http://192.168.0.14:6781/api/account/login";
        public static string LogoutURL = "http://192.168.0.14:6781/api/account/logout";
        public static string RegisterURL = "http://192.168.0.14:6781/api/account/register";
        public static string ForgotPasswordURL = "http://192.168.0.14:6781/api/account/forgotpassword";
        public static string ChangePasswordURL = "http://192.168.0.14:6781/api/account/changepassword";
        public static string UpdateUserInfoURL = "http://192.168.0.14:6781/api/account/updateuserinfo";
        public static string GetAllUsersURL = "http://192.168.0.14:6781/api/account/getallusers";
        public static string GetUserByIdURL = "http://192.168.0.14:6781/api/account/getuserbyid";
        public static string GetUserByEmailURL = "http://192.168.0.14:6781/api/account/getuserbyemail";
        public static string DeleteAccountURL = "http://192.168.0.14:6781/api/account/deleteaccount";


        //----------------API-Activity-CALLS--------------------------------------------------
        public static string CreateActivityURL = "http://192.168.0.14:6781/api/activity/createactivity";
        public static string UpdateActivityURL = "http://192.168.0.14:6781/api/activity/updatectivity";
        public static string GetAllActivitiesURL = "http://192.168.0.14:6781/api/activity/getallactivities";
        public static string GetActivityByIdURL = "http://192.168.0.14:6781/api/activity/getactivitybyid";
        public static string GetHostedActivitiesURL = "http://192.168.0.14:6781/api/activity/gethostedactivities";
        public static string GetJoinedActivitiesURL = "http://192.168.0.14:6781/api/activity/getjoinedactivities";
        public static string JoinActivityURL = "http://192.168.0.14:6781/api/activity/joinactivity";
        public static string UnJoinActivityURL = "http://192.168.0.14:6781/api/activity/unjoinactivity";
        public static string DeleteActivityURL = "http://192.168.0.14:6781/api/activity/deleteactivity";


        //----------------Internet Connection-------------------------------------------------
        public static string NoInternetText = "No Internet.";
    }
}
