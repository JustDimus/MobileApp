using MobileApp.Library.Network.NeworkCommunication.Configuration;
using System;
using System.Collections.Generic;
using System.Text;

namespace MobileApp.Configurations
{
    internal class MobileAppNetworkConfigurationManager : NetworkConfigurationManager
    {
        public override bool UseBaseUrl => true;

        public override string BaseUrl => @"https://localhost:15623/api/";

        public override TimeSpan DefaultRequestCancellationTime { get; } = new TimeSpan(0, 0, 30);
    }
}
