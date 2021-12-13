using System;
using System.Collections.Generic;
using System.Text;

namespace MobileApp.Network.Interfaces.Models
{
    public interface INetworkRequsetTemplate
    {
        Uri Uri { get; }

        RequestMethods Method { get; }
    }
}
