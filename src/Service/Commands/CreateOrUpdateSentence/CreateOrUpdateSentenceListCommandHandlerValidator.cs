using FluentValidation;

namespace Service.Commands.CreateOrUpdateSentence;

public class CreateOrUpdateSentenceListCommandHandlerValidator : AbstractValidator<CreateOrUpdateSentenceListCommand>
{
    public CreateOrUpdateSentenceListCommandHandlerValidator(CreateOrUpdateSentenceCommandHandlerValidator sentenceCommandValidator)
    {
        if (sentenceCommandValidator is null) throw new ArgumentNullException(nameof(sentenceCommandValidator));
        
        RuleFor(x => x.Sentences)
            .NotEmpty()
            .WithMessage("Specify sentences")
            .WithSeverity(Severity.Error);

        RuleForEach(x => x.Sentences).SetValidator(sentenceCommandValidator);
    }
}
