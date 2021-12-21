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
    public class RegistrationViewModel : BasePageViewModel
    {
        #region Texts
        public string HeaderText { get; } = Localization.TextTempalate;
        public string LoginTemplateText { get; } = Localization.TextTempalate;
        public string PasswordTemplateText { get; } = Localization.TextTempalate;
        public string RegistrationButtonText { get; } = Localization.TextTempalate;
        public string LoginButtonText { get; } = Localization.TextTempalate;
        #endregion

        private readonly IAuthorizationService _authorizationService;
        private readonly INetworkConnectionService _networkConnectionService;
        private readonly INavigationService _navigationService;

        private bool authorizationCompleted = false;
        private bool registrationProcessStarted = false;

        private IDisposable authorizationStatusDisposable;

        public RegistrationViewModel(
            IAuthorizationService authorizationService,
            INetworkConnectionService networkConnectionService,
            INavigationService navigationService)
        {
            this._authorizationService = authorizationService ?? throw new ArgumentNullException(nameof(authorizationService));
            this._networkConnectionService = networkConnectionService ?? throw new ArgumentNullException(nameof(networkConnectionService));
            this._navigationService = navigationService ?? throw new ArgumentNullException(nameof(navigationService));

            this.RegistrationCommand = new Command(async () => await this.RegisterAsync(), () => this.RegistrationButtonStatus);
            this.GoToLoginCommand = new Command(this.MoveToLoginPage);

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
        #endregion

        #region Commands
        public Command RegistrationCommand { get; }
        public Command GoToLoginCommand { get; }
        #endregion

        private bool RegistrationButtonStatus
        {
            get => this.registrationProcessStarted;
            set
            {
                this.registrationProcessStarted = false;
                this.RegistrationCommand.ChangeCanExecute();
            }
        }

        private async Task RegisterAsync()
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

        private void MoveToNextPage()
        {
            if (!this.PageLoaded)
            {
                return;
            }
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
