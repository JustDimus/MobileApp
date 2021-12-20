using MobileApp.Services.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MobileApp.Services.Sportsmen
{
    internal class StubSportsmenService : ISportsmenService
    {
        public AccountData SelectedSportsmen => throw new NotImplementedException();

        private AccountData savedAccount;

        public bool ClearSelectedSportsmen()
        {
            this.savedAccount = null;
            return true;
        }

        public Task<DataRequest<List<BodyData>>> GetBodyDataListAsync(AccountData account)
        {
            return Task.FromResult(new DataRequest<List<BodyData>>()
            {
                IsSuccessful = true,
                Result = new List<BodyData>()
                {
                    new BodyData()
                    {

                    },
                    new BodyData()
                    {

                    }
                }
            });
        }

        public Task<DataRequest<List<NutritionData>>> GetNutritionDataListAsync(AccountData account)
        {
            return Task.FromResult(new DataRequest<List<NutritionData>>()
            {
                IsSuccessful = true,
                Result = new List<NutritionData>()
                {
                    new NutritionData()
                    {

                    },
                    new NutritionData()
                    {

                    }
                }
            });
        }

        public Task<DataRequest<List<AccountData>>> GetSportsmenListAsync()
        {
            return Task.FromResult(new DataRequest<List<AccountData>>()
            {
                IsSuccessful = true,
                Result = new List<AccountData>()
                {
                    new AccountData()
                    {

                    },
                    new AccountData()
                    {

                    }
                }
            });
        }

        public bool SelectSportsmen(AccountData account)
        {
            this.savedAccount = account;
            return true;
        }
    }
}
