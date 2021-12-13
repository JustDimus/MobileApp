using System;
using System.Collections.Generic;
using System.Reactive.Linq;
using System.Text;

namespace MobileApp.Network.NetworkConnection
{
    public class StubNetworkConnectionService : INetworkConnectionService
    {
        private const bool DEFAULT_STATUS = true;

        public bool NetworkStatus => DEFAULT_STATUS;

        public IDisposable Subscribe(IObserver<bool> observer)
        {
            return Observable.Return(DEFAULT_STATUS).Subscribe(observer);
        }
    }
}
