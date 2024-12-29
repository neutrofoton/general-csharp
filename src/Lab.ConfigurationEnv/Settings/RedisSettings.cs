using System;
using System.ComponentModel.DataAnnotations;
using Lab.ConfigurationEnv.Settings.Validations;

namespace Lab.ConfigurationEnv.Settings;

public class RedisSettings : Validatable
{
    [Required]
    public string Host { get; set; }

    [Range(1, 65535, ErrorMessage = "Port must be between 1 and 65535.")]
    public int? Port { get; set; }

    [Required]
    public string InstanceName { get; set; }
}