using Microsoft.AspNetCore.Mvc;
using TicketService.Src.interfaces;

namespace TicketService.Src.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TicketController : ControllerBase
    {
        private readonly ITicketRepository _ticketRepository;

        public TicketController(ITicketRepository ticketRepository)
        {
            _ticketRepository = ticketRepository;
        }

        [HttpGet]
        public IActionResult GetTickets()
        {
            try
            {
                var tickets = _ticketRepository.GetTickets().Result;
                return Ok(tickets);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }

        [HttpGet("{id:string}")]
        public IActionResult GetTicketById(string id)
        {
            try
            {
                var ticket = _ticketRepository.GetTicketById(id).Result;
                return Ok(ticket);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }
    }
}