using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;

namespace MobileApp.ViewModels.Pages
{
    public abstract class BasePageViewModel : INotifyPropertyChanged, IPageViewModel
    {
        protected bool PageLoaded { get; private set; } = false;

        public void ChangePageStatus(bool pageLoadingStatus)
        {
            if (pageLoadingStatus && !this.PageLoaded)
            {
                this.PageLoaded = true;
                this.OnPageLoaded();
            }

            if (!pageLoadingStatus && this.PageLoaded)
            {
                this.PageLoaded = false;
                this.OnPageUnloaded();
            }
        }

        protected virtual void OnPageLoaded()
        {

        }

        protected virtual void OnPageUnloaded()
        {

        }

        #region INotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion
    }
}
