using MobileApp.Library.DataManagement.Authorization;
using MobileApp.Library.DataManagement.RequestManagement;
using MobileApp.Services.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MobileApp.Services.Sportsmen.Implementation
{
    public class SportsmenService : ISportsmenService
    {
        private IAuthorizationService _authorizationService;
        private IRequestManager _requestManager;

        public SportsmenService(
            IAuthorizationService authorizationService,
            IRequestManager requestManager)
        {
            this._requestManager = requestManager;
            this._authorizationService = authorizationService;
        }

        public AccountData SelectedSportsmen => throw new NotImplementedException();

        public bool ClearSelectedSportsmen()
        {
            throw new NotImplementedException();
        }

        public Task<DataRequest<List<BodyData>>> GetBodyDataListAsync(AccountData account)
        {
            throw new NotImplementedException();
        }

        public Task<DataRequest<List<NutritionData>>> GetNutritionDataListAsync(AccountData account)
        {
            throw new NotImplementedException();
        }

        public Task<DataRequest<List<AccountData>>> GetSportsmenListAsync()
        {
            throw new NotImplementedException();
        }

        public bool SelectSportsmen(AccountData account)
        {
            throw new NotImplementedException();
        }
    }
}
