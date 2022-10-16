using MediatR;

namespace Service.Commands.DeleteGroup;

/// <summary>
/// Удаление групп
/// </summary>
public class DeleteGroupListCommand : IRequest
{
    /// <summary>
    /// Список идентификаторов групп
    /// </summary>
    public List<Guid> Groups { get; set; } = new List<Guid>(0);
}
