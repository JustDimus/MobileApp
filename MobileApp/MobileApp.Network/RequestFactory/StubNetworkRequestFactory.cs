using MobileApp.Network.Interfaces.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace MobileApp.Network.Interfaces.RequestFactory
{
    public class StubNetworkRequestFactory : INetworkRequestFactory
    {
        public INetworkRequest CreateRequest(INetworkRequsetTemplate requestTemplate)
        {
            return null;
        }

        public INetworkRequest CreateRequest<TEntity>(INetworkRequsetTemplate requestTemplate, TEntity body) where TEntity : class
        {
            return null;
        }
    }
}
