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
                        CreatedAt = TimeZoneInfo.ConvertTime(DateTimeOffset.UtcNow, TimeZoneInfo.FindSystemTimeZoneById("Pacific SA Standard Time")),
                        TicketType = Random.Shared.Next(0, 2) == 0 ? "ida" : "vuelta",
                        TicketStatus = "Active",
                        Amount = 2.50,
                        IsDeleted = false
                    });
                }
            }
        }
    }
}