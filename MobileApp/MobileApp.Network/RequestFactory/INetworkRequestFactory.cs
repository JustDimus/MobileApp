using MobileApp.Network.Interfaces.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace MobileApp.Network.Interfaces.RequestFactory
{
    public interface INetworkRequestFactory
    {
        INetworkRequest CreateRequest(INetworkRequsetTemplate requestTemplate);

        INetworkRequest CreateRequest<TEntity>(INetworkRequsetTemplate requestTemplate, TEntity body) where TEntity : class;
    }
}
