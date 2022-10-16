using MediatR;
using Service.Dto;

namespace Service.Commands.CreateOrUpdateSentence;

/// <summary>
/// Создание или обновление списка предложений
/// </summary>
public class CreateOrUpdateSentenceListCommand : IRequest<List<SentenceDto>>
{
    /// <summary>
    /// Список предложений
    /// </summary>
    public List<CreateOrUpdateSentenceCommand> Sentences { get; set; } = new List<CreateOrUpdateSentenceCommand>(0);
}
