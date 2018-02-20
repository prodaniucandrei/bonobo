using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using bonobo.Data;
using bonobo.iOS.Data;
using CoreFoundation;
using Foundation;
using SystemConfiguration;
using UIKit;
using Xamarin.Forms;

//Assembly addition that will let the compiler know to use this platform specific code for the interface
[assembly: Dependency(typeof(NetworkConnection))]


namespace bonobo.iOS.Data
{
    public class NetworkConnection : INetworkConnection
    {
        public bool IsConnected { get; set; }
        public void CheckNetworkConnection()
        {
            InternetStatus();
        }

        //check network reachability flags
        public bool InternetStatus()
        {
            NetworkReachabilityFlags flags;
            bool DefaultNetworkAvailable = IsNetworkAvailable(out flags);

            if (DefaultNetworkAvailable && (flags & NetworkReachabilityFlags.IsDirect) != 0)
            {
                return false;
            }
            else if ((flags & NetworkReachabilityFlags.IsWWAN) != 0)
            {
                return true;
            }
            else if (flags == 0)
            {
                return false;
            }
            return true;
        }

        private event EventHandler ReachabilityChanged;
        private void OnChange(NetworkReachabilityFlags flags)
        {
            var h = ReachabilityChanged;
            if (h != null)
                h(null, EventArgs.Empty);
        }

        private NetworkReachability defaultReachability;
        public bool IsNetworkAvailable(out NetworkReachabilityFlags flags)
        {
            if(defaultReachability == null)
            {
                defaultReachability = new NetworkReachability(new System.Net.IPAddress(0));
                defaultReachability.SetNotification(OnChange);
                defaultReachability.Schedule(CFRunLoop.Current, CFRunLoop.ModeDefault);
            }
            if(!defaultReachability.TryGetFlags(out flags))
            {
                return false;
            }
            return IsReachableWithoutRequiringConnection(flags);
        }

        private bool IsReachableWithoutRequiringConnection(NetworkReachabilityFlags flags)
        {
            bool IsReachable = (flags & NetworkReachabilityFlags.Reachable) != 0;
            bool NoConnectionRequired = (flags & NetworkReachabilityFlags.ConnectionRequired) == 0;

            if ((flags & NetworkReachabilityFlags.IsWWAN) != 0)
                NoConnectionRequired = true;
            return IsReachable && NoConnectionRequired;
        }
    }
}