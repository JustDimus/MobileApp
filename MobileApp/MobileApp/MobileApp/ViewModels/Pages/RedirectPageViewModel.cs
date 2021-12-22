using MobileApp.Library.DataManagement.Authorization;
using MobileApp.Library.Network.NetworkConnection;
using MobileApp.Services.Account;
using MobileApp.Services.Navigation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MobileApp.ViewModels.Pages
{
    public class RedirectPageViewModel : BasePageViewModel
    {
        private IAuthorizationService _authorizationService;
        private INavigationService _navigationService;
        private IAccountService _accountService;

        private IDisposable authorizationStatusDisposable;

        public RedirectPageViewModel(
            IAuthorizationService authorizationService,
            INavigationService navigationService,
            IAccountService accountService)
        {
            this._authorizationService = authorizationService ?? throw new ArgumentNullException(nameof(authorizationService));
            this._navigationService = navigationService ?? throw new ArgumentNullException(nameof(navigationService));
            this._accountService = accountService ?? throw new ArgumentNullException(nameof(accountService));

            this.authorizationStatusDisposable = this._authorizationService.AuthorizationStatusObservable.Subscribe(this.OnAuthorizationStatusChanged);
        }

        protected override async void OnPageLoaded()
        {
            var accountRoles = await this._accountService.GetUserRoleAsync();

            if (accountRoles.IsSuccessful && accountRoles.Result != null && accountRoles.Result.Any())
            {
                var role = accountRoles.Result.First();

                switch (role)
                {
                    case Services.Models.UserRoles.Athlet:
                        this._navigationService.MoveToPage(Services.Navigation.Pages.AccountInfo);
                        break;
                    case Services.Models.UserRoles.Coach:
                    case Services.Models.UserRoles.Relative:
                    default:
                        this._navigationService.MoveToPage(Services.Navigation.Pages.SportsmenList);
                        break;
                }
            }
        }

        private void MoveToLoginPage()
        {
            if (!this.PageLoaded)
            {
                return;
            }

            this._navigationService.MoveToPage(Services.Navigation.Pages.Login);
        }

        private void OnAuthorizationStatusChanged(AuthorizationStatuses status)
        {
            switch (status)
            {
                case AuthorizationStatuses.Authorized:
                    break;
                case AuthorizationStatuses.Unauthorized:
                case AuthorizationStatuses.Undefined:
                default:
                    MoveToLoginPage();
                    break;
            }
        }
    }
}
