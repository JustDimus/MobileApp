﻿using MobileApp.Library.DataManagement.Authorization;
using MobileApp.Library.Network.NetworkConnection;
using MobileApp.Services.Navigation;
using System;
using System.Collections.Generic;
using System.Text;
using Localization = MobileApp.Resources.Texts.ApplicationLocalization;

namespace MobileApp.ViewModels.Pages
{
    public class SportsmenListViewModel : BasePageViewModel
    {
        #region Texts
        public string HeaderText { get; } = Localization.TextTempalate;
        public string AddSportsmenButtonText { get; } = Localization.TextTempalate;
        public string DeleteSportsmenButtonText { get; } = Localization.TextTempalate;
        public string SportsmenHeaderText { get; } = Localization.TextTempalate;
        public string LoginButtonText { get; } = Localization.TextTempalate;
        public string NameText { get; } = Localization.TextTempalate;
        public string PhoneText { get; } = Localization.TextTempalate;
        public string EmailText { get; } = Localization.TextTempalate;
        public string BirthdayText { get; } = Localization.TextTempalate;
        public string SexText { get; } = Localization.TextTempalate;
        #endregion

        private readonly INavigationService _navigationService;

        internal SportsmenListViewModel(
            INavigationService navigationService)
        {
            this._navigationService = navigationService ?? throw new ArgumentNullException(nameof(navigationService));


        }
    }
}
