using System;
using System.Collections.Generic;
using System.Text;
using Localization = MobileApp.Resources.Texts.ApplicationLocalization;

namespace MobileApp.ViewModels.Pages
{
    public class RelativeInfoViewModel : BasePageViewModel
    {
        #region Texts
        public string HeaderText { get; } = Localization.TextTempalate;
        public string UsernameText { get; } = Localization.TextTempalate;
        public string NameText { get; } = Localization.TextTempalate;
        public string PhoneText { get; } = Localization.TextTempalate;
        public string EmailText { get; } = Localization.TextTempalate;
        public string BirthdayText { get; } = Localization.TextTempalate;
        public string SexText { get; } = Localization.TextTempalate;
        public string SaveButtonText { get; } = Localization.TextTempalate;
        public string ChangePasswordButtonText { get; } = Localization.TextTempalate;
        #endregion

        public RelativeInfoViewModel()
        {
            
        }
    }
}
