using MobileApp.Services.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MobileApp.Services.Sportsmen
{
    internal class StubSportsmenService : ISportsmenService
    {
        public Task<DataRequest<List<SportsmentData>>> GetSportsmenList()
        {
            return Task.FromResult(new DataRequest<List<SportsmentData>>()
            {
                IsSuccessful = true,
                Result = new List<SportsmentData>()
                {
                    new SportsmentData()
                    {

                    },
                    new SportsmentData()
                    {

                    }
                }
            });
        }
    }
}
