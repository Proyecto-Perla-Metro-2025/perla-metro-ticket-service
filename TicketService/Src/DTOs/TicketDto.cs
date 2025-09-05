using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TicketService.Src.DTOs
{
    public class TicketDto
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();

        public string PassengerId { get; set; } = null!;

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow.AddHours(-4);

        public string TicketType { get; set; } = null!;

        public string TicketStatus { get; set; } = null!;

        public double Amount { get; set; } = 0.0;
    }
}