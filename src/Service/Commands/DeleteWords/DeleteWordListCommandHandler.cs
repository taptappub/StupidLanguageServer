using DataAccess.EF;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Service.Commands.DeleteWords;

public class DeleteWordListCommandHandler : IRequestHandler<DeleteWordListCommand>
{
    private readonly IUnitOfWorkFactory _unitOfWorkFactory;
    private readonly ILogger<DeleteWordListCommandHandler> _logger;
    private readonly DeleteWordListCommandHandlerValidator _validator;

    public DeleteWordListCommandHandler(
        IUnitOfWorkFactory unitOfWorkFactory,
        ILogger<DeleteWordListCommandHandler> logger,
        DeleteWordListCommandHandlerValidator validator)
    {
        _unitOfWorkFactory = unitOfWorkFactory ?? throw new ArgumentNullException(nameof(unitOfWorkFactory));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        _validator = validator ?? throw new ArgumentNullException(nameof(validator));
    }

    public async Task<Unit> Handle(DeleteWordListCommand request, CancellationToken cancellationToken)
    {
        request.Validate(_validator, _logger);
        _logger.LogInformation("Start handling");
        try
        {
            await using var uow = _unitOfWorkFactory.Create();
            var words = await uow.Repositories.Word.GetWordsByExternalIds(request.Words, cancellationToken);

            foreach (var word in words)
            {
                word.IsDeleted = true;
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
