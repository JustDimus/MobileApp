using MobileApp.Library.DataManagement.Models;
using MobileApp.Library.Network.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace MobileApp.Library.DataManagement.Request
{
    internal class LoginRequest : BaseJsonRelativeRequest
    {
        private LoginData _loginData;

        public LoginRequest(LoginData loginData)
        {
            this._loginData = loginData;
        }

        public override RequestMethods Method => RequestMethods.GET;

        public override string Address => $@"userbycreds?login={this._loginData.Login}&password={this._loginData.Password}";
    }
}
