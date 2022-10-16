using MediatR;
using Service.Dto;

namespace Service.Commands.CreateOrUpdateUser;

/// <summary>
/// Создание или обновление пользователя
/// </summary>
public class CreateOrUpdateUserCommand : IRequest<UserDto>
{
    /// <summary>
    /// Идентификатор пользователя
    /// </summary>
    public string ExternalId { get; } = string.Empty;

    /// <summary>
    /// Имя пользователя
    /// </summary>
    public string Name { get; } = string.Empty;

    /// <summary>
    /// Ссылка на аватарку
    /// </summary>
    public string? AvatarUrl { get; }

    /// <summary>
    /// Ctor
    /// </summary>
    public CreateOrUpdateUserCommand(string externalId, string name, string? avatarUrl)
    {
        if (string.IsNullOrEmpty(externalId)) throw new ArgumentException(nameof(externalId));
        if (string.IsNullOrEmpty(name)) throw new ArgumentException(nameof(name));

        ExternalId = externalId;
        Name = name;
        AvatarUrl = avatarUrl;
    }
}
