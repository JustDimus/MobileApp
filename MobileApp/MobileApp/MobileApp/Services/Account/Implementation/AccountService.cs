using MobileApp.Library.DataManagement.Authorization;
using MobileApp.Library.DataManagement.RequestManagement;
using MobileApp.Services.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MobileApp.Services.Account.Implementation
{
    public class AccountService : IAccountService
    {
        private IAuthorizationService _authorizationService;
        private IRequestManager _requestManager;

        public AccountService(
            IAuthorizationService authorizationService,
            IRequestManager requestManager)
        {
            this._requestManager = requestManager;
            this._authorizationService = authorizationService;
        }

        public async Task<DataRequest<AccountData>> GetAccountDataAsync()
        {
            var token = await this._authorizationService.UpdateToken();

            var request = new GetAccountDataRequest(token);

            var result = await this._requestManager.SendRequestAsync<AccountData>(request);

            if (result)
            {
                return new DataRequest<AccountData>()
                {
                    IsSuccessful = true,
                    Result = result.Result
                };
            }

            return new DataRequest<AccountData>()
            {
                IsSuccessful = false
            };
        }

        public async Task<DataRequest<List<UserRoles>>> GetUserRoleAsync()
        {
            var accountInfo = await this.GetAccountDataAsync();

            if (accountInfo.IsSuccessful)
            {
                var userToken = this._authorizationService.GetLastUsedToken();
                var request = new GetUserRoleDataRequest(accountInfo.Result.Id, userToken);

                var requestResult = await this._requestManager.SendRequestAsync<List<UserRoles>>(request);

                if (requestResult.IsSuccessful)
                {
                    return new DataRequest<List<UserRoles>>()
                    {
                        IsSuccessful = true,
                        Result = requestResult.Result
                    };
                }
            }

            return new DataRequest<List<UserRoles>>()
            {
                IsSuccessful = false
            };
        }
    }
}
