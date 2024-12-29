using System;
using System.ComponentModel.DataAnnotations;
using Lab.ConfigurationEnv.Settings.Validations;

namespace Lab.ConfigurationEnv.Settings;

public class MessagingSettings : Validatable
{
    [Required]
    public RabbitMQSettings? RabbitMQ { get; set; }

    [Required]
    public SmtpSettings? Smtp { get; set; }
}