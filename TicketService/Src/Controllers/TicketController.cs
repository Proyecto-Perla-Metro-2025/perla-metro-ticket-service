using Microsoft.AspNetCore.Mvc;
using TicketService.Src.DTOs;
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
        public async Task<IActionResult> GetTickets()
        {
            try
            {
                var tickets = await _ticketRepository.GetTickets();
                return Ok(tickets);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetTicketById(string id)
        {
            try
            {
                var ticket = await _ticketRepository.GetTicketById(id);
                return Ok(ticket);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateTicket([FromBody] CreateTicketDto createTicketDto)
        {
            try
            {
                var ticket = await _ticketRepository.CreateTicket(createTicketDto);
                return Ok(ticket);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }
    }
}