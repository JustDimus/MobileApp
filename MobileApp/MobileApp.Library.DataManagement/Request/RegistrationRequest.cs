using MobileApp.Library.DataManagement.Models;
using MobileApp.Library.Network.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace MobileApp.Library.DataManagement.Request
{
    public class RegistrationRequest : BaseJsonRelativeRequest
    {
        private readonly RegistrationBody _requestBody;

        public RegistrationRequest(RegistrationData registrationData)
        {
            if (registrationData == null)
            {
                throw new ArgumentNullException(nameof(registrationData));
            }

            this._requestBody = new RegistrationBody(registrationData);
        }

        public override RequestMethods Method => RequestMethods.POST;

        public override object BodyEntity => this._requestBody;

        public override string Address => @"create/user";

        private class RegistrationBody
        {
            public RegistrationBody(RegistrationData registrationData)
            {
                this.Email = registrationData.Email;
                this.Sex = registrationData.Sex;
                this.Password = registrationData.Password;
                this.Phone = registrationData.Phone;
                this.Name = registrationData.Name;
            }

            [JsonProperty("email")]
            public string Email { get; set; }
            [JsonProperty("name")]
            public string Name { get; set; }
            [JsonProperty("password")]
            public string Password { get; set; }
            [JsonProperty("phone")]
            public string Phone { get; set; }
            [JsonProperty("sex")]
            public string Sex { get; set; }
        }
    }
}
