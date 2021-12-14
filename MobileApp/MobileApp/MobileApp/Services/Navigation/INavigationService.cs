using System;
using System.Collections.Generic;
using System.Text;

namespace MobileApp.Services.Navigation
{
    internal interface INavigationService
    {
        void MoveToPage(Pages page);

        IObservable<Pages> CurrentPageKey { get; }
    }
}
