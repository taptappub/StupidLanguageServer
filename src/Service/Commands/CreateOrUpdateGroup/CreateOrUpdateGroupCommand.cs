using MediatR;
using Service.Dto;

namespace Service.Commands.CreateOrUpdateGroup;

/// <summary>
/// Создание или обновление группы
/// </summary>
public class CreateOrUpdateGroupCommand : IRequest<GroupDto>
{
    /// <summary>
    /// Идентификатор группы
    /// </summary>
    public Guid? ExternalId { get; set; }

    /// <summary>
    /// Название
    /// </summary>
    public string Name { get; set; } = string.Empty;

    public int RepetitionProgress { get; set; }
}
