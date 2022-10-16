using FluentValidation;

namespace Service.Commands.CreateOrUpdateWords;

public class CreateOrUpdateWordCommandValidator : AbstractValidator<CreateOrUpdateWordCommnad>
{
    public CreateOrUpdateWordCommandValidator()
    {
        RuleFor(x => x.ExternalId)
            .Must(x => x is null || x != Guid.Empty)
            .WithMessage($"Specify null or not empty value")
            .WithSeverity(Severity.Error);

        RuleFor(x => x.Value)
            .NotEmpty()
            .WithMessage($"Specify not empty value")
            .WithSeverity(Severity.Error);

        RuleFor(x => x.RepetitionProgress)
            .GreaterThanOrEqualTo(0)
            .WithMessage($"Specify not negative value")
            .WithSeverity(Severity.Error);

        RuleFor(x => x.Description)
            .MaximumLength(8192)
            .WithMessage($"Specify value length can not be more then 8192")
            .WithSeverity(Severity.Error);

        RuleFor(x => x.ImageUrl)
            .Must(v => v is null || Uri.IsWellFormedUriString(v, UriKind.Absolute))
            .WithMessage($"Specify well formed url or null")
            .WithSeverity(Severity.Error);

        RuleFor(x => x.GroupId)
            .Must(x => x != Guid.Empty)
            .WithMessage($"Specify not empty value")
            .WithSeverity(Severity.Error);
    }
}
