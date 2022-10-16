using DataAccess.EF;
using Domain.Entities;
using Infrastructure.Exceptions;
using Infrastructure.Logging;
using MediatR;
using Microsoft.Extensions.Logging;
using Service.DomainExtensions;
using Service.Dto;

namespace Service.Commands.CreateOrUpdateGroup;

public class CreateOrUpdateGroupCommandHandler : IRequestHandler<CreateOrUpdateGroupCommand, GroupDto>
{
    private readonly IUnitOfWorkFactory _unitOfWorkFactory;
    private readonly ILogger<CreateOrUpdateGroupCommandHandler> _logger;
    private readonly CreateOrUpdateGroupCommandValidator _validator;

    public CreateOrUpdateGroupCommandHandler(
        IUnitOfWorkFactory unitOfWorkFactory,
        ILogger<CreateOrUpdateGroupCommandHandler> logger,
        CreateOrUpdateGroupCommandValidator validator)
    {
        _unitOfWorkFactory = unitOfWorkFactory ?? throw new ArgumentNullException(nameof(unitOfWorkFactory));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        _validator = validator ?? throw new ArgumentNullException(nameof(validator));
    }

    public async Task<GroupDto> Handle(CreateOrUpdateGroupCommand request, CancellationToken cancellationToken)
    {
        if (request is null) throw new ArgumentNullException(nameof(request));

        request.Validate(_validator, _logger);
        using var propLogScope = _logger.SetProperty($"{nameof(Group)}.{nameof(Group.ExternalId)}", request.ExternalId);
        _logger.LogInformation("Start handling");
        try
        {
            await using var uow = _unitOfWorkFactory.Create();
            var user = await uow.GetAuthorizedUser(_logger, cancellationToken);
            
            var group = request.ExternalId is null
                ? await CreateGroup(uow, request, user, cancellationToken)
                : await UpdateGroup(request, uow, cancellationToken);

            return group.ToDto();
        }
        finally
        {
            _logger.LogInformation("Handling completed");
        }
    }

    private async Task<Group> UpdateGroup(
        CreateOrUpdateGroupCommand request,
        IUnitOfWork uow,
        CancellationToken cancellationToken)
    {
        _logger.LogInformation($"Updating {nameof(Group)}");

        var group = await uow.Repositories.Group.GetByExternalId(request.ExternalId!.Value, cancellationToken);
        if (group is null)
        {
            _logger.LogWarning($"{nameof(Group)} not found");
            
            var ex = new ObjectNotFoundException($"{nameof(Group)} not found");
            ex.Data.Add($"{nameof(Group)}.{nameof(Group.ExternalId)}", request.ExternalId);
            throw ex;
        }
        group.Name = request.Name;
        group.RepetitionProgress = request.RepetitionProgress;

        await uow.SaveChangesAsync(cancellationToken);
        _logger.LogInformation($"{nameof(Group)}.{nameof(Group.Id)}={{{nameof(Group)}.{nameof(Group.Id)}}} updated successfully", group.Id);

        return group;
    }

    private async Task<Group> CreateGroup(
        IUnitOfWork uow,
        CreateOrUpdateGroupCommand request,
        User user,
        CancellationToken cancellationToken)
    {
        _logger.LogInformation($"Creating new {nameof(Group)}");

        var group = new Group(user, request.Name)
        {
            RepetitionProgress = request.RepetitionProgress,
        };
        uow.Repositories.Group.Add(group);

        await uow.SaveChangesAsync(cancellationToken);
        _logger.LogInformation($"New {nameof(Group)}.{nameof(Group.Id)}={{{nameof(Group)}.{nameof(Group.Id)}}} created successfully", group.Id);

        return group;
    }
}
