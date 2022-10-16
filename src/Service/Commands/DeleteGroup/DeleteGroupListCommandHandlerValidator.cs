using FluentValidation;

namespace Service.Commands.DeleteGroup;

public class DeleteGroupListCommandHandlerValidator: AbstractValidator<DeleteGroupListCommand>
{
    public DeleteGroupListCommandHandlerValidator()
    {
        RuleFor(x => x.Groups)
            .NotEmpty()
            .WithMessage("Specify groups")
            .WithSeverity(Severity.Error);
            
        RuleForEach(x => x.Groups)
            .NotEmpty()
            .WithMessage("Specify not empty groups externalId")
            .WithSeverity(Severity.Error);
    }
}
