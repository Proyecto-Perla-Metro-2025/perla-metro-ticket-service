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
            try
            {
                var newTicket = ticket.toModel();

                if (_context.Tickets.FindAsync(t => t.PassengerId == newTicket.PassengerId && t.CreatedAt == newTicket.CreatedAt).Result.Any())
                {
                    throw new Exception("Ticket already exists");
                }

                await _context.Tickets.InsertOneAsync(newTicket);
                return newTicket.ToDto();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public Task<TicketDtoById?> GetTicketById(string id)
        {
            try
            {
                var ticket = _context.Tickets.FindAsync(t => t.Id == id).Result.FirstOrDefault();

                if (ticket == null)
                {
                    throw new Exception("Ticket not found");
                }

                return Task.FromResult(ticket.toDtoById());
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<ICollection<TicketDto?>> GetTickets()
        {
            try
            {
                var tickets = _context.Tickets.FindAsync(ticket => true).Result.ToList();
                
                if (tickets == null || tickets.Count == 0)
                {
                    throw new Exception("No tickets found");
                }

                return await Task.FromResult(tickets.Select(t => t.ToDto()).ToList());
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}