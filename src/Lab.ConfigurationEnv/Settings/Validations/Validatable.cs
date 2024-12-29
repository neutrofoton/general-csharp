using System;
using System.ComponentModel.DataAnnotations;

namespace Lab.ConfigurationEnv.Settings.Validations;

public abstract class Validatable : IValidatable
{
    public virtual List<ValidationResult> Validate()
    {
        var results = new List<ValidationResult>();
        var context = new ValidationContext(this);

        // Validate current object's properties
        Validator.TryValidateObject(this, context, results, validateAllProperties: true);

        // Recursively validate nested properties
        foreach (var property in GetType().GetProperties())
        {
            if (typeof(IValidatable).IsAssignableFrom(property.PropertyType))
            {
                var nestedValue = property.GetValue(this) as IValidatable;
                if (nestedValue != null)
                {
                    results.AddRange(nestedValue.Validate());
                }
            }
            else if (typeof(IEnumerable<IValidatable>).IsAssignableFrom(property.PropertyType))
            {
                // Handle collections of IValidatable
                var collection = property.GetValue(this) as IEnumerable<IValidatable>;
                if (collection != null)
                {
                    foreach (var item in collection)
                    {
                        results.AddRange(item.Validate());
                    }
                }
            }
        }

        return results;
    }
}
