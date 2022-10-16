using ValidationResult = Infrastructure.Exceptions.Models.ValidationResult;

namespace WebAPI.ViewModels;

/// <summary>
/// Ошибки валидации
/// </summary>
public class ValidationErrorViewModel
{
    /// <summary>
    /// Сообщение ошибки
    /// </summary>
    public string? Message { get; init; }

    /// <summary>
    /// Результат валидации полей
    /// </summary>
    public IReadOnlyCollection<ValidationResult> ValidationResults { get; init; } = Array.Empty<ValidationResult>();
}
