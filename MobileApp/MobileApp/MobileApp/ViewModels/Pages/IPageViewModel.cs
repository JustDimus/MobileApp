using System;
using System.Collections.Generic;
using System.Text;

namespace MobileApp.ViewModels.Pages
{
    public interface IPageViewModel
    {
        void ChangePageStatus(bool pageLoadingStatus);
    }
}
