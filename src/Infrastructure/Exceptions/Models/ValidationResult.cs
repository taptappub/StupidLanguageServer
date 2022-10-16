namespace Infrastructure.Exceptions.Models;

/// <summary>
/// Результат валидации
/// </summary>
public class ValidationResult
{
    /// <summary>
    /// Поле
    /// </summary>
    public string? PropertyName { get; init; }

    /// <summary>
    /// Уровень
    /// </summary>
    public string? Severity { get; init; }

    /// <summary>
    /// Сообщение
    /// </summary>
    public string Message { get; init; } = string.Empty;
}