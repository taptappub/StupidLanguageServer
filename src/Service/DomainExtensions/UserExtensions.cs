using Domain.Entities;
using Service.Dto;

namespace Service.DomainExtensions;

internal static class UserExtensions
{
    public static UserDto ToDto(this User user)
    {
        if (user is null) throw new ArgumentNullException(nameof(user));

        return new UserDto
        {
            AvatarUrl = user.AvatarUrl,
            ExternalId = user.ExternalId,
            Id = user.Id,
            Name = user.Name
        };
    }
}
