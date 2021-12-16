using MobileApp.Library.DataManagement.Authorization;
using MobileApp.Services.Account;
using MobileApp.Services.Models;
using MobileApp.Services.Navigation;
using MobileApp.Services.Sportsmen;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Localization = MobileApp.Resources.Texts.ApplicationLocalization;

namespace MobileApp.ViewModels.Pages
{
    public class SportsmenInfoViewModel : BasePageViewModel
    {
        #region Texts
        public string HeaderText { get; } = Localization.TextTempalate;
        public string NameText { get; } = Localization.TextTempalate;
        public string PhoneText { get; } = Localization.TextTempalate;
        public string EmailText { get; } = Localization.TextTempalate;
        public string BirthdayText { get; } = Localization.TextTempalate;
        public string SexText { get; } = Localization.TextTempalate;
        public string NutritionText { get; } = Localization.TextTempalate;
        public string BodyText { get; } = Localization.TextTempalate;
        public string GroupsText { get; } = Localization.TextTempalate;
        public string ErrorText { get; } = Localization.TextTempalate;
        #endregion

        private IAuthorizationService _authorizationService;
        private INavigationService _navigationService;
        private IAccountService _accountService;

        private bool authorizationCompleted = false;
        private AccountData loadedAccountData;

        private IDisposable authorizationStatusDisposable;
        private Task pageReloadingTask;

        internal SportsmenInfoViewModel(
            IAuthorizationService authorizationService,
            IAccountService accountService,
            INavigationService navigationService)
        {
            this._navigationService = navigationService ?? throw new ArgumentNullException(nameof(navigationService));
            this._authorizationService = authorizationService ?? throw new ArgumentNullException(nameof(authorizationService));
            this._accountService = accountService ?? throw new ArgumentNullException(nameof(accountService));

            this.authorizationStatusDisposable = this._authorizationService.AuthorizationStatusObservable.Subscribe(this.OnAuthorizationStatusChanged);
        }

        #region Bindings
        public AccountData CurrentAccount
        {
            get => this.loadedAccountData;
            set
            {
                this.loadedAccountData = value;
            }
        }
        #endregion

        protected override void OnPageLoaded()
        {
            this.pageReloadingTask = ReloadPageDataAsync();
        }

        protected override void OnPageUnloaded()
        {
            this.ClearPageData();
        }

        private void ClearPageData()
        {
            this.CurrentAccount = null;
        }

        private async Task ReloadPageDataAsync()
        {
            var accountData = await this._accountService.GetAccountDataAsync();

            if (accountData.IsSuccessful)
            {
                this.loadedAccountData = accountData.Result;
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
                    this.authorizationCompleted = true;
                    break;
                case AuthorizationStatuses.Unauthorized:
                case AuthorizationStatuses.Undefined:
                default:
                    this.MoveToLoginPage();
                    break;
            }
        }
    }
}
