using MobileApp.Library.DataManagement.Authorization;
using MobileApp.Services.Account;
using MobileApp.Services.Models;
using MobileApp.Services.Navigation;
using MobileApp.Services.Sportsmen;
using SkiaSharp;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Forms;
using Localization = MobileApp.Resources.Texts.ApplicationLocalization;

namespace MobileApp.ViewModels.Pages
{
    public class AccountInfoViewModel : BasePageViewModel
    {
        #region Texts
        public string HeaderText { get; } = Localization.AccountInfo_HeaderText;
        public string NameText { get; } = Localization.AccountInfo_NameText;
        public string PhoneText { get; } = Localization.AccountInfo_PhoneText;
        public string EmailText { get; } = Localization.AccountInfo_EmailText;
        public string BirthdayText { get; } = Localization.AccountInfo_BirthdayText;
        public string SexText { get; } = Localization.AccountInfo_SexText;
        public string NutritionText { get; } = Localization.AccountInfo_NutritionText;
        public string BodyText { get; } = Localization.AccountInfo_BodyText;
        public string GroupsText { get; } = Localization.AccountInfo_GroupsInfo;
        public string ErrorText { get; } = Localization.AccountInfo_ErrorText;
        public string SaveChangesButtonText { get; } = Localization.AccountInfo_SaveChangesButtonText;
        public string ChangePasswordButtonText { get; } = Localization.AccountInfo_ChangePasswordButtonText;
        #endregion

        private const int IMAGE_WIDTH = 450;
        private const int IMAGE_HEIGHT = 250;

        private IAuthorizationService _authorizationService;
        private INavigationService _navigationService;
        private IAccountService _accountService;
        private ISportsmenService _sportsmenService;

        private bool authorizationCompleted = false;
        private bool refreshCompleted = false;
        private bool isAthlet = false;
        private bool isReadonly = false;
        private bool isAthletDataLoadingCompleted = false;
        private AccountData loadedAccountData;
        private List<NutritionData> loadedNutritionData;
        private List<BodyData> loadedBodyData;

        private IDisposable authorizationStatusDisposable;
        private Task pageReloadingTask;

        public AccountInfoViewModel(
            IAuthorizationService authorizationService,
            IAccountService accountService,
            ISportsmenService sportsmenService,
            INavigationService navigationService)
        {
            this._navigationService = navigationService ?? throw new ArgumentNullException(nameof(navigationService));
            this._authorizationService = authorizationService ?? throw new ArgumentNullException(nameof(authorizationService));
            this._accountService = accountService ?? throw new ArgumentNullException(nameof(accountService));
            this._sportsmenService = sportsmenService ?? throw new ArgumentNullException(nameof(sportsmenService));

            this.GoToSportsmenListCommand = new Command(this.MoveToSportsmenListPage);
            this.SaveChangesCommand = new Command(async () => await this.SaveAccountInfoChangesAsync());

            this.authorizationStatusDisposable = this._authorizationService.AuthorizationStatusObservable.Subscribe(this.OnAuthorizationStatusChanged);
        }

        #region Commands
        public Command GoToSportsmenListCommand { get; }
        public Command SaveChangesCommand { get; }
        #endregion

        #region Bindings
        public AccountData CurrentAccount
        {
            get => this.loadedAccountData;
            private set
            {
                this.loadedAccountData = value;
                this.OnPropertyChanged();
            }
        }

        public bool RefreshStatus
        {
            get => this.refreshCompleted;
            private set
            {
                this.refreshCompleted = RefreshStatus;
                this.OnPropertyChanged();
            }
        }

        public bool ShowAthletInfo
        {
            get => this.isAthlet;
            private set
            {
                this.isAthlet = value;
                this.OnPropertyChanged();
            }
        }

        public bool IsReadonly
        {
            get => this.isReadonly;
            set
            {
                this.isReadonly = value;
                this.OnPropertyChanged();
            }
        }

        public bool AthletDataLoadingStatus
        {
            get => this.isAthletDataLoadingCompleted;
            set
            {
                this.isAthletDataLoadingCompleted = value;
                this.OnPropertyChanged();
            }
        }
        private ImageSource bodyImageSource;
        public ImageSource BodyImageSource
        {
            get => this.bodyImageSource;
            set
            {
                this.bodyImageSource = value;
                this.OnPropertyChanged();
            }
        }
        private ImageSource nutritionImageSource;
        public ImageSource NutritionImageSource
        {
            get => this.nutritionImageSource;
            set
            {
                this.nutritionImageSource = value;
                this.OnPropertyChanged();
            }
        }
        private bool showRedirectButtons;
        public bool ShowRedirectSection
        {
            get => this.showRedirectButtons;
            set
            {
                this.showRedirectButtons = value;
                this.OnPropertyChanged();
            }
        }
        #endregion

