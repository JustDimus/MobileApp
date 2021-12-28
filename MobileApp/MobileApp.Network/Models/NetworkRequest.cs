using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace MobileApp.Library.Network.Models
{
    public abstract class NetworkRequest : INetworkRequest
    {
        private Dictionary<string, string> _headersDictionary = new Dictionary<string, string>();

        public abstract string Address { get; }

        public virtual bool IsUrlRelative { get; } = true;

        public abstract RequestMethods Method { get; }

        public Uri Url => new Uri(this.Address, this.IsUrlRelative ? UriKind.Relative : UriKind.Absolute);

        public IReadOnlyDictionary<string, string> Headers => this._headersDictionary;

        public virtual object BodyEntity { get; }

        public virtual string Body => BodyEntity != null ? JsonConvert.SerializeObject(BodyEntity) : null;

        public virtual string MediaType { get; } = null;

        protected void AddHeader(string key, string value)
        {
            this._headersDictionary.Add(key, value);
        }
    }
}
