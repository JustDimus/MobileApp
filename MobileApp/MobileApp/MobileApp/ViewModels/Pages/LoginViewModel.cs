using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;
using Localization = MobileApp.Resources.Texts.ApplicationLocalization;

namespace MobileApp.ViewModels.Pages
{
    public class LoginViewModel : BasePageViewModel
    {
        #region Texts
        public string HeaderText { get; } = Localization.TextTempalate;
        public string LoginTemplateText { get; } = Localization.TextTempalate;
        public string PasswordTemplateText { get; } = Localization.TextTempalate;
        public string LoginButtonText { get; } = Localization.TextTempalate;
        public string RegistrationButtonText { get; } = Localization.TextTempalate;
        #endregion

        public LoginViewModel()
        {
            
        }

        public Command LoginCommand { get; }
        public Command GoToRegistrationCommand { get; }


    }
}
