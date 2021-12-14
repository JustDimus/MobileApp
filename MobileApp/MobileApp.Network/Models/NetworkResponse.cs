using System;
using System.Collections.Generic;
using System.Text;

namespace MobileApp.Library.Network.Models
{
    internal sealed class NetworkResponse : INetworkResponse
    {
        public int ResponseCode { get; internal set; }

        public string Body { get; internal set; }
    }
}
