using System;
using System.Collections.Generic;
using System.Text;

namespace MobileApp.Library.Network.Models
{
    public interface INetworkResponse
    {
        int ResponseCode { get; }

        string Body { get; }
    }
}
