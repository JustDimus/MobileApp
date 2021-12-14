using System;
using System.Collections.Generic;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using System.Text;

namespace MobileApp.Services.Navigation.Implementation
{
    internal class NavigationService : INavigationService
    {
        private readonly ReplaySubject<Pages> _currentPageKey = new ReplaySubject<Pages>(1);

        public NavigationService()
        {
            this._currentPageKey.OnNext(Pages.Login);
        }

        public IObservable<Pages> CurrentPageKey => this._currentPageKey.AsObservable();

        public void MoveToPage(Pages page)
        {
            this._currentPageKey.OnNext(page);
        }
    }
}
