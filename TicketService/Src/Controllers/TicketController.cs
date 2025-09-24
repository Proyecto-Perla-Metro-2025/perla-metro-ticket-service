using Microsoft.AspNetCore.Mvc;
using TicketService.Src.DTOs;
using TicketService.Src.interfaces;
using TicketService.Src.Responses;

namespace TicketService.Src.Controllers
{
    /// <summary>
    /// Controlador para la gestión de tickets.
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class TicketController : ControllerBase
    {
        // Repositorio de tickets.
        private readonly ITicketRepository _ticketRepository;

        /// <summary>
        /// Constructor que inicializa el controlador con el repositorio de tickets.
        /// </summary>
        /// <param name="ticketRepository">Repositorio de tickets.</param>
        public TicketController(ITicketRepository ticketRepository)
        {
            _ticketRepository = ticketRepository;
        }

        /// <summary>
        /// Obtiene todos los tickets.
        /// </summary>
        /// <returns>Retorna una lista de tickets.</returns>
        [HttpGet]
        public async Task<IActionResult> GetTickets()
        {
            try
            {
                var tickets = await _ticketRepository.GetTickets();
                var response = new ApiResponse<object>(tickets, "Tickets retrieved successfully", true);
                return Ok(response);
            }
            catch (Exception)
            {
                return StatusCode(500, new { message = "An error occurred while retrieving tickets." });
            }
        }

        /// <summary>
        /// Obtiene un ticket por su ID.
        /// </summary>
        /// <param name="id">ID del ticket.</param>
        /// <returns>Retorna el ticket correspondiente o un mensaje de error.</returns>
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

                var response = new ApiResponse<object>(ticket, "Ticket retrieved successfully", true);

                return Ok(response);
            }
            catch (Exception)
            {
                return StatusCode(500, new { message = "An error occurred while retrieving the ticket." });
            }
        }

        /// <summary>
        /// Crea un nuevo ticket.
        /// </summary>
        /// <param name="createTicketDto">Datos del ticket a crear.</param>
        /// <returns>Retorna el ticket creado o un mensaje de error.</returns>
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

                var response = new ApiResponse<object>(ticket, "Ticket created successfully", true);

                return Ok(response);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        /// <summary>
        /// Actualiza un ticket existente.
        /// </summary>
        /// <param name="id">ID del ticket a actualizar.</param>
        /// <param name="updateTicketDto">Datos del ticket a actualizar.</param>
        /// <returns>Retorna el ticket actualizado o un mensaje de error.</returns>
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

                var response = new ApiResponse<object>(updatedTicket, "Ticket updated successfully", true);

                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        /// <summary>
        /// Elimina un ticket por su ID.
        /// </summary>
        /// <param name="id">ID del ticket a eliminar.</param>
        /// <returns>Retorna un mensaje de éxito o un mensaje de error.</returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTicket(string id)
        {
            if (id == null) return BadRequest(new { message = "Ticket ID is required." });

            try
            {
                var ticket = await _ticketRepository.DeleteTicket(id);

                if (!ticket)
                {
                    return NotFound(new { message = "Ticket not found." });
                }

                var response = new ApiResponse<object?>(null, "Ticket deleted successfully", true);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
    }
}