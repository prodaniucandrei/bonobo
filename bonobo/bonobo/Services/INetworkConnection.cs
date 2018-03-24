using System;
using System.Collections.Generic;
using System.Text;

namespace bonobo.Data
{
    public interface INetworkConnection
    {
        bool IsConnected { get; set; }
        void CheckNetworkConnection();
    }
}