        protected override void OnPageLoaded()
        {
            this.ClearPageData();
            this.pageReloadingTask = ReloadPageDataAsync();
        }

        protected override void OnPageUnloaded()
        {
            this.ShowAthletInfo = false;
            this.ClearGraphics();
        }

        private void ClearPageData()
        {
            this.ShowRedirectSection = false;
            this.CurrentAccount = null;
            this.RefreshStatus = true;
            this.IsReadonly = true;
            this.ShowAthletInfo = false;
            this.ClearGraphics();
        }

        private async Task ReloadPageDataAsync()
        {
            var currentUserRole = await this._accountService.GetUserRoleAsync();

            if (currentUserRole.IsSuccessful && currentUserRole.Result != null && currentUserRole.Result.Any())
            {
                var userRole = currentUserRole.Result.First();

                switch (userRole)
                {
                    case UserRoles.Athlet:
                        await this.LoadBaseAccountInfoAsync();
                        var currentAccount = await this._accountService.GetAccountDataAsync();
                        await this.LoadSportsmenInfoAsync(currentAccount.Result);
                        this.ShowRedirectSection = false;
                        break;
                    case UserRoles.Relative:
                    case UserRoles.Coach:
                    default:
                        var selectedAccount = this._sportsmenService.SelectedSportsmen;
                        if (selectedAccount == null)
                        {
                            await this.LoadBaseAccountInfoAsync();
                        }
                        else
                        {
                            this.LoadBaseAccountInfoAsync(selectedAccount);
                            await this.LoadSportsmenInfoAsync(selectedAccount);
                        }
                        this.ShowRedirectSection = true;
                        break;
                }
            }
            else
            {
                this.RefreshStatus = false;
            }
        }

        private async Task SaveAccountInfoChangesAsync()
        {

        }

        private async Task LoadBaseAccountInfoAsync()
        {
            this.IsReadonly = false;

            var accountData = await this._accountService.GetAccountDataAsync();

            if (accountData.IsSuccessful)
            {
                this.LoadBaseAccountInfoAsync(accountData.Result);
            }
            else
            {
                this.RefreshStatus = false;
            }
        }

        private void LoadBaseAccountInfoAsync(AccountData accountData)
        {
            if (accountData == null)
            {
                return;
            }

            this.CurrentAccount = accountData;
        }

        private async Task LoadSportsmenInfoAsync(AccountData accountData)
        {
            this.ShowAthletInfo = true;

            var accountBodyData = await this._sportsmenService.GetBodyDataListAsync(accountData);
            var accountNutritionData = await this._sportsmenService.GetNutritionDataListAsync(accountData);

            this.AthletDataLoadingStatus = accountBodyData.IsSuccessful && accountNutritionData.IsSuccessful;

            if (this.AthletDataLoadingStatus)
            {
                this.loadedBodyData = accountBodyData.Result;
                this.loadedNutritionData = accountNutritionData.Result;
                await this.RefreshGraphics();
            }
        }

        private async Task RefreshGraphics()
        {
            var bodyResult = await Task.Run<Stream>(this.CreateBodyInfoImage);
            var nutritionResult = await Task.Run<Stream>(this.CreateNutritionInfoImage);

            this.BodyImageSource = ImageSource.FromStream(() => bodyResult);
            this.NutritionImageSource = ImageSource.FromStream(() => nutritionResult);
        }

