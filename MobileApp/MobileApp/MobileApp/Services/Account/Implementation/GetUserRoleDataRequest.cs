using MobileApp.Library.DataManagement.Request;
using MobileApp.Library.Network.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace MobileApp.Services.Account.Implementation
{
    public class GetUserRoleDataRequest : BaseAuthorizationRequest
    {
        public GetUserRoleDataRequest(string userId, string token)
            : base(token)
        {

        }

        public override string Address => @"User/Roles";

        public override RequestMethods Method => RequestMethods.GET;
    }
}
