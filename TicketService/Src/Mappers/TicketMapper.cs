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

        public static Ticket toModel(this CreateTicketDto createTicketDto)
        {
            if (createTicketDto == null) return null!;

            return new Ticket
            {
                PassengerId = createTicketDto.PassengerId,
                TicketType = createTicketDto.TicketType.NormalizeTicketValue(),
                TicketStatus = createTicketDto.TicketStatus.NormalizeTicketValue(),
                Amount = createTicketDto.Amount,
            };
        }

        public static void UpdateFromDto(this Ticket existingTicket, UpdateTicketDto updateDto)
        {
            if (updateDto == null) return;

            if (!string.IsNullOrWhiteSpace(updateDto.TicketType))
            {
                existingTicket.TicketType = updateDto.TicketType.NormalizeTicketValue();
            }

            if (!string.IsNullOrWhiteSpace(updateDto.TicketStatus))
            {
                existingTicket.TicketStatus = updateDto.TicketStatus.NormalizeTicketValue();
            }

            if (updateDto.Amount.HasValue)
            {
                existingTicket.Amount = updateDto.Amount.Value;
            }
        }

        public static string NormalizeTicketValue(this string value)
        {
            return string.IsNullOrWhiteSpace(value) ? value : value.Trim().ToLowerInvariant();
        }

        public static string TrimOnly(this string value)
        {
            return string.IsNullOrWhiteSpace(value) ? value : value.Trim();
        }
    }
}