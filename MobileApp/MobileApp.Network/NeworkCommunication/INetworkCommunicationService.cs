using MobileApp.Library.Network.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MobileApp.Library.Network.NeworkCommunication
{
    public interface INetworkCommunicationService
    {
        Task<INetworkResponse> SendRequestAsync(INetworkRequest request);
    }
}
