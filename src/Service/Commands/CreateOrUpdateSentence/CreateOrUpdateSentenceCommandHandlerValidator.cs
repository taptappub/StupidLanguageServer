using FluentValidation;

namespace Service.Commands.CreateOrUpdateSentence;

public class CreateOrUpdateSentenceCommandHandlerValidator : AbstractValidator<CreateOrUpdateSentenceCommand>
{
    public CreateOrUpdateSentenceCommandHandlerValidator()
    {
        RuleFor(x => x.ExternalId)
            .Must(x => x is null || x != Guid.Empty)
            .WithMessage($"Specify null or not empty value")
            .WithSeverity(Severity.Error);

        RuleFor(x => x.Value)
            .NotEmpty()
            .WithMessage($"Specify not empty value")
            .WithSeverity(Severity.Error);

        RuleFor(x => x.WordExternalIds)
            .Must(x => x.Count <= 100)
            .WithMessage("Max allowed words count is 100")
            .WithSeverity(Severity.Error);
        RuleForEach(x => x.WordExternalIds)
            .NotEmpty()
            .WithMessage($"Specify not empty word externalId value");
    }
}
