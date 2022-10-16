using Service.Dto;

namespace WebAPI.ViewModels.v1;

/// <summary>
/// Постраничное получение слов
/// </summary>
public class WordPageViewModel
{
    /// <summary>
    /// Последний ID страницы
    /// </summary>
    public long? LastId { get; set; }

    /// <summary>
    /// Слова страницы
    /// </summary>
    public IReadOnlyCollection<WordViewModel> Words { get; set; } = Array.Empty<WordViewModel>();

    /// <summary>
    /// Ctor
    /// </summary>
    public WordPageViewModel(IReadOnlyList<WordDto> wordDtoList)
    {
        if (wordDtoList is null)
            throw new ArgumentNullException(nameof(wordDtoList));

        LastId = wordDtoList.Count > 0 ? wordDtoList[^1].Id : null;
        Words = wordDtoList.Select(x => new WordViewModel(x)).ToList();
    }
}
