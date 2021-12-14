using System;
using System.Collections.Generic;
using System.Text;

namespace MobileApp.Library.Network.NetworkConnection
{
    public interface INetworkConnectionService : IObservable<bool>
    {
        bool NetworkStatus { get; }
    }
}
