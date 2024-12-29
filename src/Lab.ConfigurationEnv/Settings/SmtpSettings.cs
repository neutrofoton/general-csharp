using System;
using System.ComponentModel.DataAnnotations;
using Lab.ConfigurationEnv.Settings.Validations;

namespace Lab.ConfigurationEnv.Settings;

public class SmtpSettings : Validatable
{
    public string Server { get; set; }

    [Range(1, 65535, ErrorMessage = "Port must be between 1 and 65535.")]
    public int Port { get; set; }

    [Required]
    public string Username { get; set; }

    [Required]
    public string Password { get; set; }

    public bool IsEnableSsl { get; set; }
}