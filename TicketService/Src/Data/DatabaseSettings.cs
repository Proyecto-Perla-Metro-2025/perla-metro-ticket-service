namespace TicketService.Src.Data
{
    public class DatabaseSettings
    {
        public string ConnectionString { get; set; } = string.Empty;
        public string DatabaseName { get; set; } = string.Empty;
        public string TicketsCollectionName { get; set; } = string.Empty;
    }
}