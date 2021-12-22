using MobileApp.Library.DataManagement.Authorization;
using MobileApp.Library.DataManagement.RequestManagement;
using MobileApp.Services.Account;
using MobileApp.Services.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MobileApp.Services.Sportsmen.Implementation
{
    public class SportsmenService : ISportsmenService
    {
        private IAuthorizationService _authorizationService;
        private IRequestManager _requestManager;
        private IAccountService _accountService;

        private AccountData selectedAccount;

        public SportsmenService(
            IAccountService accountService,
            IAuthorizationService authorizationService,
            IRequestManager requestManager)
        {
            this._requestManager = requestManager;
            this._authorizationService = authorizationService;
            this._accountService = accountService;
        }

        public AccountData SelectedSportsmen => throw new NotImplementedException();

        public bool ClearSelectedSportsmen()
        {
            if (this.selectedAccount != null)
            {
                this.selectedAccount = null;
                return true;
            }

            return false;
        }

        public async Task<DataRequest<List<BodyData>>> GetBodyDataListAsync(AccountData account)
        {
            var token = await this._authorizationService.UpdateToken();

            var request = new GetBodyDataListDataRequest(account.Id, token);

            var result = await this._requestManager.SendRequestAsync<List<BodyData>>(request);

            if (result.IsSuccessful)
            {
                return new DataRequest<List<BodyData>>()
                {
                    IsSuccessful = true,
                    Result = result.Result,
                };
            }

            return new DataRequest<List<BodyData>>()
            {
                IsSuccessful = false
            };
        }

        public async Task<DataRequest<List<NutritionData>>> GetNutritionDataListAsync(AccountData account)
        {
            var token = await this._authorizationService.UpdateToken();

            var request = new GetNutritionDataListDataRequest(account.Id, token);

            var result = await this._requestManager.SendRequestAsync<List<NutritionData>>(request);

            if (result.IsSuccessful)
            {
                return new DataRequest<List<NutritionData>>()
                {
                    IsSuccessful = true,
                    Result = result.Result,
                };
            }

            return new DataRequest<List<NutritionData>>()
            {
                IsSuccessful = false
            };
        }

        public async Task<DataRequest<List<AccountData>>> GetSportsmenListAsync()
        {
            var token = await this._authorizationService.UpdateToken();

            var account = await this._accountService.GetAccountDataAsync();

            if (account.IsSuccessful)
            {
                var request = new GetNutritionDataListDataRequest(account.Result.Id, token);

                var result = await this._requestManager.SendRequestAsync<List<AccountData>>(request);

                if (result.IsSuccessful)
                {
                    return new DataRequest<List<AccountData>>()
                    {
                        IsSuccessful = true,
                        Result = result.Result,
                    };
                }
            }

            return new DataRequest<List<AccountData>>()
            {
                IsSuccessful = false
            };
        }

        public bool SelectSportsmen(AccountData account)
        {
            if (account != null)
            {
                this.selectedAccount = account;
                return true;
            }

            return false;
        }
    }
}
