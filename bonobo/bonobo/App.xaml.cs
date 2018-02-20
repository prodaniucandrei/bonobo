using bonobo.Data;
using bonobo.Models;
using bonobo.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace bonobo
{
    public partial class App : Application
    {

        static TokenDatabaseController tokenDatabase;
        static UserDatabaseController userDatabase;
        static RestService restService;
        private static Label LabelScreen;
        private static bool HasInternet;
        private static Page CurrentPage;
        private static Timer Timer; //check periodically if the device still has network
        private static bool NoInternetShow;

        public App()
        {
            InitializeComponent();

            //the page that the app shows when launches
            MainPage = new LoginPage();
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }

        //returns user DB controller
        public static UserDatabaseController UserDatabase
        {
            get
            {
                if (userDatabase == null)
                {
                    userDatabase = new UserDatabaseController();
                }
                return userDatabase;
            }
        }

        //returns token DB controller
        public static TokenDatabaseController TokenDatabase
        {
            get
            {
                if (tokenDatabase == null)
                {
                    tokenDatabase = new TokenDatabaseController();
                }
                return tokenDatabase;
            }
        }

        //returns restservice obj
        public static RestService RestService
        {
            get
            {
                if (restService == null)
                {
                    restService = new RestService();
                }
                return restService;
            }
        }

        //----------------Internet Connection----------------

        public static void StartCheckIfInternet(Label label, Page page)
        {
            LabelScreen = label;
            label.Text = Constants.NoInternetText;
            label.IsVisible = false;
            HasInternet = true;
            CurrentPage = page;

            if(Timer == null)
            {
                Timer = new Timer((e) => 
                {
                    CheckInternetOverTime();
                }, null, 10, (int)TimeSpan.FromSeconds(3).TotalMilliseconds);
            }
        }

        /* The Timer will use this function every couple of seconds; 
         * this will check the Internet, and if it is disabled, it will change the UI elements */
        private static void CheckInternetOverTime()
        {
            var NetworkConnection = DependencyService.Get<INetworkConnection>();
            NetworkConnection.CheckNetworkConnection();
            if (!NetworkConnection.IsConnected)
            {
                //the Timer runs on a different thread, so you need to use this function in order to change UI elements
                Device.BeginInvokeOnMainThread(async () =>
                {
                    if (HasInternet)
                    {
                        if (!NoInternetShow)
                        {
                            HasInternet = false;
                            LabelScreen.IsVisible = true;
                            await ShowDisplayAlert();
                        }
                    }
                });
            }
            else
            {
                Device.BeginInvokeOnMainThread(() =>
                {
                    HasInternet = true;
                    LabelScreen.IsVisible = false;
                });
            }
        }

        /* returns a boolean that represents the IsConnected variable; 
         * used to check the Internet Connection immediately (faster than CheckInternetOverTime()) */
        public static async Task<bool> CheckInternet()
        {
            var NetworkConnection = DependencyService.Get<INetworkConnection>();
            NetworkConnection.CheckNetworkConnection();
            return NetworkConnection.IsConnected;
        }

        //doaes the same thing as CheckInternet() but it will show an alert and doesn't return IsConnected
        public static async Task<bool> CheckInternetAlert()
        {
            var NetworkConnection = DependencyService.Get<INetworkConnection>();
            NetworkConnection.CheckNetworkConnection();
            if(!NetworkConnection.IsConnected)
            {
                if(!NoInternetShow)
                {
                    await ShowDisplayAlert();
                }
                return false;
            }
            return true;
        }

        //we use an external function because we want to know if we already displayed the error;
        private static async Task ShowDisplayAlert()
        {
            NoInternetShow = false;
            await CurrentPage.DisplayAlert("Internet", "Device has no internet connection.", "OK");
            NoInternetShow = false;
        }
    }
}
