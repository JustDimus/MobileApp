using MobileApp.Services.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MobileApp.Services.Sportsmen
{
    internal interface ISportsmenService
    {
        bool SelectSportsmen(AccountData account);

        bool ClearSelectedSportsmen();

        AccountData SelectedSportsmen { get; }

        Task<DataRequest<List<AccountData>>> GetSportsmenListAsync();

        Task<DataRequest<>>
    }
}
