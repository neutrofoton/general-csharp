using System.ComponentModel.DataAnnotations;
using Lab.ConfigurationEnv;
using Lab.ConfigurationEnv.Settings;
using Lab.ConfigurationEnv.Settings.Extensions;
using Lab.ConfigurationEnv.Settings.Validations;
using Microsoft.Extensions.Options;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


ILogger logger = builder.Services.BuildServiceProvider().GetRequiredService<ILogger<Program>>();

// Bind configuration
logger.LogDebug("Reading configuration");


// 1. Configure MicroserviceSettings from appsettings.json
//builder.Services.Configure<MicroserviceSettings>(builder.Configuration.GetSection("MicroserviceSettings"));
builder.Services
  .AddOptions<MicroserviceSettings>()
  .Bind(builder.Configuration.GetSection("Microservice"))
  .ValidateDataAnnotations();


// 2. Validate the configuration before the app runs using a scoped service method
builder.Services.AddSingleton<IStartupFilter>(new ConfigurationValidationStartupFilter());



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


namespace Lab.ConfigurationEnv 
{
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
            logger.LogDebug($"Database Connection: {Settings.Infrastructure.Database.ConnectionStrings}");
            logger.LogInformation($"Redis Host: {Settings.Infrastructure.Caching.Redis.Host}");
            logger.LogInformation($"RabbitMQ Host: {Settings.Infrastructure.Messaging.RabbitMQ.Host}");
        }
    }
}

