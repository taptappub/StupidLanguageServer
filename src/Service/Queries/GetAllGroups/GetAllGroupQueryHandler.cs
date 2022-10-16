using DataAccess.EF;
using MediatR;
using Microsoft.Extensions.Logging;
using Service.DomainExtensions;
using Service.Dto;

namespace Service.Queries.GetAllGroups;

public class GetAllGroupsQueryHandler : IRequestHandler<GetAllGroupsQuery, List<GroupDto>>
{
    private readonly IUnitOfWorkFactory _unitOfWorkFactory;
    private readonly ILogger<GetAllGroupsQueryHandler> _logger;

    public GetAllGroupsQueryHandler(IUnitOfWorkFactory unitOfWorkFactory, ILogger<GetAllGroupsQueryHandler> logger)
    {
        _unitOfWorkFactory = unitOfWorkFactory ?? throw new ArgumentNullException(nameof(unitOfWorkFactory));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    public async Task<List<GroupDto>> Handle(GetAllGroupsQuery request, CancellationToken cancellationToken)
    {
        if (request is null) throw new ArgumentNullException(nameof(request));

        _logger.LogInformation("Start handling");
        try
        {
            await using var uow = _unitOfWorkFactory.CreateReadOnly();
            var groupRepository = uow.Repositories.Group;

            var list = await groupRepository.GetList(request.LastId, request.Limit, cancellationToken);

            return list
                .Select(x => x.ToDto())
                .ToList();
        }
        finally
        {
            _logger.LogInformation("Handling completed");
        }
    }
}
