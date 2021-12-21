using MobileApp.Library.DataManagement.Request;
using MobileApp.Library.Network.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace MobileApp.Services.Account.Implementation
{
    public class GetAccountDataRequest : BaseAuthorizationRequest
    {
        public GetAccountDataRequest(string token)
            : base(token)
        {

        }

        public override string Address => @"ProfileInfo";

        public override RequestMethods Method => RequestMethods.GET;
    }
}
