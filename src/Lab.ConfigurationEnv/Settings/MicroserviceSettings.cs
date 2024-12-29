using System;
using System.ComponentModel.DataAnnotations;
using Lab.ConfigurationEnv.Settings.Validations;

namespace Lab.ConfigurationEnv.Settings;

public class MicroserviceSettings : Validatable
{
    public InfrastructureSettings Infrastructure { get; set; } = new();

    [Range(1, 65535, ErrorMessage = "MyPort must be between 1 and 65535.")]
    public int MyPort { get; set; }
}