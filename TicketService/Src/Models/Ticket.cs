using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace TicketService.Src.Models
{
    public class Ticket
    {
        [BsonId]
        [BsonRepresentation(BsonType.String)]
        public string Id { get; set; } = Guid.NewGuid().ToString();

        [BsonElement("PassengerId")]
        [BsonRepresentation(BsonType.String)]
        public string PassengerId { get; set; } = null!;

        [BsonElement("CreatedAt")]
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        [BsonElement("TicketType")]
        public string TicketType { get; set; } = null!;

        [BsonElement("TicketStatus")]
        public string TicketStatus { get; set; } = null!;

        [BsonElement("Amount")]
        public double Amount { get; set; } = 0.0;

        [BsonElement("IsDeleted")]
        public bool IsDeleted { get; set; } = false;
    }
}