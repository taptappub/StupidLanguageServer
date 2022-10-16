using FluentValidation;

namespace Service.Commands.DeleteSentences;

public class DeleteSentenceListCommandHandlerValidator : AbstractValidator<DeleteSentenceListCommand>
{
    public DeleteSentenceListCommandHandlerValidator()
    {
        RuleFor(x => x.Sentences)
            .NotEmpty()
            .WithMessage("Specify sentences")
            .WithSeverity(Severity.Error);
            
        RuleForEach(x => x.Sentences)
            .NotEmpty()
            .WithMessage("Specify not empty sentence externalId")
            .WithSeverity(Severity.Error);
    }
}
