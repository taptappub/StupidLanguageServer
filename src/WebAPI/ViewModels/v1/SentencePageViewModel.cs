using Service.Dto;

namespace WebAPI.ViewModels.v1;

/// <summary>
/// Страница предложений
/// </summary>
public class SentencePageViewModel
{
    /// <summary>
    /// Последний ID страницы
    /// </summary>
    public long? LastId { get; set; }

    /// <summary>
    /// Предложения страницы
    /// </summary>
    public List<SentenceViewModel> Sentences { get; set; } = new List<SentenceViewModel>(0);

    /// <summary>
    /// Ctor
    /// </summary>
    public SentencePageViewModel(IReadOnlyList<SentenceDto> sentenceDtos)
    {
        if (sentenceDtos is null)
            throw new ArgumentNullException(nameof(sentenceDtos));

        LastId = sentenceDtos.Count > 0 ? sentenceDtos[^1].Id : null;
        Sentences = sentenceDtos.Select(x => new SentenceViewModel(x)).ToList();
    }
}
