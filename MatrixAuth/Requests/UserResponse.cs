using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace MatrixAuth.Requests
{
    public class UserResponse
    {
        [JsonProperty(PropertyName = "access_token", NullValueHandling = NullValueHandling.Ignore)]
        public string? access_token { get; set; }
        [JsonProperty(PropertyName = "home_server", NullValueHandling = NullValueHandling.Ignore)]
        public string home_server { get; set; }
        [JsonProperty(PropertyName = "user_id", NullValueHandling = NullValueHandling.Ignore)]
        public string user_id { get; set; }
        [JsonProperty(PropertyName = "refresh_token", NullValueHandling = NullValueHandling.Ignore)]
        public string? refresh_token { get; set; }
    }
}
