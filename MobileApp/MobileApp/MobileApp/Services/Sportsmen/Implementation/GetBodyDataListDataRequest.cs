using MobileApp.Library.DataManagement.Request;
using MobileApp.Library.Network.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace MobileApp.Services.Sportsmen.Implementation
{
    public class GetBodyDataListDataRequest : BaseAuthorizationRequest
    {
        private string athletId;

        public GetBodyDataListDataRequest(string athletId, string token)
            : base(token)
        {
            this.athletId = athletId;
        }

        public override string Address => $@"Athlet/BodyInformation?athletId={this.athletId}";

        public override RequestMethods Method => RequestMethods.GET;
    }
}
