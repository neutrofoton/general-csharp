using System;
using Microsoft.Extensions.Options;

namespace Lab.ConfigurationEnv.Settings.Extensions;

public class ConfigurationValidationStartupFilter : IStartupFilter
{
    private readonly ILogger<ConfigurationValidationStartupFilter> logger;

   

    public Action<IApplicationBuilder> Configure(Action<IApplicationBuilder> next)
    {
        return app =>
        {
            // Validate the configuration as part of the startup process
            var serviceProvider = app.ApplicationServices;
            ValidateConfiguration(serviceProvider);

            next(app);
        };
    }

    private void ValidateConfiguration(IServiceProvider serviceProvider)
    {
        // Get the configuration values from DI
        var logger = serviceProvider.GetRequiredService<ILogger<ConfigurationValidationStartupFilter>>();
        var settings = serviceProvider.GetRequiredService<IOptions<MicroserviceSettings>>().Value;

        // Validate the settings
        var validationResults = settings.Validate();
        if (validationResults.Any())
        {
            logger.LogError("Configuration errors:");
            // If validation fails, log the errors or throw an exception
            foreach (var error in validationResults)
            {
                logger.LogError($"- {error}");
            }

            // Optionally, throw an exception to prevent the app from starting
            throw new InvalidOperationException("Configuration validation failed.");
        }
    }
}

