using MobileApp.Library.DataManagement.Authorization;
using MobileApp.Services.Account;
using MobileApp.Services.Models;
using MobileApp.Services.Navigation;
using MobileApp.Services.Sportsmen;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Localization = MobileApp.Resources.Texts.ApplicationLocalization;

namespace MobileApp.ViewModels.Pages
{
    public class AccountInfoViewModel : BasePageViewModel
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
        private ISportsmenService _sportsmenService;

        private bool authorizationCompleted = false;
        private bool refreshCompleted = false;
        private bool isAthlet = false;
        private bool isReadonly = false;
        private bool isAthletDataLoadingCompleted = false;
        private AccountData loadedAccountData;
        private List<NutritionData> loadedNutritionData;
        private List<BodyData> loadedBodyData;

        private IDisposable authorizationStatusDisposable;
        private Task pageReloadingTask;

        internal AccountInfoViewModel(
            IAuthorizationService authorizationService,
            IAccountService accountService,
            ISportsmenService sportsmenService,
            INavigationService navigationService)
        {
            this._navigationService = navigationService ?? throw new ArgumentNullException(nameof(navigationService));
            this._authorizationService = authorizationService ?? throw new ArgumentNullException(nameof(authorizationService));
            this._accountService = accountService ?? throw new ArgumentNullException(nameof(accountService));
            this._sportsmenService = sportsmenService ?? throw new ArgumentNullException(nameof(sportsmenService));

            this.authorizationStatusDisposable = this._authorizationService.AuthorizationStatusObservable.Subscribe(this.OnAuthorizationStatusChanged);
        }

        #region Bindings
        public AccountData CurrentAccount
        {
            get => this.loadedAccountData;
            private set
            {
                this.loadedAccountData = value;
            }
        }

        public bool RefreshStatus
        {
            get => this.refreshCompleted;
            private set
            {
                this.refreshCompleted = RefreshStatus;
                this.OnPropertyChanged();
            }
        }

        public bool ShowAthletInfo
        {
            get => this.isAthlet;
            private set
            {
                this.isAthlet = value;
                this.OnPropertyChanged();
            }
        }

        public bool IsReadonly
        {
            get => this.isReadonly;
            set
            {
                this.isReadonly = value;
                this.OnPropertyChanged();
            }
        }

        public bool AthletDataLoadingStatus
        {
            get => this.isAthletDataLoadingCompleted;
            set
            {
                this.isAthletDataLoadingCompleted = value;
                this.OnPropertyChanged();
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
            this.RefreshStatus = true;
            this.IsReadonly = true;
        }

        private async Task ReloadPageDataAsync()
        {
            var currentUserRole = await this._accountService.GetUserRoleAsync();

            if (currentUserRole.IsSuccessful && currentUserRole.Result != null && currentUserRole.Result.Any())
            {
                var userRole = (UserRoles)currentUserRole.Result.First();

                switch(userRole)
                {
                    case UserRoles.Athlet:
                        await this.LoadBaseAccountInfoAsync();

                        var currentAccount = await this._accountService.GetAccountDataAsync();
                        await this.LoadSpotsmenInfoAsync(currentAccount.Result);
                        break;
                    case UserRoles.Relative:
                    case UserRoles.Coach:
                    default:
                        var selectedAccount = this._sportsmenService.SelectedSportsmen;
                        if (selectedAccount == null)
                        {
                            await this.LoadBaseAccountInfoAsync();
                        }
                        else
                        {
                            this.LoadBaseAccountInfoAsync(selectedAccount);
                            await this.LoadSpotsmenInfoAsync(selectedAccount);
                        }    
                        break;
                }
            }
            else
            {
                this.RefreshStatus = false;
            }
        }

        private async Task LoadBaseAccountInfoAsync()
        {
            this.IsReadonly = false;

            var accountData = await this._accountService.GetAccountDataAsync();

            if (accountData.IsSuccessful)
            {
                this.LoadBaseAccountInfoAsync(accountData.Result);
            }
            else
            {
                this.RefreshStatus = false;
            }
        }
        
        private void LoadBaseAccountInfoAsync(AccountData accountData)
        {
            if (accountData == null)
            {
                return;
            }

            this.CurrentAccount = accountData;
        }

        private async Task LoadSpotsmenInfoAsync(AccountData accountData)
        {
            var accountBodyData = await this._sportsmenService.GetBodyDataListAsync(accountData);
            var accountNutritionData = await this._sportsmenService.GetNutritionDataListAsync(accountData);

            this.AthletDataLoadingStatus = accountBodyData.IsSuccessful && accountNutritionData.IsSuccessful;

            if (this.AthletDataLoadingStatus)
            {
                this.loadedBodyData = accountBodyData.Result;
                this.loadedNutritionData = accountNutritionData.Result;
            }
        }

        private async Task RefreshGraphics()
        {

        }

        private async Task ClearGraphics()
        {

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
