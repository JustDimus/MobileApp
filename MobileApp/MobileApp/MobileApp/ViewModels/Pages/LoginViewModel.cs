using MobileApp.Library.DataManagement.Authorization;
using MobileApp.Library.DataManagement.Models;
using MobileApp.Library.Network.NetworkConnection;
using MobileApp.Services.Navigation;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Localization = MobileApp.Resources.Texts.ApplicationLocalization;

namespace MobileApp.ViewModels.Pages
{
    public class LoginViewModel : BasePageViewModel
    {
        #region Texts
        public string HeaderText { get; } = Localization.Login_HeaderText;
        public string LoginTemplateText { get; } = Localization.Login_LoginTemplateText;
        public string PasswordTemplateText { get; } = Localization.Login_PasswordTemplateText;
        public string LoginButtonText { get; } = Localization.Login_LoginButtonText;
        public string RegistrationButtonText { get; } = Localization.Login_RegistrationButtonText;
        public string ErrorText { get; } = Localization.Login_ErrorText;
        #endregion

        private readonly IAuthorizationService _authorizationService;
        private readonly INetworkConnectionService _networkConnectionService;
        private readonly INavigationService _navigationService;

        private bool authorizationCompleted = false;
        private bool loginProcessStarted = false;

        private IDisposable authorizationStatusDisposable;

        public LoginViewModel(
            IAuthorizationService authorizationService,
            INetworkConnectionService networkConnectionService,
            INavigationService navigationService)
        {
            this._authorizationService = authorizationService ?? throw new ArgumentNullException(nameof(authorizationService));
            this._networkConnectionService = networkConnectionService ?? throw new ArgumentNullException(nameof(networkConnectionService));
            this._navigationService = navigationService ?? throw new ArgumentNullException(nameof(navigationService));

            this.LoginCommand = new Command(async () => await this.LoginAsync(), () => this.LoginButtonStatus);
            this.GoToRegistrationCommand = new Command(this.MoveToRegistrationPage);

            this.authorizationStatusDisposable = this._authorizationService.AuthorizationStatusObservable.Subscribe(this.OnAuthorizationStatusChanged);
        }

        #region Bindings
        private string login;
        public string LoginProperty
        {
            get => this.login;
            set
            {
                this.login = value;
                this.OnPropertyChanged();
                this.OnPropertyChanged(nameof(this.LoginButtonStatus));
            }
        }

        private string password;
        public string PasswordProperty
        {
            get => this.password;
            set
            {
                this.password = value;
                this.OnPropertyChanged();
                this.LoginCommand.ChangeCanExecute();
            }
        }

        private bool errorStatus;
        public bool ShowError
        {
            get => this.errorStatus;
            set
            {
                this.errorStatus = value;
                this.LoginCommand.ChangeCanExecute();
            }
        }
        #endregion

        #region Commands
        public Command LoginCommand { get; }
        public Command GoToRegistrationCommand { get; }
        #endregion

        protected override void OnPageLoaded()
        {
            if (this.authorizationCompleted)
            {
                this.MoveToNextPage();
            }
        }

        private bool LoginButtonStatus
        {
            get => !this.loginProcessStarted
                && !string.IsNullOrWhiteSpace(this.LoginProperty)
                && !string.IsNullOrWhiteSpace(this.PasswordProperty);
            set
            {
                this.loginProcessStarted = value;
                this.LoginCommand.ChangeCanExecute();
            }
        }

        private async Task LoginAsync()
        {
            this.LoginButtonStatus = true;

            var loginData = new LoginData()
            {
                Login = this.LoginProperty,
                Password = this.PasswordProperty
            };

            var result = await this._authorizationService.AuthorizeAsync(loginData);

            if (result == AuthorizationStatuses.Authorized)
            {
                this.MoveToNextPage();
            }

            this.LoginButtonStatus = false;
        }

        private void MoveToRegistrationPage()
        {
            if (!this.PageLoaded)
            {
                return;
            }

            this._navigationService.MoveToPage(Services.Navigation.Pages.Registration);
        }

        private void MoveToNextPage()
        {
            if (!this.PageLoaded)
            {
                return;
            }

            this._navigationService.MoveToPage(Services.Navigation.Pages.AccountInfo);
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
                    this.authorizationCompleted = false;
                    break;
            }

            this.MoveToNextPage();
        }
    }
}
