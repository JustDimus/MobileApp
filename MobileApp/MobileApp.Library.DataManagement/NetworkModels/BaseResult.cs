using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace MobileApp.Library.DataManagement.NetworkModels
{
    internal class BaseResult<TEntity>
    {
        [JsonProperty("data")]
        public TEntity Result { get; set; }
        [JsonProperty("completed")]
        public bool IsSuccessful { get; set; }
        [JsonProperty("message")]
        public string ResponseMessage { get; set; }
    }
}
