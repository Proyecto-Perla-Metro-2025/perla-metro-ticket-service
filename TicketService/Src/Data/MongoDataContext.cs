using Microsoft.Extensions.Options;
using MongoDB.Driver;
using TicketService.Src.Models;

namespace TicketService.Src.Data
{
    /// <summary>
    /// Clase de contexto para la base de datos MongoDB.
    /// </summary>
    public class MongoDataContext
    {
        // Instancia de la base de datos MongoDB.
        private readonly IMongoDatabase _database;

        // Configuración de la base de datos.
        private readonly DatabaseSettings _dbSettings;

        /// <summary>
        /// Constructor que inicializa el contexto de datos con la configuración proporcionada.
        /// </summary>
        /// <param name="dbSettings">Parámetros de configuración de la base de datos.</param>
        public MongoDataContext(IOptions<DatabaseSettings> dbSettings)
        {
            _dbSettings = dbSettings.Value;

            var mongoClient = new MongoClient(_dbSettings.ConnectionString);
            _database = mongoClient.GetDatabase(_dbSettings.DatabaseName);
        }

        /// <summary>
        /// Colección de tickets en la base de datos.
        /// </summary>
        public IMongoCollection<Ticket> Tickets =>
            _database.GetCollection<Ticket>(_dbSettings.TicketsCollectionName);
    }
}