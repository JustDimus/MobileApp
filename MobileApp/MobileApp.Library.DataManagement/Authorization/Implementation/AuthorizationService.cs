using MobileApp.Library.DataManagement.Models;
using MobileApp.Library.DataManagement.NetworkModels;
using MobileApp.Library.DataManagement.Request;
using MobileApp.Library.DataManagement.RequestManagement;
using System;
using System.Collections.Generic;
using System.Reactive.Subjects;
using System.Text;
using System.Threading.Tasks;

namespace MobileApp.Library.DataManagement.Authorization.Implementation
{
    public class AuthorizationService : IAuthorizationService
    {
        private readonly IRequestManager _requestManager;

        private AuthorizationStatuses lastAuthorizationStatus;

        private string lastUsedToken;

        private BaseApplicationRequest previousAuthorizationRequest;

        private BehaviorSubject<AuthorizationStatuses> authorizationStatusSubject = new BehaviorSubject<AuthorizationStatuses>(AuthorizationStatuses.Undefined);

        public AuthorizationService(
            IRequestManager requestManager)
        {
            this._requestManager = requestManager ?? throw new ArgumentNullException(nameof(requestManager));
        }

        public IObservable<AuthorizationStatuses> AuthorizationStatusObservable => this.authorizationStatusSubject;

        public Task<AuthorizationStatuses> AuthorizeAsync(LoginData loginData)
        {
            if (loginData == null)
            {
                throw new ArgumentNullException(nameof(loginData));
            }

            var request = new LoginRequest(loginData);

            return this.SendAuthorizationRequest(request);
        }

        public Task<AuthorizationStatuses> AuthorizeAsync(RegistrationData registrationData)
        {
            if (registrationData == null)
            {
                throw new ArgumentNullException(nameof(registrationData));
            }

            var request = new RegistrationRequest(registrationData);

            return this.SendAuthorizationRequest(request);
        }

        public string GetLastUsedToken()
        {
            return this.lastUsedToken;
        }

        public async Task<string> UpdateToken()
        {
            if (this.previousAuthorizationRequest == null)
            {
                return null;
            }

            var authorizationResult = await this.SendAuthorizationRequest(previousAuthorizationRequest);

            if (authorizationResult == AuthorizationStatuses.Authorized)
            {
                return this.lastUsedToken;
            }

            return null;
        }

        private async Task<AuthorizationStatuses> SendAuthorizationRequest(BaseApplicationRequest authorizationRequest)
        {
            if (authorizationRequest == null)
            {
                throw new ArgumentNullException(nameof(authorizationRequest));
            }

            var result = await this._requestManager.SendRequestAsync<BaseResult<AuthorizationIdentifier>>(authorizationRequest);

            var authorizationStatus = result ? AuthorizationStatuses.Authorized : AuthorizationStatuses.Unauthorized;
            if (authorizationStatus != this.lastAuthorizationStatus)
            {
                this.previousAuthorizationRequest = authorizationRequest;
                this.authorizationStatusSubject.OnNext(authorizationStatus);
            }

            if (result)
            {
                this.lastUsedToken = result.RawBodyResponse;
            }

            return AuthorizationStatuses.Unauthorized;
        }
    }
}
