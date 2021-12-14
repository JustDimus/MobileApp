using MobileApp.Library.DataManagement.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MobileApp.Library.DataManagement.Authorization
{
    public interface IAuthorizationService
    {
        IObservable<AuthorizationStatuses> AuthorizationStatusObservable { get; }

        Task<AuthorizationStatuses> AuthorizeAsync(LoginData loginData);

        Task<AuthorizationStatuses> AuthorizeAsync(RegistrationData registrationData);

        string GetLastUsedToken();

        Task<string> UpdateToken();
    }
}
