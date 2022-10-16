using DataAccess.EF;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Service.Commands.DeleteSentences;

public class DeleteSentenceListCommandHandler : IRequestHandler<DeleteSentenceListCommand>
{
    private readonly IUnitOfWorkFactory _unitOfWorkFactory;
    private readonly ILogger<DeleteSentenceListCommandHandler> _logger;
    private readonly DeleteSentenceListCommandHandlerValidator _validator;

    public DeleteSentenceListCommandHandler(
        IUnitOfWorkFactory unitOfWorkFactory,
        ILogger<DeleteSentenceListCommandHandler> logger,
        DeleteSentenceListCommandHandlerValidator validator)
    {
        _unitOfWorkFactory = unitOfWorkFactory ?? throw new ArgumentNullException(nameof(unitOfWorkFactory));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        _validator = validator ?? throw new ArgumentNullException(nameof(validator));
    }

    public async Task<Unit> Handle(DeleteSentenceListCommand request, CancellationToken cancellationToken)
    {
        request.Validate(_validator, _logger);
        _logger.LogInformation("Start handling");
        try
        {
            await using var uow = _unitOfWorkFactory.Create();
            var sentences = await uow.Repositories.Sentence.GetSentencesByExternalIds(request.Sentences, cancellationToken);

            foreach (var sentence in sentences)
            {
                sentence.IsDeleted = true;
            }

            await uow.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
        finally
        {
            _logger.LogInformation("Handling completed");
        }
    }
}
