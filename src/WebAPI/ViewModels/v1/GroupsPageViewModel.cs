namespace WebAPI.ViewModels.v1;

/// <summary>
/// Страница групп
/// </summary>
public class GroupsPageViewModel
{
    /// <summary>
    /// Последний ID страницы
    /// </summary>
    public long? LastId { get; init; }

    /// <summary>
    /// Группы страницы
    /// </summary>
    public IReadOnlyCollection<GroupViewModel> Groups { get; init; } = Array.Empty<GroupViewModel>();
}
