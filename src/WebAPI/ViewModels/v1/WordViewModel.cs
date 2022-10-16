using Service.Dto;

namespace WebAPI.ViewModels.v1;

/// <summary>
/// Слово
/// </summary>
public class WordViewModel
{
    /// <summary>
    /// Идентификатор
    /// </summary>
    public Guid ExternalId { get; set; }

    /// <summary>
    /// Текст слова
    /// </summary>
    public string Value { get; set; } = string.Empty;

    public int RepetitionProgress { get; set; }

    /// <summary>
    /// Описание
    /// </summary>
    public string? Description { get; set; }

    /// <summary>
    /// Ссылка на изображение
    /// </summary>
    public string? ImageUrl { get; set; }

    /// <summary>
    /// Идентификатор группы
    /// </summary>
    /// <value></value>
    public Guid GroupId { get; set; }

    /// <summary>
    /// Ctor
    /// </summary>
    public WordViewModel(WordDto wordDto)
    {
        if (wordDto is null) throw new ArgumentNullException(nameof(wordDto));

        Description = wordDto.Description;
        ExternalId = wordDto.ExternalId;
        GroupId = wordDto.GroupId;
        ImageUrl = wordDto.ImageUrl;
        RepetitionProgress = wordDto.RepetitionProgress;
        Value = wordDto.Value;
    }
}
