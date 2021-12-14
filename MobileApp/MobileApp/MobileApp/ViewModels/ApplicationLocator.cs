﻿using MobileApp.Configurations;
using MobileApp.Library.DataManagement.Authorization;
using MobileApp.Library.DataManagement.Authorization.Implementation;
using MobileApp.Library.DataManagement.RequestManagement;
using MobileApp.Library.DataManagement.RequestManagement.Implementation;
using MobileApp.Library.Network.NetworkConnection;
using MobileApp.Library.Network.NetworkConnection.Implementation;
using MobileApp.Library.Network.NeworkCommunication;
using MobileApp.Library.Network.NeworkCommunication.Configuration;
using MobileApp.Library.Network.NeworkCommunication.Implementation;
using MobileApp.Services.Navigation;
using MobileApp.Services.Navigation.Implementation;
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
#else
            container.Register<INetworkConnectionService, NetworkConnectionService>().AsSingleton();
            container.Register<INetworkCommunicationService, NetworkCommunicationService>().AsSingleton();
            container.Register<IAuthorizationService, AuthorizationService>().AsSingleton();
#endif
            container.Register<LoginViewModel>().AsSingleton();
            container.Register<RegistrationViewModel>().AsSingleton();
            container.Register<RelativeInfoViewModel>().AsSingleton();
            container.Register<SportsmenInfoViewModel>().AsSingleton();
            container.Register<SportsmenListViewModel>().AsSingleton();
        }

        public LoginViewModel LoginPageViewModel { get; }

        public RegistrationViewModel RegistrationPageViewModel { get; }

        public RelativeInfoViewModel RelativePageViewPageModel { get; }

        public SportsmenInfoViewModel SportsmenInfoPageViewModel { get; }

        public SportsmenListViewModel SportsmenListPageViewModel { get; }
    }
}
