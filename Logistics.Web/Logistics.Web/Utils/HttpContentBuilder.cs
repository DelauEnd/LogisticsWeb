using Newtonsoft.Json;
using System.Net.Http;
using System.Text;

namespace Logistics.Web.Utils
{
    public class HttpContentBuilder
    {
        public static StringContent BuildContent(object obj)
            => new StringContent(JsonConvert.SerializeObject(obj), Encoding.UTF8, "application/json");
    }
}
