using FluentValidation;

namespace Service.Commands.CreateOrUpdateUser;

public class CreateOrUpdateUserCommandValidator : AbstractValidator<CreateOrUpdateUserCommand>
{
    public CreateOrUpdateUserCommandValidator()
    {
        RuleFor(x => x.ExternalId)
            .NotEmpty()
            .WithMessage($"Specify not empty value")
            .WithSeverity(Severity.Error);

        RuleFor(x => x.Name)
            .NotEmpty()
            .WithMessage($"Specify not empty value")
            .WithSeverity(Severity.Error);

        RuleFor(x => x.AvatarUrl)
            .Must(v => v is null || Uri.IsWellFormedUriString(v, UriKind.Absolute))
            .WithMessage($"Specify well formed url or null")
            .WithSeverity(Severity.Error);
    }
}
