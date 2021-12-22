using MobileApp.Library.DataManagement.Models;
using System;
using System.Collections.Generic;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using System.Text;
using System.Threading.Tasks;

namespace MobileApp.Library.DataManagement.Authorization
{
    public class StubAuthorizationService : IAuthorizationService
    {
        private const AuthorizationStatuses DEFAULT_AUTHORIZATION_STATUS = AuthorizationStatuses.Authorized;

        private BehaviorSubject<AuthorizationStatuses> _behaviorSubject = new BehaviorSubject<AuthorizationStatuses>(DEFAULT_AUTHORIZATION_STATUS);

        public IObservable<AuthorizationStatuses> AuthorizationStatusObservable => this._behaviorSubject;

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
