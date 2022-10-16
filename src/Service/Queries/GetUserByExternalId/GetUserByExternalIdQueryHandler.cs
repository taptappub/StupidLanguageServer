using DataAccess.EF;
using Domain.Entities;
using Infrastructure.Exceptions;
using Infrastructure.Logging;
using MediatR;
using Microsoft.Extensions.Logging;
using Service.DomainExtensions;
using Service.Dto;

namespace Service.Queries.GetUserByExternalId;

public class GetUserByExternalIdQueryHandler : IRequestHandler<GetUserByExternalIdQuery, UserDto>
{
    private readonly IUnitOfWorkFactory _unitOfWorkFactory;
    private readonly ILogger<GetUserByExternalIdQueryHandler> _logger;

    public GetUserByExternalIdQueryHandler(IUnitOfWorkFactory unitOfWorkFactory, ILogger<GetUserByExternalIdQueryHandler> logger)
    {
        _unitOfWorkFactory = unitOfWorkFactory ?? throw new ArgumentNullException(nameof(unitOfWorkFactory));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    public async Task<UserDto> Handle(GetUserByExternalIdQuery request, CancellationToken cancellationToken)
    {
        if (request is null) throw new ArgumentNullException(nameof(request));

        using var propLogScope = _logger.SetProperty($"{nameof(User)}.ExternalId", request.ExternalId);
        _logger.LogInformation("Start handling");
        try
        {
            await using var uow = _unitOfWorkFactory.CreateReadOnly();

            var user = await uow.Repositories.User.GetByExternalId(request.ExternalId, cancellationToken);
            if (user is null)
            {
                var message = $"{nameof(User)} not found";
                _logger.LogWarning(message);

                var ex = new ObjectNotFoundException(message);
                ex.Data.Add($"{nameof(User)}.{nameof(User.ExternalId)}", request.ExternalId);
                throw ex;
            }
            return user.ToDto();
        }
        finally
        {
            _logger.LogInformation("Handling completed");
        }
    }
}
