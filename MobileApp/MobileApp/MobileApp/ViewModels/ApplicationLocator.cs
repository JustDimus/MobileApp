using MobileApp.Network.NetworkConnection;
using MobileApp.Network.NetworkConnection.Implementation;
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
#if STUB
            container.Register<INetworkConnectionService, StubNetworkConnectionService>().AsSingleton();
#else
            container.Register<INetworkConnectionService, NetworkConnectionService>().AsSingleton();

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
