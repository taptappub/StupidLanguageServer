using MediatR;

namespace Service.Commands.DeleteSentences;

/// <summary>
/// Удаление списка предложений
/// </summary>
public class DeleteSentenceListCommand : IRequest
{
    /// <summary>
    /// Список идентификаторов предложений
    /// </summary>
    public List<Guid> Sentences { get; set; } = new List<Guid>(0);
}
