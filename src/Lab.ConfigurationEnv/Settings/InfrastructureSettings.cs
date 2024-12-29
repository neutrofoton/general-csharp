using System;
using System.ComponentModel.DataAnnotations;
using Lab.ConfigurationEnv.Settings.Validations;

namespace Lab.ConfigurationEnv.Settings;

public class InfrastructureSettings : Validatable
{
    public CachingSettings Caching { get; set; } = new();
    public DatabaseSettings Database { get; set; } = new();
    public MessagingSettings Messaging { get; set; } = new();


}