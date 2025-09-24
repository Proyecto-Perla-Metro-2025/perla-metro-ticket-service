using System.ComponentModel.DataAnnotations;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace TicketService.Src.Models
{
    /// <summary>
    /// Clase que representa un ticket en el sistema.
    /// </summary>
    public class Ticket
    {
        // ID del ticket.
        [BsonId]
        [BsonRepresentation(BsonType.String)]
        [Required]
        public string Id { get; set; } = Guid.NewGuid().ToString();

        // ID del pasajero.
        [BsonElement("PassengerId")]
        [BsonRepresentation(BsonType.String)]
        [Required]
        public string PassengerId { get; set; } = null!;

        // Fecha de creación del ticket.
        [BsonElement("CreatedAt")]
        [Required]
        public DateTimeOffset CreatedAt { get; set; } = TimeZoneInfo
            .ConvertTime(DateTimeOffset.UtcNow, TimeZoneInfo.FindSystemTimeZoneById("Pacific SA Standard Time"));

        // Tipo de ticket.
        [BsonElement("TicketType")]
        [Required]
        [RegularExpression("^(ida|Ida|vuelta|Vuelta)$", ErrorMessage = "TicketType must be either 'ida' or 'vuelta'.")]
        public string TicketType { get; set; } = null!;

        // Estado del ticket.
        [BsonElement("TicketStatus")]
        [Required]
        [RegularExpression("^(activo|Activo|usado|Usado|caducado|Caducado)$", ErrorMessage = "TicketStatus must be either 'activo', 'usado', or 'caducado'.")]
        public string TicketStatus { get; set; } = null!;

        // Monto del ticket.
        [BsonElement("Amount")]
        [Required]
        [Range(0.0, double.MaxValue, ErrorMessage = "Amount must be a non-negative value.")]
        public double Amount { get; set; } = 0.0;

        // Fecha de eliminación del ticket (soft delete).
        [BsonElement("DeletedAt")]
        public DateTime? DeletedAt { get; set; } = null;

        // Propiedades calculadas para verificar el estado del ticket.
        [BsonIgnore]
        public bool IsDeleted => DeletedAt.HasValue;

        // Propiedad que indica si el ticket está activo (no eliminado).
        [BsonIgnore]
        public bool IsActive => !IsDeleted;

        // Método para realizar un soft delete en el ticket.
        public void SoftDelete() => DeletedAt = TimeZoneInfo
            .ConvertTime(DateTimeOffset.UtcNow, TimeZoneInfo.FindSystemTimeZoneById("Pacific SA Standard Time")).DateTime;

    }
}