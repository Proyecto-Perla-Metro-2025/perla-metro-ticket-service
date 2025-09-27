using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TicketService.Src.DTOs;
using TicketService.Src.Models;

namespace TicketService.Src.Mappers
{
    /// <summary>
    /// Clase estática que proporciona métodos de extensión para mapear entre modelos y DTOs de tickets.
    /// </summary>
    public static class TicketMapper
    {
        /// <summary>
        /// Mapea un modelo de Ticket a un DTO de Ticket.
        /// </summary>
        /// <param name="ticket">El modelo de Ticket.</param>
        /// <returns>El DTO de Ticket.</returns>
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

        /// <summary>
        /// Mapea un modelo de Ticket a un DTO de Ticket por ID.
        /// </summary>
        /// <param name="ticket">El modelo de Ticket.</param>
        /// <returns>El DTO de Ticket por ID.</returns>
        public static TicketDtoById? ToDtoById(this Ticket? ticket)
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

        /// <summary>
        /// Mapea un DTO de creación de Ticket a un modelo de Ticket.
        /// </summary>
        /// <param name="createTicketDto">El DTO de creación de Ticket.</param>
        /// <returns>El modelo de Ticket.</returns>
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

        /// <summary>
        /// Actualiza un modelo de Ticket existente con los valores de un DTO de actualización de Ticket.
        /// </summary>
        /// <param name="existingTicket">El modelo de Ticket existente.</param>
        /// <param name="updateDto">El DTO de actualización de Ticket.</param>
        public static void UpdateFromDto(this Ticket existingTicket, UpdateTicketDto updateDto)
        {
            if (updateDto == null) return;

            if (!string.IsNullOrWhiteSpace(updateDto.TicketType))
            {
                existingTicket.TicketType = updateDto.TicketType.NormalizeTicketValue();
            }

            if (!string.IsNullOrWhiteSpace(updateDto.PassengerId))
            {
                existingTicket.PassengerId = updateDto.PassengerId.TrimOnly();
            }

            if (updateDto.Amount.HasValue)
            {
                existingTicket.Amount = updateDto.Amount.Value;
            }

        }

        /// <summary>
        /// Normaliza un valor de ticket a minúsculas y sin espacios en blanco al inicio o al final.
        /// </summary>
        /// <param name="value">El valor de ticket a normalizar.</param>
        /// <returns>El valor de ticket normalizado.</returns>
        public static string NormalizeTicketValue(this string value)
        {
            return string.IsNullOrWhiteSpace(value) ? value : value.Trim().ToLowerInvariant();
        }

        /// <summary>
        /// Elimina los espacios en blanco al inicio y al final de una cadena.
        /// </summary>
        /// <param name="value">La cadena de texto a procesar.</param>
        /// <returns>La cadena de texto sin espacios en blanco al inicio y al final.</returns>
        public static string TrimOnly(this string value)
        {
            return string.IsNullOrWhiteSpace(value) ? value : value.Trim();
        }
    }
}