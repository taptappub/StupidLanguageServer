using Domain.Entities;
using Service.Dto;

namespace Service.DomainExtensions;

public static class WordExtensions
{
    public static WordDto ToDto(this Word w)
    {
        if (w is null) throw new ArgumentNullException(nameof(w));

        return new WordDto
        {
            Description = w.Description,
            ExternalId = w.ExternalId,
            GroupId = w.Group.ExternalId,
            Id = w.Id,
            ImageUrl = w.ImageUrl,
            RepetitionProgress = w.RepetitionProgress,
            Value = w.Value
        };
    }
}
