using MobileApp.Services.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MobileApp.Services.Account
{
    public interface IAccountService
    {
        IObservable<List<UserRoles>> AuthorizedUserRolesObservable { get; }

        Task<DataRequest<List<UserRoles>>> GetUserRoleAsync();

        Task<DataRequest<AccountData>> GetAccountDataAsync();
    }
}
