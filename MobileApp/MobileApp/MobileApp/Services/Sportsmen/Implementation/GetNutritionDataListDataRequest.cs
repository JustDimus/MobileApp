using MobileApp.Library.DataManagement.Request;
using MobileApp.Library.Network.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace MobileApp.Services.Sportsmen.Implementation
{
    public class GetNutritionDataListDataRequest : BaseAuthorizationRequest
    {
        private string athletId;

        public GetNutritionDataListDataRequest(string athletId, string token)
            : base(token)
        {
            this.athletId = athletId;
        }

        public override string Address => $@"Athlet/NutrionInformation?athletId={this.athletId}";

        public override RequestMethods Method => RequestMethods.GET;
    }
}
