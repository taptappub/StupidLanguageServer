using DataAccess.EF;
using Domain.Entities;
using Infrastructure.Exceptions;
using Infrastructure.Logging;
using MediatR;
using Microsoft.Extensions.Logging;
using Service.DomainExtensions;
using Service.Dto;

namespace Service.Queries.GetGroupWordsQuery;

public class GetGroupWordsQueryHandler : IRequestHandler<GetGroupWordsQuery, List<WordDto>>
{
    private readonly IUnitOfWorkFactory _unitOfWorkFactory;
    private readonly ILogger<GetGroupWordsQueryHandler> _logger;

    public GetGroupWordsQueryHandler(IUnitOfWorkFactory unitOfWorkFactory, ILogger<GetGroupWordsQueryHandler> logger)
    {
        _unitOfWorkFactory = unitOfWorkFactory ?? throw new ArgumentNullException(nameof(unitOfWorkFactory));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    public async Task<List<WordDto>> Handle(GetGroupWordsQuery request, CancellationToken cancellationToken)
    {
        if (request is null) throw new ArgumentNullException(nameof(request));

        using var propLogScope = _logger.SetProperty($"{nameof(Group)}.ExternalId", request.GroupId);
        _logger.LogInformation("Start handling");
        try
        {
            await using var uow = _unitOfWorkFactory.CreateReadOnly();

            var group = await uow.Repositories.Group.GetByExternalId(request.GroupId, cancellationToken);
            if (group is null)
            {
                var message = $"{nameof(Group)} not found";
                
                _logger.LogWarning(message);
                var ex = new ObjectNotFoundException(message);
                ex.Data.Add($"{nameof(Group)}.{nameof(Group.ExternalId)}", request.GroupId);
                throw ex;
            }
            var wordPage = await uow.Repositories.Word.GetGroupWords(group.ExternalId, request.LastId, request.Limit, cancellationToken);

            return wordPage.Select(x => x.ToDto())
                .ToList();
        }
        finally
        {
            _logger.LogInformation("Handling completed");
        }
    }
}
