using Newtonsoft.Json;

namespace Logistics.Models.WebModels
{
    public class UserRole
    {
        [JsonProperty("login")]
        public string UserName { get; set; }

        [JsonProperty("role")]
        public string Role { get; set; }
    }
}
