using Logistics.Models.OwnedModels;
using System.Collections.Generic;

namespace Logistics.Models.BrokerModels
{
    public class OrderMessageBase
    {
        public int Id { get; set; }
        public string Status { get; set; }
        public Person Sender { get; set; }
        public string SenderAddress { get; set; }
        public Person Destination { get; set; }
        public string DestinationAddress { get; set; }
        public IEnumerable<CargoForOrderMessage> Cargoes { get; set; }
    }
}
