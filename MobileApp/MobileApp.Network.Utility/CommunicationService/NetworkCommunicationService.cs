using MobileApp.Network.Interfaces.CommunicationService;
using MobileApp.Network.Interfaces.Models;
using MobileApp.Network.Interfaces.RequestFactory;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MobileApp.Network.Utility.CommunicationService
{
    public class NetworkCommunicationService : INetworkCommunicationService
    {
        private readonly INetworkRequestFactory _requestFactory;

        public NetworkCommunicationService(
            INetworkRequestFactory requestFactory)
        {
            this._requestFactory = requestFactory ?? throw new ArgumentNullException(nameof(requestFactory));
        }

        public INetworkRequestFactory RequestFactory => this._requestFactory;

        public Task<INetworkResponse> SendRequestAsync(INetworkRequest request)
        {
            throw new NotImplementedException();
        }
    }
}
