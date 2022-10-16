using MediatR;

namespace Service.Commands.DeleteWords;

/// <summary>
/// Удаление списка слов
/// </summary>
public class DeleteWordListCommand : IRequest
{
    /// <summary>
    /// список идентификаторов слов
    /// </summary>
    public List<Guid> Words { get; set; } = new List<Guid>(0);
}
