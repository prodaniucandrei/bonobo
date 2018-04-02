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

        //-----------------STYLE--------------------------------------------------------------
        public static Color BackgroundColor = Color.White;
        public static Color MainTextColor = Color.FromHex("88c7c9");


        //----------------LOGIN-STYLE-----------------------------------------------------
        public static int LoginIconHeight = 120; //for phone


        //----------------DASHBOARD-STYLE-----------------------------------------------------
        public static Color CurrentTabColor = Color.FromHex("6ca9ab");
        public static Color DefaultTabColor = Color.FromHex("88c7c9");
        public static int RowHeight = 50;
    

        //----------------API-ACCOUNT-CALLS---------------------------------------------------
        public static string LoginURL = "http://192.168.0.14:6781/api/account/login";
        public static string LogoutURL = "http://192.168.0.14:6781/api/account/logout";
        public static string RegisterURL = "http://192.168.0.14:6781/api/account/register";
        public static string ForgotPasswordURL = "http://192.168.0.14:6781/api/account/forgotpassword";
        public static string ChangePasswordURL = "http://192.168.0.14:6781/api/account/changepassword";
        public static string UpdateUserInfoURL = "http://192.168.0.14:6781/api/account/updateuserinfo";
        public static string GetAllUsersURL = "http://192.168.0.14:6781/api/account/getallusers";
        public static string FindUserByIdURL = "http://192.168.0.14:6781/api/account/finduserbyid";
        public static string FindUserByEmailURL = "http://192.168.0.14:6781/api/account/finduserbyemail";
        public static string DeleteAccountURL = "http://192.168.0.14:6781/api/account/deleteaccount";


        //----------------API-ACTIVITY-CALLS--------------------------------------------------
        public static string CreateActivityURL = "http://192.168.0.14:6781/api/activity/createactivity";
        public static string UpdateActivityURL = "http://192.168.0.14:6781/api/activity/updatectivity";
        public static string GetAllActivitiesURL = "http://192.168.0.14:6781/api/activity/getallactivities";
        public static string FindActivityByIdURL = "http://192.168.0.14:6781/api/activity/findactivitybyid";
        public static string GetHostedActivitiesURL = "http://192.168.0.14:6781/api/activity/gethostedactivities";
        public static string GetJoinedActivitiesURL = "http://192.168.0.14:6781/api/activity/getjoinedactivities";
        public static string JoinActivityURL = "http://192.168.0.14:6781/api/activity/joinactivity";
        public static string UnJoinActivityURL = "http://192.168.0.14:6781/api/activity/unjoinactivity";
        public static string DeleteActivityURL = "http://192.168.0.14:6781/api/activity/deleteactivity";


        //----------------Internet Connection-------------------------------------------------
        public static string NoInternetText = "No Internet.";
    }
}
