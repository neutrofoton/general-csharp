using System.ComponentModel.DataAnnotations;
using Lab.ConfigurationEnv.Settings;
using Lab.ConfigurationEnv.Settings.Validations;
using Microsoft.Extensions.Options;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


// Bind configuration
var settings = new MicroserviceSettings();
builder.Configuration.GetSection("Microservice").Bind(settings);

// Validate configuration
var validationResults = settings.Validate();

if (validationResults.Count > 0)
{
    Console.WriteLine("Configuration validation failed:");
    foreach (var result in validationResults)
    {
        Console.WriteLine($" - {result.ErrorMessage}");
    }

    throw new ValidationException("Invalid configuration");
}

builder.Services.AddScoped<MyService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();



app.MapGet("/get-config", (MyService service) =>
{
    service.DoLog();
    return service.Settings;
})
.WithName("GetConfig")
.WithOpenApi();

app.Run();

public class MyService
{
    private readonly MicroserviceSettings _settings;
    private readonly ILogger<MyService> logger;

    public MyService(ILogger<MyService> logger, IOptions<MicroserviceSettings> options)
    {
        _settings = options.Value;
        this.logger = logger;
    }

    public MicroserviceSettings Settings => _settings;

    public void DoLog()
    {
        logger.LogInformation($"Database Connection: {Settings.Infrastructure.Database.ConnectionStrings}");
        logger.LogInformation($"Redis Host: {Settings.Infrastructure.Caching.Redis.Host}");
        logger.LogInformation($"RabbitMQ Host: {Settings.Infrastructure.Messaging.RabbitMQ.Host}");
    }
}