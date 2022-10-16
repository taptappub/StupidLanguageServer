using Service.Dto;

namespace WebAPI.ViewModels.v1;

/// <summary>
/// Пользователь
/// </summary>
public class UserViewModel
{
    /// <summary>
    /// Идентификатор
    /// </summary>
    public string ExternalId { get; set; } = string.Empty;

    /// <summary>
    /// Имя пользователя
    /// </summary>
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// Ссылка на аватар
    /// </summary>
    public string? AvatarUrl { get; set; }

    /// <summary>
    /// Ctor
    /// </summary>
    public UserViewModel(UserDto userDto)
    {
        if (userDto is null) throw new ArgumentNullException(nameof(userDto));

        ExternalId = userDto.ExternalId;
        Name = userDto.Name;
        AvatarUrl = userDto.AvatarUrl;
    }
}
