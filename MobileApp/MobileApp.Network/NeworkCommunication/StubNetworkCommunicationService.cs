using MobileApp.Library.Network.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MobileApp.Library.Network.NeworkCommunication
{
    public class StubNetworkCommunicationService : INetworkCommunicationService
    {
        private const int DEFAULT_RESPONSE_CODE = 200;

        public Task<INetworkResponse> SendRequestAsync(INetworkRequest request)
        {
            return Task.FromResult<INetworkResponse>(new NetworkResponse()
            {
                ResponseCode = DEFAULT_RESPONSE_CODE
            });
        }
    }
}
