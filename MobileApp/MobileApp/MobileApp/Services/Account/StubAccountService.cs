using MobileApp.Services.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MobileApp.Services.Account
{
    public class StubAccountService : IAccountService
    {
        public Task<DataRequest<AccountData>> GetAccountDataAsync()
        {
            return Task.FromResult(new DataRequest<AccountData>
            {
                IsSuccessful = true,
                Result = new AccountData()
                {
                    Id = "hello-world-my-systems"
                }
            });
        }

        public Task<DataRequest<List<UserRoles>>> GetUserRoleAsync()
        {
            return Task.FromResult(new DataRequest<List<UserRoles>>
            {
                IsSuccessful = true,
                Result = new List<UserRoles>
                {
                    UserRoles.Athlet
                }
            });
        }
    }
}
