using MediatR;
using Service.Dto;

namespace Service.Queries.GetAllGroups;

/// <summary>
/// Запрос на постраничное получение групп
/// </summary>
public class GetAllGroupsQuery : IRequest<List<GroupDto>>
{
    /// <summary>
    /// Последний Id предыдущей страницы
    /// </summary>
    public long? LastId { get; set; }

    /// <summary>
    /// Размер страницы
    /// </summary>
    public int Limit { get; set; } = 500;
}
