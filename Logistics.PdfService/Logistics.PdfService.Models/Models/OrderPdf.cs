using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;

namespace Logistics.PDFService.Models
{
    public class OrderPdf
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [BsonDateTimeOptions(Kind = DateTimeKind.Local)]
        public DateTime CreationDate { get; set; }

        public byte[] PdfFile { get; set; }
    }
}
