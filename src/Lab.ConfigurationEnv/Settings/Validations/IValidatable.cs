using System;
using System.ComponentModel.DataAnnotations;

namespace Lab.ConfigurationEnv.Settings;

public interface IValidatable
{
    List<ValidationResult> Validate();
}
