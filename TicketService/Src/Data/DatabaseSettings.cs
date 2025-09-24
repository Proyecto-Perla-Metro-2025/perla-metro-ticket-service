namespace TicketService.Src.Data
{
    /// <summary>
    /// Clase de configuración para la base de datos.
    /// </summary>
    public class DatabaseSettings
    {
        // String de conexión a la base de datos.
        public string ConnectionString { get; set; } = string.Empty;
        
        // Nombre de la base de datos.
        public string DatabaseName { get; set; } = string.Empty;

        // Nombre de la colección de tickets.
        public string TicketsCollectionName { get; set; } = string.Empty;
    }
}