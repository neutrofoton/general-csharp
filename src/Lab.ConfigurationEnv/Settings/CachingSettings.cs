using System;
using System.ComponentModel.DataAnnotations;
using Lab.ConfigurationEnv.Settings.Validations;

namespace Lab.ConfigurationEnv.Settings;

public class CachingSettings : Validatable
{
    public RedisSettings Redis { get; set; } = new();

}