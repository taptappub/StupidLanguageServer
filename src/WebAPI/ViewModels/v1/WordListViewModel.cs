namespace WebAPI.ViewModels.v1;

/// <summary>
/// Список слов
/// </summary>
public class WordListViewModel
{
    /// <summary>
    /// Слова
    /// </summary>
    public IReadOnlyCollection<WordViewModel> Words { get; set; } = Array.Empty<WordViewModel>();
}
