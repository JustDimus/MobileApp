using MobileApp.Library.DataManagement.Authorization;
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
        public string HeaderText { get; } = Localization.TextTempalate;
        public string LoginTemplateText { get; } = Localization.TextTempalate;
        public string PasswordTemplateText { get; } = Localization.TextTempalate;
        public string LoginButtonText { get; } = Localization.TextTempalate;
        public string RegistrationButtonText { get; } = Localization.TextTempalate;
        public string ErrorText { get; } = Localization.TextTempalate;
        #endregion

        private readonly IAuthorizationService _authorizationService;
        private readonly INetworkConnectionService _networkConnectionService;
        private readonly INavigationService _navigationService;

        private bool authorizationCompleted = false;
        private bool loginProcessStarted = false;

        private IDisposable authorizationStatusDisposable;

        internal LoginViewModel(
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
            }
        }

        private bool errorStatus;
        public bool ShowError
        {
            get => this.errorStatus;
            set
            {
                this.errorStatus = value;
                this.OnPropertyChanged();
            }
        }
        #endregion

        #region Commands
        public Command LoginCommand { get; }
        public Command GoToRegistrationCommand { get; }
        #endregion

        private bool LoginButtonStatus
        {
            get => this.loginProcessStarted;
            set
            {
                this.loginProcessStarted = value;
                this.LoginCommand.ChangeCanExecute();
            }
        }

        private async Task LoginAsync()
        {
            this.LoginButtonStatus = false;



            this.LoginButtonStatus = true;
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

            //TODO
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
