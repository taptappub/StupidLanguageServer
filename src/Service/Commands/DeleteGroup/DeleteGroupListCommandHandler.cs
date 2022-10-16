using DataAccess.EF;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Service.Commands.DeleteGroup;

public class DeleteGroupListCommandHandler : IRequestHandler<DeleteGroupListCommand>
{
    private readonly IUnitOfWorkFactory _unitOfWorkFactory;
    private readonly ILogger<DeleteGroupListCommandHandler> _logger;
    private readonly DeleteGroupListCommandHandlerValidator _validator;

    public DeleteGroupListCommandHandler(
        IUnitOfWorkFactory unitOfWorkFactory,
        ILogger<DeleteGroupListCommandHandler> logger,
        DeleteGroupListCommandHandlerValidator validator)
    {
        _unitOfWorkFactory = unitOfWorkFactory ?? throw new ArgumentNullException(nameof(unitOfWorkFactory));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        _validator = validator ?? throw new ArgumentNullException(nameof(validator));
    }

    public async Task<Unit> Handle(DeleteGroupListCommand request, CancellationToken cancellationToken)
    {
        request.Validate(_validator, _logger);
        _logger.LogInformation("Start handling");
        try
        {
            await using var uow = _unitOfWorkFactory.Create();
            var groups = await uow.Repositories.Group.GetByExternalIds(request.Groups, cancellationToken);

            foreach (var group in groups)
            {
                group.IsDeleted = true;
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
