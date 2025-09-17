using MongoDB.Driver;
using TicketService.Src.Data;
using TicketService.Src.DTOs;
using TicketService.Src.interfaces;
using TicketService.Src.Mappers;


namespace TicketService.Src.Repositories
{
    public class TicketRepository : ITicketRepository
    {
        private readonly MongoDataContext _context;

        public TicketRepository(MongoDataContext context)
        {
            _context = context;
        }

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

        public async Task<TicketDtoById?> GetTicketById(string id)
        {
            var ticket = await _context.Tickets
                .Find(t => t.Id == id && t.DeletedAt == null)
                .FirstOrDefaultAsync();

            return ticket?.toDtoById();
        }

        public async Task<ICollection<TicketDto?>> GetTickets() 
        {
            var tickets = await _context.Tickets
                .Find(t => t.DeletedAt == null)
                .ToListAsync();

            return tickets.Select(t => t.ToDto()).ToList();
        }

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