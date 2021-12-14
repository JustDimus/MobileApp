using MobileApp.Library.DataManagement.Request;
using MobileApp.Library.Network.NetworkConnection;
using MobileApp.Library.Network.NeworkCommunication;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MobileApp.Library.DataManagement.RequestManagement.Implementation
{
    public class NetworkRequestManager : IRequestManager
    {
        private static int SUCCESS_RESPONSE_CODE = 200;

        private readonly INetworkCommunicationService _networkCommunicationService;

        private readonly INetworkConnectionService _networkConnectonService;

        private CancellationTokenSource networkDisappearedCancellationTokenSource;
        private bool networkConnectionExist = false;
        private IDisposable networkStatusDisposable;

        public NetworkRequestManager(
            INetworkCommunicationService networkCommunicationService,
            INetworkConnectionService networkConnectonService)
        {
            this._networkCommunicationService = networkCommunicationService;
            this._networkConnectonService = networkConnectonService;

            this.networkStatusDisposable = this._networkConnectonService.Subscribe(this.OnNetworkConnectionChanged);
        }

        public async Task<ResponseContext> SendRequestAsync(BaseApplicationRequest request)
        {
            if (!this.networkConnectionExist)
            {
                return false;
            }

            var result = await this._networkCommunicationService.SendRequestAsync(request);

            if (result.ResponseCode == SUCCESS_RESPONSE_CODE)
            {
                return true;
            }

            return false;
        }

        public async Task<ResponseContext<TEntity>> SendRequestAsync<TEntity>(BaseApplicationRequest request) where TEntity : class
        {
            if (!this.networkConnectionExist)
            {
                return ResponseContext<TEntity>.FromStatus(false);
            }

            var result = await this._networkCommunicationService.SendRequestAsync(request);

            if (result.ResponseCode == SUCCESS_RESPONSE_CODE)
            {
                var serializedData = JsonConvert.DeserializeObject<TEntity>(result.Body);

                if (serializedData != null)
                {
                    return ResponseContext<TEntity>.FromResult(serializedData).AddRawBodyData(result.Body);
                }
            }

            return ResponseContext<TEntity>.FromStatus(false);
        }

        private void OnNetworkConnectionChanged(bool networkConnectionStatus)
        {
            if (this.networkConnectionExist != networkConnectionStatus)
            {
                this.networkConnectionExist = networkConnectionStatus;

                if (this.networkConnectionExist)
                {
                    this.networkDisappearedCancellationTokenSource = new CancellationTokenSource();
                }
                else
                {
                    this.networkDisappearedCancellationTokenSource?.Dispose();
                }
            }
        }

        #region IDisposable
        private bool disposedValue = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposedValue)
            {
                if (disposing)
                {
                    //Managed resources
                    this.networkDisappearedCancellationTokenSource?.Dispose();
                    this.networkStatusDisposable?.Dispose();
                }

                //Unmanaged resources

                this.disposedValue = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        #endregion
    }
}
