using FluentValidation;

namespace Service.Commands.CreateOrUpdateGroup;

public class CreateOrUpdateGroupCommandValidator : AbstractValidator<CreateOrUpdateGroupCommand>
{
    public CreateOrUpdateGroupCommandValidator()
    {
        RuleFor(x => x.ExternalId)
            .Must(x=> x is null || x.Value != Guid.Empty)
            .WithMessage($"Specify not empty value")
            .WithSeverity(Severity.Error);

        RuleFor(x => x.Name)
            .NotEmpty()
            .WithMessage($"Specify not empty value")
            .WithSeverity(Severity.Error);

        RuleFor(x => x.RepetitionProgress)
            .GreaterThanOrEqualTo(0)
            .WithMessage($"Specify not negative value")
            .WithSeverity(Severity.Error);
    }
}
