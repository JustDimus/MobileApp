using MobileApp.Library.DataManagement.Request;
using MobileApp.Library.Network.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace MobileApp.Services.Account.Implementation
{
    public class GetUserRoleDataRequest : BaseAuthorizationRequest
    {
        private string userId;

        public GetUserRoleDataRequest(string userId, string token)
            : base(token)
        {
            this.userId = userId;
        }

        public override string Address => $@"User/Roles?userId={this.userId}";

        public override RequestMethods Method => RequestMethods.GET;
    }
}
