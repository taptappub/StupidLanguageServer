using FluentValidation;

namespace Service.Commands.DeleteWords;

public class DeleteWordListCommandHandlerValidator : AbstractValidator<DeleteWordListCommand>
{
    public DeleteWordListCommandHandlerValidator()
    {
        RuleFor(x => x.Words)
            .NotEmpty()
            .WithMessage("Specify words")
            .WithSeverity(Severity.Error);
            
        RuleForEach(x => x.Words)
            .NotEmpty()
            .WithMessage("Specify not empty words externalId")
            .WithSeverity(Severity.Error);
    }
}
