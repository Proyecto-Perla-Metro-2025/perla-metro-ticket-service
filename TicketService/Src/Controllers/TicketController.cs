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
            catch (Exception)
            {
                return StatusCode(500, new { message = "An error occurred while retrieving tickets." });
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetTicketById(string id)
        {
            if (id == null) return BadRequest(new { message = "Ticket ID is required." });

            try
            {
                var ticket = await _ticketRepository.GetTicketById(id);

                if (ticket == null)
                {
                    return NotFound(new { message = "Ticket not found." });
                }

                return Ok(ticket);
            }
            catch (Exception)
            {
                return StatusCode(500, new { message = "An error occurred while retrieving the ticket." });
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateTicket([FromBody] CreateTicketDto createTicketDto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            try
            {
                var ticket = await _ticketRepository.CreateTicket(createTicketDto);

                if (ticket == null)
                {
                    return BadRequest(new { message = "Failed to create ticket." });
                }

                return Ok(ticket);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
            catch (Exception)
            {
                return StatusCode(500, new { message = "An error occurred while creating the ticket." });
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateTicket(string id, [FromBody] UpdateTicketDto updateTicketDto)
        {
            if (id == null) return BadRequest(new { message = "Ticket ID is required." });

            if (!ModelState.IsValid) return BadRequest(ModelState);

            try
            {
                var updatedTicket = await _ticketRepository.UpdateTicket(id, updateTicketDto);

                if (updatedTicket == null)
                {
                    return NotFound(new { message = "Ticket not found." });
                }

                return Ok(updatedTicket);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { message = ex.Message });
            }
            catch (Exception)
            {
                return StatusCode(500, new { message = "An error occurred while updating the ticket." });
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTicket(string id)
        {
            if (id == null) return BadRequest(new { message = "Ticket ID is required." });

            try
            {
                var result = await _ticketRepository.DeleteTicket(id);

                if (!result)
                {
                    return NotFound(new { message = "Ticket not found." });
                }

                return Ok(new { message = "Ticket deleted successfully." });
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { message = ex.Message });
            }
            catch (Exception)
            {
                return StatusCode(500, new { message = "An error occurred while deleting the ticket." });
            }
        }
    }
}