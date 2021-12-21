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
        public string HeaderText { get; } = Localization.SportsmenList_HeaderText;
        public string AddSportsmenButtonText { get; } = Localization.SportsmenList_AddSportsmenButtonText;
        public string DeleteSportsmenButtonText { get; } = Localization.SportsmenList_DeleteSportsmenButtonText;
        public string SportsmenHeaderText { get; } = Localization.SportsmenList_SportsmenHeaderText;
        public string LoginButtonText { get; } = Localization.SportsmenList_LoginButtonText;
        public string NameText { get; } = Localization.SportsmenList_NameText;
        public string PhoneText { get; } = Localization.SportsmenList_PhoneText;
        public string EmailText { get; } = Localization.SportsmenList_EmailText;
        public string BirthdayText { get; } = Localization.SportsmenList_BirthdayText;
        public string SexText { get; } = Localization.SportsmenList_SexText;
        public string RefreshDataError { get; } = Localization.SportsmenList_RefreshDataError;
        #endregion

        private readonly INavigationService _navigationService;
        private readonly IAuthorizationService _authorizationService;
        private readonly IAccountService _accountService;
        private readonly ISportsmenService _sportsmenService;

        private bool dataReloadStatus = false;

        private IDisposable authorizationDisposable;

        public SportsmenListViewModel(
            INavigationService navigationService,
            ISportsmenService sportsmenService,
            IAuthorizationService authorizationService)
        {
            this._navigationService = navigationService ?? throw new ArgumentNullException(nameof(navigationService));
            this._authorizationService = authorizationService ?? throw new ArgumentNullException(nameof(authorizationService));
            this._sportsmenService = sportsmenService ?? throw new ArgumentNullException(nameof(sportsmenService));

            this.AddSportsmenCommand = new Command(() => { });
            this.GoToProfileCommand = new Command(this.MoveToProfilePage);

            this.authorizationDisposable = this._authorizationService.AuthorizationStatusObservable.Subscribe(this.OnAuthorizationChanged);
        }

        #region Commands
        public Command AddSportsmenCommand { get; }
        public Command GoToProfileCommand { get; }
        #endregion

        #region Bindings
        public IEnumerable<AccountData> sportsmenList;
        public IEnumerable<AccountData> SportsmenDataList
        {
            get => this.sportsmenList;
            set
            {
                this.sportsmenList = value;
                this.OnPropertyChanged();
            }
        }
        public bool DataReloadStatus
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
            this.DataReloadStatus = false;
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
                this.DataReloadStatus = true;
                this.SportsmenDataList = sportsmenListResult.Result;
            }
            else
            {
                this.DataReloadStatus = false;
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

        private void MoveToProfilePage()
        {
            if (!this.PageLoaded)
            {
                return;
            }

            this._sportsmenService.ClearSelectedSportsmen();
            this._navigationService.MoveToPage(Services.Navigation.Pages.AccountInfo);
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
