using Infrastructure.Exceptions.Models;

namespace Infrastructure.Exceptions;

public class ValidationErrorException : HttpException
{
    public IReadOnlyCollection<ValidationResult> ValidationResults { get; }

    public ValidationErrorException(string message, IReadOnlyCollection<ValidationResult>? results = null) 
        : base(message)
    {
        ValidationResults = results ?? Array.Empty<ValidationResult>();
        HttpStatus = 400;
    }
}
