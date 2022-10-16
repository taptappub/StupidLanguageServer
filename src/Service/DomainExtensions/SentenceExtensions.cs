using Domain.Entities;
using Service.Dto;

namespace Service.DomainExtensions;

static class SentenceExtensions
{
    public static SentenceDto ToDto(this Sentence sentence)
    {
        if (sentence is null) throw new ArgumentNullException(nameof(sentence));

        return new SentenceDto()
        {
            ExternalId = sentence.ExternalId,
            Id = sentence.Id,
            Value = sentence.Value,
            WordExternalIds = sentence.OrderedWords.Select(x => x.Word.ExternalId).ToList()
        };
    }
}
