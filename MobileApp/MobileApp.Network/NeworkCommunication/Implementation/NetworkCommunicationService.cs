using MobileApp.Library.Network.Models;
using MobileApp.Library.Network.NeworkCommunication.Configuration;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MobileApp.Library.Network.NeworkCommunication.Implementation
{
    public class NetworkCommunicationService : INetworkCommunicationService
    {
        private readonly NetworkConfigurationManager _configurationManager;

        private HttpClient httpClient = new HttpClient();

        public NetworkCommunicationService(NetworkConfigurationManager configurationManager)
        {
            this._configurationManager = configurationManager;

            if (this._configurationManager.UseBaseUrl)
            {
                this.httpClient.BaseAddress = new Uri(this._configurationManager.BaseUrl);
            }
        }

        public async Task<INetworkResponse> SendRequestAsync(INetworkRequest request)
        {
            if (request == null)
            {
                throw new ArgumentNullException(nameof(request));
            }

            try
            {
                using (var httpRequest = new HttpRequestMessage()
                {
                    Method = this.GetHttpMethod(request.Method),
                    Content = request.Body == null ? null : new StringContent(request.Body, Encoding.UTF8, request.MediaType),
                    RequestUri = request.Url
                })
                {
                    try
                    {
                        foreach (var header in request.Headers)
                        {
                            httpRequest.Headers.TryAddWithoutValidation(header.Key, header.Value);
                        }
                    }
                    catch (Exception ex)
                    {
                        ex.Data.ToString();
                    }

                    return await GetResponse(httpRequest);
                }
            }
            catch(Exception ex)
            {
                ex.Data.ToString();
                return null;
            }
        }

        private async Task<INetworkResponse> GetResponse(HttpRequestMessage message)
        {
            INetworkResponse result = null;

            using (CancellationTokenSource source = new CancellationTokenSource(this._configurationManager.DefaultRequestCancellationTime))
            {
                try
                {
                    var response = await this.httpClient.SendAsync(message);

                    result = new NetworkResponse()
                    {
                        ResponseCode = ((int)response.StatusCode),
                        Body = await response.Content.ReadAsStringAsync()
                    };
                }
                catch (Exception ex)
                {
                    result = new NetworkResponse()
                    {
                        ResponseCode = -1
                    };
                }
            }

            return result;
        }

        private HttpMethod GetHttpMethod(RequestMethods method)
        {
            switch (method)
            {
                case RequestMethods.GET:
                    return HttpMethod.Get;
                case RequestMethods.POST:
                    return HttpMethod.Post;
                default:
                    throw new ArgumentException(nameof(method));
            }
        }
    }
}
