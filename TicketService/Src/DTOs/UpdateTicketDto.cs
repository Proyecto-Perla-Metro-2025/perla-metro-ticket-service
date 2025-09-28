using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Bson.Serialization.Attributes;
using TicketService.Src.Models;

namespace TicketService.Src.DTOs
{
    public class UpdateTicketDto
    {
        [Required]
        public string? PassengerId { get; set; } = null;
        
        [RegularExpression("^(?i)(ida|vuelta)$", ErrorMessage = "TicketType must be either 'ida' or 'vuelta'.")]
        public string? TicketType { get; set; } = null;

        [Range(0.0, double.MaxValue, ErrorMessage = "Amount must be a non-negative value.")]
        public double? Amount { get; set; } = null;
    }
}