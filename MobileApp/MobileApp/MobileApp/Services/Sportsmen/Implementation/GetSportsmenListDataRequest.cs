using MobileApp.Library.DataManagement.Request;
using MobileApp.Library.Network.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace MobileApp.Services.Sportsmen.Implementation
{
    internal class GetSportsmenListDataRequest : BaseAuthorizationRequest
    {
        private string relativeId;

        public GetSportsmenListDataRequest(string relativeId, string token)
            : base(token)
        {
            this.relativeId = relativeId;
        }

        public override string Address => $@"AthletByRelative?relativeId={this.relativeId}";

        public override RequestMethods Method => RequestMethods.GET;
    }
}
