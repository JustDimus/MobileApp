using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace MobileApp.Network.Interfaces.Models
{
    public interface INetworkRequest
    {
        Uri Uri { get; }

        HttpMethod Method { get; }

        string Body { get; }

        IReadOnlyDictionary<string, string> Headers { get; }

        bool SetBody<TEntity>(TEntity body);

        Task<INetworkResponse> SendAsync();

        Task<INetworkResponse<TEntity>> SendAsync<TEntity>() where TEntity : class;
    }
}
