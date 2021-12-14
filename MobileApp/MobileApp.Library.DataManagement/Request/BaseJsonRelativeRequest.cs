using MobileApp.Library.Network.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace MobileApp.Library.DataManagement.Request
{
    public abstract class BaseJsonRelativeRequest : BaseApplicationRequest
    {
        public override string MediaType => @"application/json";

        public override bool IsUrlRelative => true;
    }
}
