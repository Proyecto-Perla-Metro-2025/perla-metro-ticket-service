using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TicketService.Src.Models;

namespace TicketService.Src.interfaces
{
    public interface TicketInterface
    {
        public Task<ICollection<Ticket>> GetTickets();
        public Task<Ticket> GetTicketById(int id);
    }
}