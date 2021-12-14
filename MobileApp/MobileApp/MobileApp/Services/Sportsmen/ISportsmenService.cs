using MobileApp.Services.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MobileApp.Services.Sportsmen
{
    internal interface ISportsmenService
    {
        Task<DataRequest<List<SportsmentData>>> GetSportsmenList();
    }
}
