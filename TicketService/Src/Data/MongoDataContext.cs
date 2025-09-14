using Microsoft.Extensions.Options;
using MongoDB.Driver;
using TicketService.Src.Models;

namespace TicketService.Src.Data
{
    public class MongoDataContext
    {
        private readonly IMongoDatabase _database;
        private readonly DatabaseSettings _dbSettings;

        public MongoDataContext(IOptions<DatabaseSettings> dbSettings)
        {
            _dbSettings = dbSettings.Value;
            
            var mongoClient = new MongoClient(_dbSettings.ConnectionString);
            _database = mongoClient.GetDatabase(_dbSettings.DatabaseName);
        }

        public IMongoCollection<Ticket> Tickets => 
            _database.GetCollection<Ticket>(_dbSettings.TicketsCollectionName);
    }
}