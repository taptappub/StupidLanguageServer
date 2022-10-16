using MediatR;
using Service.Dto;

namespace Service.Commands.CreateOrUpdateSentence;

/// <summary>
/// Создание или обновление предложения
/// </summary>
public class CreateOrUpdateSentenceCommand
{
    /// <summary>
    /// Идентификатор предложения
    /// </summary>
    public Guid? ExternalId { get; set; }

    /// <summary>
    /// Текст предложения
    /// </summary>
    public string Value { get; set; } = string.Empty;

    /// <summary>
    /// Слова предложения
    /// </summary>
    public HashSet<Guid> WordExternalIds { get; set; } = new HashSet<Guid>(0);
}
