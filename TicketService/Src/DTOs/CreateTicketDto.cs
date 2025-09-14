using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace TicketService.Src.DTOs
{
    public class CreateTicketDto
    {
        [BsonElement("PassengerId")]
        [BsonRepresentation(BsonType.String)]
        [Required]
        public string PassengerId { get; set; } = null!;

        [BsonElement("TicketType")]
        [Required]
        [RegularExpression("^(?i)(ida|vuelta)$", ErrorMessage = "TicketType must be either 'ida' or 'vuelta'.")]
        public string TicketType { get; set; } = null!;

        [BsonElement("TicketStatus")]
        [Required]
        [RegularExpression("^(?i)(activo|usado|caducado)$", ErrorMessage = "TicketStatus must be either 'activo', 'usado', or 'caducado'.")]
        public string TicketStatus { get; set; } = null!;

        [BsonElement("Amount")]
        [Required]
        [Range(0.0, double.MaxValue, ErrorMessage = "Amount must be a non-negative value.")]
        public double Amount { get; set; } = 0.0;
    }
}