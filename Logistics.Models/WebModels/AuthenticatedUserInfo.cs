using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Logistics.Models.WebModels
{
    public class AuthenticatedUserInfo
    {
        [JsonPropertyName("token")]
        public string Token { get; set; }
        [JsonPropertyName("roles")]
        public List<string> Roles { get; set; }
    }
}