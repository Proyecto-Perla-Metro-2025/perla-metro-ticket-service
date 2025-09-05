using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TicketService.Src.DTOs;
using TicketService.Src.Models;

namespace TicketService.Src.interfaces
{
    public interface ITicketRepository
    {
        public Task<ICollection<TicketDto?>> GetTickets();
        public Task<TicketDtoById?> GetTicketById(string id);
    }
}