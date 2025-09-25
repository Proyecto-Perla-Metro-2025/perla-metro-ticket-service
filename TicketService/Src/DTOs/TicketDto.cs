using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TicketService.Src.DTOs
{
    public class TicketDto
    {
        public string Id { get; set; } = "";

        public string PassengerId { get; set; } = "";

        public DateTimeOffset CreatedAt { get; set; }

        public string TicketType { get; set; } = "";

        public string TicketStatus { get; set; } = "";

        public double Amount { get; set; }
    }
}