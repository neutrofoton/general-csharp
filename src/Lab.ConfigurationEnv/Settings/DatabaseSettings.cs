using System;
using System.ComponentModel.DataAnnotations;
using Lab.ConfigurationEnv.Settings.Validations;

namespace Lab.ConfigurationEnv.Settings;

public class DatabaseSettings : Validatable
{
    [Required(AllowEmptyStrings =false,ErrorMessage ="ConnectionString is reqired")]
    public string ConnectionStrings { get; set; }
}