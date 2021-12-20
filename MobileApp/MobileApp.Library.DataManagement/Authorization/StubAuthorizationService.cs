using MobileApp.Library.DataManagement.Models;
using System;
using System.Collections.Generic;
using System.Reactive.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MobileApp.Library.DataManagement.Authorization
{
    public class StubAuthorizationService : IAuthorizationService
    {
        private const AuthorizationStatuses DEFAULT_AUTHORIZATION_STATUS = AuthorizationStatuses.Authorized;

        public IObservable<AuthorizationStatuses> AuthorizationStatusObservable => Observable.Return(DEFAULT_AUTHORIZATION_STATUS);

        public Task<AuthorizationStatuses> AuthorizeAsync(LoginData loginData)
        {
            return Task.FromResult(DEFAULT_AUTHORIZATION_STATUS);
        }

        public Task<AuthorizationStatuses> AuthorizeAsync(RegistrationData registrationData)
        {
            return Task.FromResult(DEFAULT_AUTHORIZATION_STATUS);
        }

        public string GetLastUsedToken()
        {
            return null;
        }

        public Task<string> UpdateToken()
        {
            return Task.FromResult<string>(null);
        }
    }
}
