using MobileApp.Services.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MobileApp.Services.Sportsmen
{
    internal class StubSportsmenService : ISportsmenService
    {
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
    }
}
