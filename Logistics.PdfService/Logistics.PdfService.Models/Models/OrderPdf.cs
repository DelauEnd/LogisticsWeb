using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Logistics.PdfService.Models
{
    public class OrderPdf
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public ObjectId Id { get; set; }

        public byte[] PdfFile { get; set; }
    }
}
