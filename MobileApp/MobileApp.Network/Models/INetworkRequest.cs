using System;
using System.Collections.Generic;
using System.Text;

namespace MobileApp.Library.Network.Models
{
    public interface INetworkRequest
    {
        RequestMethods Method { get; }

        string MediaType { get; }

        Uri Url { get; }

        IReadOnlyDictionary<string, string> Headers { get; }

        string Body { get; }
    }
}
