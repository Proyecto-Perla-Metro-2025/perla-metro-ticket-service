using Microsoft.Extensions.Options;
using MongoDB.Driver;
using TicketService.Src.Models;

namespace TicketService.Src.Data
{
    public class MongoDataContext
    {
        private readonly IMongoDatabase _database;

        public MongoDataContext(IOptions<DatabaseSettings> dbSettings)
        {
            var mongoClient = new MongoClient(dbSettings.Value.ConnectionString);
            _database = mongoClient.GetDatabase(dbSettings.Value.DatabaseName);
        }

        public IMongoCollection<Ticket> Tickets => _database.GetCollection<Ticket>("Tickets");
    }
}