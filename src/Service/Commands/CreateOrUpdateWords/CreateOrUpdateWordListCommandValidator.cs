using FluentValidation;

namespace Service.Commands.CreateOrUpdateWords;

public class CreateOrUpdateWordListCommandValidator: AbstractValidator<CreateOrUpdateWordListCommnad>
{
    public CreateOrUpdateWordListCommandValidator(CreateOrUpdateWordCommandValidator commandValidator)
    {
        if (commandValidator is null)
            throw new ArgumentNullException(nameof(commandValidator));

        RuleFor(x => x.Words)
            .Must(x=>x.Count < 500)
            .WithSeverity(Severity.Error)
            .WithMessage("Max allowed words count is 500");
            
        RuleForEach(x => x.Words).SetValidator(commandValidator);
    }
}
