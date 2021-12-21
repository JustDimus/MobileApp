﻿using MobileApp.Services.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MobileApp.Services.Sportsmen
{
    public interface ISportsmenService
    {
        bool SelectSportsmen(AccountData account);

        bool ClearSelectedSportsmen();

        AccountData SelectedSportsmen { get; }

        Task<DataRequest<List<AccountData>>> GetSportsmenListAsync();

        Task<DataRequest<List<NutritionData>>> GetNutritionDataListAsync(AccountData account);

        Task<DataRequest<List<BodyData>>> GetBodyDataListAsync(AccountData account);
    }
}
