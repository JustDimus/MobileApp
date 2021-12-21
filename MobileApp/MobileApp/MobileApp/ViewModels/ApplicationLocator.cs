using MobileApp.Configurations;
using MobileApp.Library.DataManagement.Authorization;
using MobileApp.Library.DataManagement.Authorization.Implementation;
using MobileApp.Library.DataManagement.RequestManagement;
using MobileApp.Library.DataManagement.RequestManagement.Implementation;
using MobileApp.Library.Network.NetworkConnection;
using MobileApp.Library.Network.NetworkConnection.Implementation;
using MobileApp.Library.Network.NeworkCommunication;
using MobileApp.Library.Network.NeworkCommunication.Configuration;
using MobileApp.Library.Network.NeworkCommunication.Implementation;
using MobileApp.Services.Account;
using MobileApp.Services.Account.Implementation;
using MobileApp.Services.Navigation;
using MobileApp.Services.Navigation.Implementation;
using MobileApp.Services.Sportsmen;
using MobileApp.ViewModels.Pages;
using Nancy.TinyIoc;
using System;
using System.Collections.Generic;
using System.Text;

namespace MobileApp.ViewModels
{
    public class ApplicationLocator
    {
        private TinyIoCContainer container = new TinyIoCContainer();

        public ApplicationLocator()
        {
            container.Register<NetworkConfigurationManager, MobileAppNetworkConfigurationManager>();
            container.Register<IRequestManager, NetworkRequestManager>().AsSingleton();
            container.Register<INavigationService, NavigationService>().AsSingleton();
#if STUB
            container.Register<INetworkConnectionService, StubNetworkConnectionService>().AsSingleton();
            container.Register<INetworkCommunicationService, StubNetworkCommunicationService>().AsSingleton();
            container.Register<IAuthorizationService, StubAuthorizationService>().AsSingleton();
            container.Register<IAccountService, StubAccountService>().AsSingleton();
            container.Register<ISportsmenService, StubSportsmenService>().AsSingleton();
#else
            container.Register<INetworkConnectionService, NetworkConnectionService>().AsSingleton();
            container.Register<INetworkCommunicationService, NetworkCommunicationService>().AsSingleton();
            container.Register<IAuthorizationService, AuthorizationService>().AsSingleton();
            container.Register<IAccountService, AccountService>().AsSingleton();
#endif
            container.Register<MainViewModel>().AsSingleton();
            container.Register<LoginViewModel>().AsSingleton();
            container.Register<RegistrationViewModel>().AsSingleton();
            container.Register<RelativeInfoViewModel>().AsSingleton();
            container.Register<AccountInfoViewModel>().AsSingleton();
            container.Register<SportsmenListViewModel>().AsSingleton();

            this.LoginPageViewModel = container.Resolve<LoginViewModel>();
            this.MainPageViewModel = container.Resolve<MainViewModel>();
            this.RegistrationPageViewModel = container.Resolve<RegistrationViewModel>();
            this.SportsmenListPageViewModel = container.Resolve<SportsmenListViewModel>();
            this.AccountInfoPageViewModel = container.Resolve<AccountInfoViewModel>();
        }

        public LoginViewModel LoginPageViewModel { get; }

        public RegistrationViewModel RegistrationPageViewModel { get; }

        public RelativeInfoViewModel RelativePageViewPageModel { get; }

        public AccountInfoViewModel AccountInfoPageViewModel { get; }

        public SportsmenListViewModel SportsmenListPageViewModel { get; }

        public MainViewModel MainPageViewModel { get; }
    }
}
