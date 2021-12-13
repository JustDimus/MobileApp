using System;
using System.Collections.Generic;
using System.Text;

namespace MobileApp.Network.Interfaces.Connection
{
    public interface INetworkConnectionService
    {
        IObservable<bool> NetworkConnectionObservable { get; }
    }
}
