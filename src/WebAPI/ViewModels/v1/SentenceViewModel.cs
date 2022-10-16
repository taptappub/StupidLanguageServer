using Service.Dto;

namespace WebAPI.ViewModels.v1;

/// <summary>
/// Предложение
/// </summary>
public class SentenceViewModel
{
    /// <summary>
    /// Идентификатор
    /// </summary>
    public Guid ExternalId { get; set; }

    /// <summary>
    /// Текст предложения
    /// </summary>
    public string Value { get; set; } = string.Empty;

    /// <summary>
    /// Слова предложения
    /// </summary>
    public IReadOnlyCollection<Guid> Words { get; set; } = Array.Empty<Guid>();

    /// <summary>
    /// Ctor
    /// </summary>
    public SentenceViewModel(SentenceDto sentenceDto)
    {
        if (sentenceDto is null) throw new ArgumentNullException(nameof(sentenceDto));

        ExternalId = sentenceDto.ExternalId;
        Value = sentenceDto.Value;
        Words = sentenceDto.WordExternalIds;
    }
}
