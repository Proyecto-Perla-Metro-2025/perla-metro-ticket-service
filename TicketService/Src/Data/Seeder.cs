using MongoDB.Driver;
using TicketService.Src.Models;

namespace TicketService.Src.Data
{
    /// <summary>
    /// Clase para inicializar datos de ejemplo en la base de datos.
    /// </summary>
    public class Seeder
    {
        /// <summary>
        /// Método para sembrar datos de ejemplo en la base de datos si la colección está vacía.
        /// </summary>
        /// <param name="context">Parámetros de contexto de la base de datos.</param>
        public static void Seed(MongoDataContext context)
        {
            var collection = context.Tickets;

            if (!collection.Find(FilterDefinition<Ticket>.Empty).Any())
            {
                for (int i = 1; i <= 10; i++)
                {
                    collection.InsertOne(new Ticket
                    {
                        Id = Guid.NewGuid().ToString(),
                        PassengerId = $"passenger-{i}",
                        CreatedAt = TimeZoneInfo.ConvertTime(DateTimeOffset.UtcNow, TimeZoneInfo.FindSystemTimeZoneById("Pacific SA Standard Time")),
                        TicketType = Random.Shared.Next(0, 2) == 0 ? "ida" : "vuelta",
                        TicketStatus = Random.Shared.Next(0, 3) == 0 ? "activo" : (Random.Shared.Next(0, 2) == 0 ? "usado" : "caducado"),
                        Amount = 2.50,
                        DeletedAt = null
                    });
                }
            }
        }
    }
}