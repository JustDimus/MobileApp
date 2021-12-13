using MobileApp.Network.Interfaces.Models;
using MobileApp.Network.Interfaces.RequestFactory;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MobileApp.Network.Interfaces.CommunicationService
{
    public interface INetworkCommunicationService
    {
        INetworkRequestFactory RequestFactory { get; }

        Task<INetworkResponse> SendRequestAsync(INetworkRequest request);
    }
}
