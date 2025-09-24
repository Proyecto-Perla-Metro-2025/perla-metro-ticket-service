using MongoDB.Driver;
using TicketService.Src.Data;
using TicketService.Src.DTOs;
using TicketService.Src.interfaces;
using TicketService.Src.Mappers;

namespace TicketService.Src.Repositories
{
    /// <summary>
    /// Clase que implementa la interfaz ITicketRepository para manejar operaciones CRUD en tickets.
    /// </summary>
    public class TicketRepository : ITicketRepository
    {
        // Contexto de la base de datos MongoDB.
        private readonly MongoDataContext _context;

        /// <summary>
        /// Constructor de la clase TicketRepository.
        /// </summary>
        /// <param name="context">Parametro de contexto de la base de datos.</param>
        public TicketRepository(MongoDataContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Este método permite crear un nuevo ticket en la base de datos.
        /// </summary>
        /// <param name="ticket">Parametro que representa el ticket a crear.</param>
        /// <returns>Retorna el ticket creado.</returns>
        /// <exception cref="InvalidOperationException">Excepcion lanzada cuando el pasajero ya tiene un ticket para la fecha actual.</exception>
        public async Task<TicketDto?> CreateTicket(CreateTicketDto ticket)
        {
            var newTicket = ticket.toModel();

            var existingTicket = await _context.Tickets
                .Find(t => t.PassengerId == newTicket.PassengerId && t.DeletedAt == null)
                .FirstOrDefaultAsync();

            if (newTicket.CreatedAt.Date == TimeZoneInfo.ConvertTime(DateTimeOffset
                                                .UtcNow, TimeZoneInfo
                                                .FindSystemTimeZoneById("Pacific SA Standard Time"))
                                                .Date && existingTicket != null)
            {
                throw new InvalidOperationException("Passenger already has a ticket for this date.");
            }

            await _context.Tickets.InsertOneAsync(newTicket);
            return newTicket.ToDto();
        }

        /// <summary>
        /// Este método permite eliminar un ticket de la base de datos.
        /// </summary>
        /// <param name="id">ID del ticket a eliminar.</param>
        /// <returns>Retorna true si el ticket fue eliminado correctamente.</returns>
        /// <exception cref="KeyNotFoundException">Excepcion lanzada cuando el ticket no es encontrado.</exception>
        public async Task<bool> DeleteTicket(string id)
        {
            var existingTicket = await _context.Tickets
                .Find(t => t.Id == id && t.DeletedAt == null)
                .FirstOrDefaultAsync();

            if (existingTicket == null)
            {
                throw new KeyNotFoundException("Ticket not found.");
            }
            else
            {

                existingTicket.SoftDelete();
                await _context.Tickets.ReplaceOneAsync(t => t.Id == id, existingTicket);
                return true;
            }
        }

        /// <summary>
        /// Este método permite obtener un ticket por su ID.
        /// </summary>
        /// <param name="id">ID del ticket a obtener.</param>
        /// <returns>Retorna el ticket encontrado o null si no existe.</returns>
        public async Task<TicketDtoById?> GetTicketById(string id)
        {
            var ticket = await _context.Tickets
                .Find(t => t.Id == id && t.DeletedAt == null)
                .FirstOrDefaultAsync();

            return ticket?.ToDtoById();
        }

        /// <summary>
        /// Este método permite obtener todos los tickets de la base de datos que no han sido eliminados (desactivados).
        /// </summary>
        /// <returns>Retorna una colección de tickets.</returns>
        public async Task<ICollection<TicketDto?>> GetTickets()
        {
            var tickets = await _context.Tickets
                .Find(t => t.DeletedAt == null)
                .ToListAsync();

            return tickets.Select(t => t.ToDto()).ToList();
        }

        /// <summary>
        /// Este método permite actualizar un ticket existente en la base de datos.
        /// </summary>
        /// <param name="id">ID del ticket a actualizar.</param>
        /// <param name="ticket">Datos del ticket a actualizar.</param>
        /// <returns>Retorna el ticket actualizado.</returns>
        /// <exception cref="KeyNotFoundException">Excepcion lanzada cuando el ticket no es encontrado.</exception>
        /// <exception cref="InvalidOperationException">Excepcion lanzada cuando el ticket no puede ser actualizado.</exception>
        public async Task<TicketDto?> UpdateTicket(string id, UpdateTicketDto ticket)
        {
            var existingTicket = await _context.Tickets
                .Find(t => t.Id == id && t.DeletedAt == null)
                .FirstOrDefaultAsync();

            if (existingTicket == null)
            {
                throw new KeyNotFoundException("Ticket not found.");
            }

            if (!string.IsNullOrWhiteSpace(ticket.TicketStatus))
            {
                var normalizedStatus = ticket.TicketStatus.NormalizeTicketValue();
                if (existingTicket.TicketStatus == "caducado" && normalizedStatus == "activo")
                {
                    throw new InvalidOperationException("Cannot reactivate an expired ticket.");
                }
            }

            existingTicket.UpdateFromDto(ticket);

            await _context.Tickets.ReplaceOneAsync(t => t.Id == id, existingTicket);
            return existingTicket.ToDto();
        }
    }
}