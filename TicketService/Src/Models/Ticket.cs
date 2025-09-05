using System.ComponentModel.DataAnnotations;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace TicketService.Src.Models
{
    public class Ticket
    {
        [BsonId]
        [BsonRepresentation(BsonType.String)]
        [Required]
        public string Id { get; set; } = Guid.NewGuid().ToString();

        [BsonElement("PassengerId")]
        [BsonRepresentation(BsonType.String)]
        [Required]
        public string PassengerId { get; set; } = null!;

        [BsonElement("CreatedAt")]
        [Required]
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow.AddHours(-4);

        [BsonElement("TicketType")]
        [Required]
        [RegularExpression("^(ida|vuelta)$", ErrorMessage = "TicketType must be either 'ida' or 'vuelta'.")]
        public string TicketType { get; set; } = null!;

        [BsonElement("TicketStatus")]
        [Required]
        [RegularExpression("^(activo|usado|caducado)$", ErrorMessage = "TicketStatus must be either 'activo', 'usado', or 'caducado'.")]
        public string TicketStatus { get; set; } = null!;

        [BsonElement("Amount")]
        [Required]
        [Range(0.0, double.MaxValue, ErrorMessage = "Amount must be a non-negative value.")]
        public double Amount { get; set; } = 0.0;

        [BsonElement("IsDeleted")]
        [Required]
        public bool IsDeleted { get; set; } = false;
    }
}