using MediatR;
using Service.Dto;

namespace Service.Queries.GetSentences;

/// <summary>
/// Постраничное получение предложений
/// </summary>
public class GetSentencesPageQuery : IRequest<List<SentenceDto>>
{
    /// <summary>
    /// Последний Id предыдущей страницы
    /// </summary>
    public long? LastId { get; set; }

    /// <summary>
    /// Размер страницы
    /// </summary>
    public int Limit { get; set; } = 250;
}
