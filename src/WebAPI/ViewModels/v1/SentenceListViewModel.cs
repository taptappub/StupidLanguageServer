using Service.Dto;

namespace WebAPI.ViewModels.v1;

/// <summary>
/// Запрос создания/обновления предложений
/// </summary>
public class SentenceListViewModel
{
    /// <summary>
    /// Предложения
    /// </summary>
    public List<SentenceViewModel> Sentences { get; set; } = new List<SentenceViewModel>(0);

    /// <summary>
    /// Ctor
    /// </summary>
    public SentenceListViewModel(List<SentenceDto> sentenceDtoList)
    {
        if (sentenceDtoList is null)
            throw new ArgumentNullException(nameof(sentenceDtoList));

        Sentences = sentenceDtoList.Select(x => new SentenceViewModel(x)).ToList();
    }
}
