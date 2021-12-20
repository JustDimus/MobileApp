using MobileApp.ViewModels.Pages;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace MobileApp.Views.Pages
{
    public class BasePage : ContentPage
    {
        public BasePage()
        {
            this.Appearing += BasePage_Appeared;
            this.Disappearing += BasePage_Disappeared;
        }

        private void BasePage_Appeared(object sender, EventArgs e)
        {
            if (BindingContext is BasePageViewModel viewModel)
            {
                viewModel.ChangePageStatus(true);
            }
        }

        private void BasePage_Disappeared(object sender, EventArgs e)
        {
            if (BindingContext is BasePageViewModel viewModel)
            {
                viewModel.ChangePageStatus(false);
            }
        }
    }
}
