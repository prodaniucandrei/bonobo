using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Net;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using bonobo.Data;
using bonobo.Droid.Data;
using Xamarin.Forms;

//Also needed Android Manifest permission to check the net state: ACCESS_NETWORK_STATE
//Assembly addition that will let the compiler know to use this platform specific code for the interface
[assembly: Dependency(typeof(NetworkConnection))]

namespace bonobo.Droid.Data
{
    public class NetworkConnection : INetworkConnection
    {
        public bool IsConnected { get; set; }
        public void CheckNetworkConnection()
        {
            //we ask the Android ConectivityManager if the device has an active network
            var ConnectivityManager = (ConnectivityManager)Android.App.Application.Context.GetSystemService(Context.ConnectivityService);
            var ActiveNetworkInfo = ConnectivityManager.ActiveNetworkInfo;
            if (ActiveNetworkInfo != null && ActiveNetworkInfo.IsConnectedOrConnecting)
            {
                IsConnected = true;
            }
            else
            {
                IsConnected = false;
            }
        }
    }
}