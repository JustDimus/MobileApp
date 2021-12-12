using System;
using System.Collections.Generic;
using System.Text;
using Localization = MobileApp.Resources.Texts.ApplicationLocalization;

namespace MobileApp.ViewModels.Pages
{
    public class RegistrationPageViewModel : BasePageViewModel
    {
        #region Texts
        public string HeaderText { get; } = Localization.TextTempalate;
        public string LoginTemplateText { get; } = Localization.TextTempalate;
        public string PasswordTemplateText { get; } = Localization.TextTempalate;
        public string RegistrationButtonText { get; } = Localization.TextTempalate;
        public string LoginButtonText { get; } = Localization.TextTempalate;
        #endregion

        public RegistrationPageViewModel()
        {

        }
    }
}
