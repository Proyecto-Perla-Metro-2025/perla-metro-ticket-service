using DotNetEnv;
using TicketService.Src.Data;
using TicketService.Src.interfaces;
using TicketService.Src.Repositories;

Env.Load();

var builder = WebApplication.CreateBuilder(args);

builder.Services.Configure<DatabaseSettings>(options =>
{
    options.ConnectionString = GetRequiredEnvVar("MONGO_CONNECTION_STRING");
    options.DatabaseName = GetRequiredEnvVar("MONGO_DATABASE_NAME");
    options.TicketsCollectionName = Environment.GetEnvironmentVariable("TICKETS_COLLECTION_NAME") ?? "tickets";
});

builder.Services.AddScoped<MongoDataContext>();
builder.Services.AddScoped<ITicketRepository, TicketRepository>();
builder.Services.AddSwaggerGen();
builder.Services.AddControllers();

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

var app = builder.Build();

static string GetRequiredEnvVar(string name)
{
    return Environment.GetEnvironmentVariable(name) 
           ?? throw new InvalidOperationException($"Environment variable '{name}' is required");
}

using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<MongoDataContext>();
    // Esto ejecutará el constructor y ConfigureIndexes()
    Console.WriteLine("MongoDB Context initialized and indexes configured.");
    Seeder.Seed(context);
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.MapControllers();
app.Run();