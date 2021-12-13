using System;
using System.Collections.Generic;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using System.Text;

namespace MobileApp.Network.Interfaces.Connection
{
    public class StubNetworkConnectionService : INetworkConnectionService
    {
        private readonly BehaviorSubject<bool> _networkConnectionObservable = new BehaviorSubject<bool>(true);

        public IObservable<bool> NetworkConnectionObservable => this._networkConnectionObservable.AsObservable();
    }
}
