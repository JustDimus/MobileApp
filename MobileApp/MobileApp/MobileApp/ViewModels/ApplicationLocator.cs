using MobileApp.Configurations;
using MobileApp.Library.Network.NetworkConnection;
using MobileApp.Library.Network.NetworkConnection.Implementation;
using MobileApp.Library.Network.NeworkCommunication;
using MobileApp.Library.Network.NeworkCommunication.Configuration;
using MobileApp.Library.Network.NeworkCommunication.Implementation;
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
#if STUB
            container.Register<INetworkConnectionService, StubNetworkConnectionService>().AsSingleton();
            container.Register<INetworkCommunicationService, StubNetworkCommunicationService>();
#else
            container.Register<INetworkConnectionService, NetworkConnectionService>().AsSingleton();
            container.Register<INetworkCommunicationService, NetworkCommunicationService>().AsSingleton();

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