        private Stream CreateBodyInfoImage()
        {
            using (SKBitmap bitmap = new SKBitmap(IMAGE_WIDTH, IMAGE_HEIGHT))
            {
                using (SKCanvas canvas = new SKCanvas(bitmap))
                {
                    using (SKPaint paint = new SKPaint()
                    {
                        Style = SKPaintStyle.Stroke,
                        Color = SKColors.Black,
                        StrokeWidth = 5,
                        StrokeCap = SKStrokeCap.Round
                    })
                    {
                        using (SKPath path = new SKPath())
                        {
                            this.CreatePath(
                                path,
                                IMAGE_WIDTH,
                                IMAGE_HEIGHT,
                                this.loadedBodyData.Select(i => i.Height), i => (int)i.GetValueOrDefault(),
                                (int)this.loadedBodyData.Select(i => i.Height).Max().GetValueOrDefault());
                            canvas.DrawPath(path, paint);
                        }
                    }

                    using (SKPaint paint = new SKPaint()
                    {
                        Style = SKPaintStyle.Stroke,
                        Color = SKColors.Green,
                        StrokeWidth = 5,
                        StrokeCap = SKStrokeCap.Round
                    })
                    {
                        using (SKPath path = new SKPath())
                        {
                            this.CreatePath(
                                path,
                                IMAGE_WIDTH,
                                IMAGE_HEIGHT,
                                this.loadedBodyData.Select(i => i.Weight), i => (int)i.GetValueOrDefault(),
                                (int)this.loadedBodyData.Select(i => i.Weight).Max().GetValueOrDefault());
                            canvas.DrawPath(path, paint);
                        }
                    }
                }

                return bitmap.Encode(SKEncodedImageFormat.Png, 1).AsStream();
            }
        }

        private Stream CreateNutritionInfoImage()
        {
            using (SKBitmap bitmap = new SKBitmap(IMAGE_WIDTH, IMAGE_HEIGHT))
            {
                using (SKCanvas canvas = new SKCanvas(bitmap))
                {
                    using (SKPaint paint = new SKPaint()
                    {
                        Style = SKPaintStyle.Stroke,
                        Color = SKColors.Yellow,
                        StrokeWidth = 5,
                        StrokeCap = SKStrokeCap.Round
                    })
                    {
                        using (SKPath path = new SKPath())
                        {
                            this.CreatePath(
                                path,
                                IMAGE_WIDTH,
                                IMAGE_HEIGHT,
                                this.loadedNutritionData.Select(i => i.Carbohydrates), i => (int)i.GetValueOrDefault(),
                                (int)this.loadedNutritionData.Select(i => i.Carbohydrates).Max().GetValueOrDefault());
                            canvas.DrawPath(path, paint);
                        }
                    }

                    using (SKPaint paint = new SKPaint()
                    {
                        Style = SKPaintStyle.Stroke,
                        Color = SKColors.Violet,
                        StrokeWidth = 5,
                        StrokeCap = SKStrokeCap.Round
                    })
                    {
                        using (SKPath path = new SKPath())
                        {
                            this.CreatePath(
                                path,
                                IMAGE_WIDTH,
                                IMAGE_HEIGHT,
                                this.loadedNutritionData.Select(i => i.AmountOfWater), i => (int)i.GetValueOrDefault(),
                                (int)this.loadedNutritionData.Select(i => i.AmountOfWater).Max().GetValueOrDefault());
                            canvas.DrawPath(path, paint);
                        }
                    }
                }

                return bitmap.Encode(SKEncodedImageFormat.Png, 1).AsStream();
            }
        }

        private void CreatePath<T>(SKPath path, int width, int height, IEnumerable<T> values, Func<T, int> selector, int max)
        {
            if (values == null && !values.Any())
            {
                return;
            }

            for (int i = 0; i < values.Count(); i++)
            {
                float x = (float)i * (float)width / (float)values.Count();
                float y = (float)selector?.Invoke(values.ElementAt(i)) / (float)max * (float)height;

                if (!path.LastPoint.IsEmpty)
                {
                    path.LineTo(new SKPoint(x, height - y));
                }
                else
                {
                    path.MoveTo(new SKPoint(x, height - y));
                }
            }
        }

        private void ClearGraphics()
        {
            this.BodyImageSource = null;
            this.NutritionImageSource = null;
        }

        private void MoveToSportsmenListPage()
        {
            if (!this.PageLoaded)
            {
                return;
            }

            this._navigationService.MoveToPage(Services.Navigation.Pages.SportsmenList);
        }

        private void MoveToLoginPage()
        {
            if (!this.PageLoaded)
            {
                return;
            }

            this._navigationService.MoveToPage(Services.Navigation.Pages.Login);
        }

        private void OnAuthorizationStatusChanged(AuthorizationStatuses status)
        {
            switch (status)
            {
                case AuthorizationStatuses.Authorized:
                    this.authorizationCompleted = true;
                    break;
                case AuthorizationStatuses.Unauthorized:
                case AuthorizationStatuses.Undefined:
                default:
                    this.MoveToLoginPage();
                    break;
            }
        }
    }
}
