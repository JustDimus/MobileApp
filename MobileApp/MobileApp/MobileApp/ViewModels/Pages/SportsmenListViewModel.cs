using MobileApp.Library.DataManagement.Authorization;
using MobileApp.Library.Network.NetworkConnection;
using MobileApp.Services.Account;
using MobileApp.Services.Models;
using MobileApp.Services.Navigation;
using MobileApp.Services.Sportsmen;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Localization = MobileApp.Resources.Texts.ApplicationLocalization;

namespace MobileApp.ViewModels.Pages
{
    public class SportsmenListViewModel : BasePageViewModel
    {
        #region Texts
        public string HeaderText { get; } = Localization.TextTempalate;
        public string AddSportsmenButtonText { get; } = Localization.TextTempalate;
        public string DeleteSportsmenButtonText { get; } = Localization.TextTempalate;
        public string SportsmenHeaderText { get; } = Localization.TextTempalate;
        public string LoginButtonText { get; } = Localization.TextTempalate;
        public string NameText { get; } = Localization.TextTempalate;
        public string PhoneText { get; } = Localization.TextTempalate;
        public string EmailText { get; } = Localization.TextTempalate;
        public string BirthdayText { get; } = Localization.TextTempalate;
        public string SexText { get; } = Localization.TextTempalate;
        #endregion

        private readonly INavigationService _navigationService;
        private readonly IAuthorizationService _authorizationService;
        private readonly IAccountService _accountService;
        private readonly ISportsmenService _sportsmenService;

        private bool dataReloadStatus = false;

        private IDisposable authorizationDisposable;

        internal SportsmenListViewModel(
            INavigationService navigationService,
            ISportsmenService sportsmenService,
            IAuthorizationService authorizationService)
        {
            this._navigationService = navigationService ?? throw new ArgumentNullException(nameof(navigationService));
            this._authorizationService = authorizationService ?? throw new ArgumentNullException(nameof(authorizationService));



            this.authorizationDisposable = this._authorizationService.AuthorizationStatusObservable.Subscribe(this.OnAuthorizationChanged);
        }

        #region Commands

        #endregion

        #region Bindings
        public List<AccountData> sportsmenList;
        public List<AccountData> SportsmenDataList
        {
            get => this.sportsmenList;
            set
            {
                this.sportsmenList = value;
                this.OnPropertyChanged();
            }
        }
        public bool DataReloadError
        {
            get => this.dataReloadStatus;
            set
            {
                this.dataReloadStatus = value;
                this.OnPropertyChanged();
            }
        }
        public AccountData OnSportsmenSelected
        {
            set
            {
                this.SwitchToSportsmenPage(value);
            }
        }
        #endregion

        protected override void OnPageLoaded()
        {
            this.SportsmenDataList = null;
            this.DataReloadError = false;
            this._sportsmenService.ClearSelectedSportsmen();
            _ = this.ReloadPageData();
        }

        private void SwitchToSportsmenPage(AccountData accountData)
        {
            this._sportsmenService.SelectSportsmen(accountData);
            this.MoveToSportsmenPage();
        }

        private async Task ReloadPageData()
        {
            var sportsmenListResult = await this._sportsmenService.GetSportsmenListAsync();

            if (sportsmenListResult.IsSuccessful)
            {
                this.DataReloadError = false;
                this.SportsmenDataList = sportsmenListResult.Result;
            }
            else
            {
                this.DataReloadError = true;
            }
        }

        private void MoveToSportsmenPage()
        {
            if (!this.PageLoaded)
            {
                return;
            }

            this._navigationService.MoveToPage(Services.Navigation.Pages.AccountInfo);
        }

        private void MoveToLoginPage()
        {
            if (!this.PageLoaded)
            {
                return;
            }

            this._navigationService.MoveToPage(Services.Navigation.Pages.Login);
        }

        private void OnAuthorizationChanged(AuthorizationStatuses status)
        {
            if (status != AuthorizationStatuses.Authorized)
            {
                this.MoveToLoginPage();
            }
        }
    }
}
