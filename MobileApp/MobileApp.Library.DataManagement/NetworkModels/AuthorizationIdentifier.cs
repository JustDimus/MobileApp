using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace MobileApp.Library.DataManagement.NetworkModels
{
    public class AuthorizationIdentifier
    {
        [JsonProperty("id")]
        public Guid Id { get; set; }

        [JsonProperty("login")]
        public string Login { get; set; }

        [JsonProperty("isAdmin")]
        public bool IsAdmin { get; set; }
    }
}
