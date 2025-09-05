using MongoDB.Driver;
using TicketService.Src.Models;

namespace TicketService.Src.Data
{
    public class Seeder
    {
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
                        CreatedAt = DateTime.UtcNow.AddHours(-4),
                        TicketType = "Single Ride",
                        TicketStatus = "Active",
                        Amount = 2.50,
                        IsDeleted = false
                    });
                }
            }
        }
    }
}