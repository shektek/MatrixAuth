using Newtonsoft.Json;

namespace MatrixAuth.API.Requests
{
    public class UserLogin
    {
        [JsonProperty(PropertyName = "token", NullValueHandling = NullValueHandling.Ignore)]
        public string? Token { get; set; }

        [JsonProperty(PropertyName = "password", NullValueHandling = NullValueHandling.Ignore)]
        public string? Password { get; set; }

        [JsonProperty(PropertyName = "homeserver", NullValueHandling = NullValueHandling.Ignore)]
        public string Homeserver { get; set; }

        [JsonProperty(PropertyName = "username", NullValueHandling = NullValueHandling.Ignore)]
        public string UserName { get; set; }
    }
}