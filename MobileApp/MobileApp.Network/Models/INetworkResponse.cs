using System;
using System.Collections.Generic;
using System.Text;

namespace MobileApp.Network.Interfaces.Models
{
    public interface INetworkResponse
    {
        int StatusCode { get; }

        string Body { get; }

        bool TryParse<TEntity>(out TEntity entity) where TEntity : class;
    }

    public interface INetworkResponse<TEntity> where TEntity : class
    {
        bool CanBeConverted { get; }

        TEntity Result { get; }
    }
}
