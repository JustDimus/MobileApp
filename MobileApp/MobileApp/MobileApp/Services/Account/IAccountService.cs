using MobileApp.Services.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MobileApp.Services.Account
{
    public interface IAccountService
    {
        Task<DataRequest<List<UserRoles>>> GetUserRoleAsync();

        Task<DataRequest<AccountData>> GetAccountDataAsync();
    }
}
