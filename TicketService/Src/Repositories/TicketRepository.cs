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
        public Task<TicketDto> GetTicketById(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<ICollection<TicketDto>> GetTickets()
        {
            try
            {
                var tickets = _context.Tickets.FindAsync(ticket => true).Result.ToList();
                return await Task.FromResult(tickets.Select(t => t.ToDto()).ToList());
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}