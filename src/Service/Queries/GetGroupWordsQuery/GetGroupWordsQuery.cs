using MediatR;
using Service.Dto;

namespace Service.Queries.GetGroupWordsQuery;

/// <summary>
/// Постраничное получение слов группы
/// </summary>
public class GetGroupWordsQuery : IRequest<List<WordDto>>
{
    /// <summary>
    /// Идентификатор группы
    /// </summary>
    public Guid GroupId { get; set; }

    /// <summary>
    /// Последний Id предыдущей страницы
    /// </summary>
    /// <value></value>
    public long? LastId { get; set; }

    /// <summary>
    /// Размер страницы
    /// </summary>
    public int Limit { get; set; } = 1000;
}
