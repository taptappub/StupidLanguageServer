using Domain.Entities;
using Service.Dto;

namespace Service.DomainExtensions;

public static class GroupExtensions
{
    public static GroupDto ToDto(this Group group)
    {
        if (group is null) throw new ArgumentNullException(nameof(group));

        return new GroupDto
        {
            ExternalId = group.ExternalId,
            Id = group.Id,
            Name = group.Name,
            RepetitionProgress = group.RepetitionProgress
        };
    }
}
