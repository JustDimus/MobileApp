using System;
using System.Collections.Generic;
using System.Text;

namespace MobileApp.Library.DataManagement.Request
{
    public abstract class BaseAuthorizationRequest : BaseJsonRelativeRequest
    {
        public BaseAuthorizationRequest(string token)
        {
            this.AddHeader("Authorization", token);
        }
    }
}
