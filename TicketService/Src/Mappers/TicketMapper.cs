using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TicketService.Src.DTOs;
using TicketService.Src.Models;

namespace TicketService.Src.Mappers
{
    public static class TicketMapper
    {
        public static TicketDto? ToDto(this Ticket? ticket)
        {
            if (ticket == null) return null;

            return new TicketDto
            {
                Id = ticket.Id,
                PassengerId = ticket.PassengerId,
                CreatedAt = ticket.CreatedAt,
                TicketType = ticket.TicketType,
                TicketStatus = ticket.TicketStatus,
                Amount = ticket.Amount
            };
        }

        public static TicketDtoById? toDtoById(this Ticket? ticket)
        {
            if (ticket == null) return null;

            return new TicketDtoById
            {
                Id = ticket.Id,
                PassengerId = ticket.PassengerId,
                CreatedAt = ticket.CreatedAt,
                TicketType = ticket.TicketType,
                Amount = ticket.Amount
            };
        }
    }
}