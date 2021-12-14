using System;
using System.Collections.Generic;
using System.Text;

namespace MobileApp.Library.Network.NeworkCommunication.Configuration
{
    public abstract class NetworkConfigurationManager
    {
        public abstract bool UseBaseUrl { get; }

        public virtual string BaseUrl { get; } = null;

        public abstract TimeSpan DefaultRequestCancellationTime { get; }
    }
}
