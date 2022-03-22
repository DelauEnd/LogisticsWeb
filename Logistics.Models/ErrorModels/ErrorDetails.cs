using Newtonsoft.Json;

namespace Logistics.Models.ErrorModels
{
    public class ErrorDetails
    {
        public string Message { get; set; }
        public string Exception { get; set; }
        public string StackTrace { get; set; }

        public override string ToString()
            => JsonConvert.SerializeObject(this);
    }
}
