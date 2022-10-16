using FluentValidation;
using Infrastructure.Exceptions;
using Infrastructure.Exceptions.Models;
using Infrastructure.Logging;
using Microsoft.Extensions.Logging;

namespace Service;

internal static class ValidationExtensions
{
    public static void Validate<T>(this T? model, IValidator<T> validator, ILogger? logger)
    {
        if (model is null)
        {
            var message = "Specify request parameters";

            logger?.LogWarning(message);
            throw new ValidationErrorException(message);
        }

        ValidateModel(model, validator, logger);
    }

    private static void ValidateModel<TModel>(TModel model, IValidator<TModel> validator, ILogger? logger)
    {
        var result = validator.Validate(model);
        if (!result.IsValid)
        {
            var messages = result.Errors
                .Select(x => new ValidationResult
                {
                    Message = x.ErrorMessage,
                    PropertyName = x.PropertyName,
                    Severity = x.Severity.ToString()
                })
                .ToList();

            using (logger?.SetProperty("ValidationErrors", messages, true))
            {
                logger?.LogWarning("Validation failed");
            }

            throw new ValidationErrorException("Validation failed", messages);
        }
    }
}
