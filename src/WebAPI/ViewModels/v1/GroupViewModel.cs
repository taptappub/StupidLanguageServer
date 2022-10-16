using Service.Dto;

namespace WebAPI.ViewModels.v1;

/// <summary>
/// Группа
/// </summary>
public class GroupViewModel
{
    /// <summary>
    /// Внешний ID группы
    /// </summary>
    public Guid ExternalId { get; set; }

    /// <summary>
    /// Название группы
    /// </summary>
    public string? Name { get; set; }

    public int RepetitionProgress { get; set; }

        public GroupViewModel(GroupDto groupDto)
    {
        if (groupDto is null) throw new ArgumentNullException(nameof(groupDto));

        ExternalId = groupDto.ExternalId;
        Name = groupDto.Name;
        RepetitionProgress = groupDto.RepetitionProgress;
    }
}
