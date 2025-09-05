using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Driver;
using TicketService.Src.Data;
using TicketService.Src.DTOs;
using TicketService.Src.interfaces;
using TicketService.Src.Mappers;
using TicketService.Src.Models;

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
                .Find(t => t.PassengerId == newTicket.PassengerId && t.IsDeleted == false)
                .FirstOrDefaultAsync();

            if (existingTicket != null)
            {
                throw new InvalidOperationException("Passenger already has a ticket for this date.");
            }

            await _context.Tickets.InsertOneAsync(newTicket);
            return newTicket.ToDto();
        }

        public async Task<TicketDtoById?> GetTicketById(string id)
        {
            var ticket = await _context.Tickets
                .Find(t => t.Id == id && t.IsDeleted == false)
                .FirstOrDefaultAsync();

            return ticket?.toDtoById();
        }

        public async Task<ICollection<TicketDto?>> GetTickets() 
        {
            var tickets = await _context.Tickets
                .Find(t => t.IsDeleted == false)
                .ToListAsync();

            return tickets.Select(t => t.ToDto()).ToList();
        }

        public async Task<TicketDto?> UpdateTicket(string id, UpdateTicketDto ticket)
        {
            var existingTicket = await _context.Tickets
                .Find(t => t.Id == id && t.IsDeleted == false)
                .FirstOrDefaultAsync();

            if (existingTicket == null)
            {
                throw new KeyNotFoundException("Ticket not found.");
            }
            
            if (!string.IsNullOrWhiteSpace(ticket.TicketType))
            {
                existingTicket.TicketType = ticket.TicketType;
            }

            if (!string.IsNullOrWhiteSpace(ticket.TicketStatus))
            {
                existingTicket.TicketStatus = ticket.TicketStatus;
            }

            if (ticket.Amount.HasValue)
            {
                existingTicket.Amount = ticket.Amount.Value;
            }
            
            await _context.Tickets.ReplaceOneAsync(t => t.Id == id, existingTicket);
            return existingTicket.ToDto();
        }
    }
}