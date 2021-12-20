using MobileApp.Services.Navigation;
using MobileApp.Views.Pages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xamarin.Forms;

namespace MobileApp.ViewModels
{
    public class MainViewModel : IDisposable
    {
        private INavigationService _navigationService;

        private List<Services.Navigation.Pages> routes = new List<Services.Navigation.Pages>();

        private IDisposable changePageDisposable;

        internal MainViewModel(
            INavigationService navigationService)
        {
            this._navigationService = navigationService ?? throw new ArgumentNullException(nameof(navigationService));
        }

        public void Start()
        {
            this.changePageDisposable = this._navigationService.CurrentPageKey
                .Subscribe((page) => Device.BeginInvokeOnMainThread(async () =>
                {
                    var nextPage = this.CreatePage(page);

                    if (nextPage != null)
                    {
                        await Navigation.PushAsync(nextPage).ConfigureAwait(true);

                        if (this.Navigation.NavigationStack.Count > 0)
                        {
                            foreach (var i in Navigation.NavigationStack.Where(i => i != nextPage).ToList())
                            {
                                Navigation.RemovePage(i);
                            }
                        }
                    }
                }));
        }

        public INavigation Navigation { get; set; }

        private BasePage CreatePage(Services.Navigation.Pages page)
        {
            BasePage resultPage = null;

            switch (page)
            {
                case Services.Navigation.Pages.Login:
                    resultPage = new LoginPage();
                    break;
                case Services.Navigation.Pages.Registration:
                    resultPage = new RegistrationPage();
                    break;
                case Services.Navigation.Pages.SpotsmenList:
                    resultPage = new SportsmenListPage();
                    break;
                case Services.Navigation.Pages.AccountInfo:
                    resultPage = new AccountInfoPage();
                    break;
                default:
                    break;
            }

            return resultPage;
        }

        #region IDisposable
        private bool disposedValue;

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: dispose managed state (managed objects)
                }

                // TODO: free unmanaged resources (unmanaged objects) and override finalizer
                // TODO: set large fields to null
                disposedValue = true;
            }
        }

        // // TODO: override finalizer only if 'Dispose(bool disposing)' has code to free unmanaged resources
        // ~MainViewModel()
        // {
        //     // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
        //     Dispose(disposing: false);
        // }

        public void Dispose()
        {
            // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }
        #endregion
    }
}
