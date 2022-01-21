using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace WannaWhat.DTOs
{
    public class UserRegistrationResponse
    {
        public bool IsValid { get; set; }
        public String Type { get; set; }
        public String Title { get; set; }
        public int Status { get; set; }
        public String TraceId { get; set; }
        public AuthErrors errors { get; set; }

        public UserRegistrationResponse()
        {
            this.errors = new AuthErrors();
        }
    }

    public class AuthErrors
    {
        public AuthErrors()
        {
            this.PersonalInfoName = new List<string>();
            this.PersonalInfoSurname = new List<string>();
        }

        [JsonProperty("PersonalInfo.Name")]
        public List<string> PersonalInfoName { get; set; }

        [JsonProperty("PersonalInfo.Surname")]
        public List<string> PersonalInfoSurname { get; set; }
    }
}
