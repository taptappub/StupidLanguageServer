using Service.Queries.GetGroupWordsQuery;

namespace WebAPI.ViewModels.v1;

/// <summary>
/// Запрос постраничного получения слов группы
/// </summary>
public class GetGroupWordsQueryViewModel
{
    /// <summary>
    /// Последний Id предыдущей страницы
    /// </summary>
    public long? LastId { get; set; }

    /// <summary>
    /// Размер страницы
    /// </summary>
    public int Limit { get; set; } = 500;

    /// <summary>
    /// Конвертация к модели query
    /// </summary>
    internal GetGroupWordsQuery ToQuery(Guid groupExternalId) =>
        new GetGroupWordsQuery
        {
            GroupId = groupExternalId,
            LastId = LastId,
            Limit = Limit
        };
}
