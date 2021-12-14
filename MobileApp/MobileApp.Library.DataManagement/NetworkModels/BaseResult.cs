using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace MobileApp.Library.DataManagement.NetworkModels
{
    internal class BaseResult<TEntity>
    {
        [JsonProperty("Data")]
        public TEntity Result { get; set; }
        [JsonProperty("Completed")]
        public bool IsSuccessful { get; set; }
        [JsonProperty("Message")]
        public string ResponseMessage { get; set; }
    }
}
