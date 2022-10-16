namespace Service.Commands.CreateOrUpdateWords;

/// <summary>
/// Создание или обновление слова
/// </summary>
public class CreateOrUpdateWordCommnad
{
    /// <summary>
    /// Идентификатор слова
    /// </summary>
    public Guid? ExternalId { get; set; }

    /// <summary>
    /// Слово
    /// </summary>
    public string Value { get; set; } = string.Empty;

    public int RepetitionProgress { get; set; }

    /// <summary>
    /// Описание слова
    /// </summary>
    public string? Description { get; set; }

    /// <summary>
    /// Ссылка на картику
    /// </summary>
    public string? ImageUrl { get; set; }

    /// <summary>
    /// Идентифкатор группы, к которой относится слово
    /// </summary>
    public Guid GroupId { get; set; }
}
