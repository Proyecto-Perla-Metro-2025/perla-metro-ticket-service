using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TicketService.Src.Responses
{
    /// <summary>
    /// Clase que representa una respuesta de API genérica.
    /// </summary>
    /// <typeparam name="T">Tipo de datos de la respuesta.</typeparam>
    /// <param name="data">Parámetro que representa los datos de la respuesta.</param>
    /// <param name="message">Parámetro que representa un mensaje de la respuesta.</param>
    /// <param name="success">Parámetro que indica si la operación fue exitosa.</param>
    public class ApiResponse<T>(T data, string message, bool success)
    {
        // Datos de la respuesta.
        public T Data { get; set; } = data;
        // Mensaje de la respuesta.
        public string Message { get; set; } = message;
        // Indica si la operación fue exitosa.
        public bool Success { get; set; } = success;
    }
}