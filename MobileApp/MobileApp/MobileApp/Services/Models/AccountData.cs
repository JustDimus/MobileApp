using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace MobileApp.Services.Models
{
    public class AccountData
    {
        [JsonProperty("Id")]
        public string Id { get; set; }
        [JsonProperty("Fio")]
        public string Fio { get; set; }
        [JsonProperty("Phone")]
        public string Phone { get; set; }
        [JsonProperty("Email")]
        public string Email { get; set; }
        [JsonProperty("BirthdayDate")]
        public DateTime? BirthdayDate { get; set; }
        [JsonProperty("Sex")]
        public string Sex { get; set; }
        [JsonProperty("Login")]
        public string Login { get; set; }
        [JsonProperty("Password")]
        public string Password { get; set; }
    }
}
