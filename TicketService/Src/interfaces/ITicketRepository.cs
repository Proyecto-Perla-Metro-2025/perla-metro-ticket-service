using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TicketService.Src.DTOs;
using TicketService.Src.Models;

namespace TicketService.Src.interfaces
{
    /// <summary>
    /// Interfaz que define las operaciones CRUD para los tickets.
    /// </summary>
    public interface ITicketRepository
    {
        /// <summary>
        /// Obtiene todos los tickets.
        /// </summary>
        /// <returns>Una colección de DTOs de tickets.</returns>
        public Task<ICollection<TicketDto?>> GetTickets();

        /// <summary>
        /// Obtiene un ticket por su ID.
        /// </summary>
        /// <param name="id">El ID del ticket.</param>
        /// <returns>El DTO de ticket correspondiente al ID.</returns>
        public Task<TicketDtoById?> GetTicketById(string id);

        /// <summary>
        /// Crea un nuevo ticket.
        /// </summary>
        /// <param name="ticket">El DTO de creación de ticket.</param>
        /// <returns>El DTO de ticket creado.</returns>
        public Task<TicketDto?> CreateTicket(CreateTicketDto ticket);

        /// <summary>
        /// Actualiza un ticket existente.
        /// </summary>
        /// <param name="id">El ID del ticket.</param>
        /// <param name="ticket">El DTO de actualización de ticket.</param>
        /// <returns>El DTO de ticket actualizado.</returns>
        public Task<TicketDto?> UpdateTicket(string id, UpdateTicketDto ticket);

        /// <summary>
        /// Elimina (soft delete) un ticket por su ID.
        /// </summary>
        /// <param name="id">El ID del ticket.</param>
        /// <returns>True si se eliminó el ticket, de lo contrario false.</returns>
        public Task<bool> DeleteTicket(string id);
    }
}