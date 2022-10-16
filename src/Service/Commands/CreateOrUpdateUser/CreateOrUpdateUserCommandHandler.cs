using DataAccess.EF;
using Domain.Entities;
using Infrastructure.Exceptions;
using Infrastructure.Logging;
using MediatR;
using Microsoft.Extensions.Logging;
using Service.DomainExtensions;
using Service.Dto;

namespace Service.Commands.CreateOrUpdateUser;

public class CreateOrUpdateUserCommandHandler : IRequestHandler<CreateOrUpdateUserCommand, UserDto>
{
    private readonly IUnitOfWorkFactory _unitOfWorkFactory;
    private readonly ILogger<CreateOrUpdateUserCommandHandler> _logger;
    private readonly CreateOrUpdateUserCommandValidator _validator;

    public CreateOrUpdateUserCommandHandler(
        IUnitOfWorkFactory unitOfWorkFactory,
        ILogger<CreateOrUpdateUserCommandHandler> logger,
        CreateOrUpdateUserCommandValidator validator)
    {
        _unitOfWorkFactory = unitOfWorkFactory ?? throw new ArgumentNullException(nameof(unitOfWorkFactory));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        _validator = validator ?? throw new ArgumentNullException(nameof(validator));
    }

    public async Task<UserDto> Handle(CreateOrUpdateUserCommand request, CancellationToken cancellationToken)
    {
        if (request is null) throw new ValidationErrorException("Specify command arguments");

        request.Validate(_validator, _logger);
        using var _ = _logger.SetProperty($"{nameof(User)}.{nameof(User.ExternalId)}", request.ExternalId);
        _logger.LogInformation("Start handling");
        try
        {
            await using var uow = _unitOfWorkFactory.Create();
            var userRepository = uow.Repositories.User;

            var user = await userRepository.GetByExternalId(request.ExternalId, cancellationToken);
            if (user is null)
            {
                _logger.LogInformation("Creating new user");

                user = new User(request.ExternalId, request.Name, request.ExternalId);
                userRepository.Add(user);

                await uow.SaveChangesAsync(cancellationToken);
                using (_logger.SetProperty($"{nameof(User)}.{nameof(User.Id)}", user.Id))
                {
                    _logger.LogInformation($"New user created successfully");
                }
            }
            else
            {
                using var __ = _logger.SetProperty($"{nameof(User)}.{nameof(User.Id)}", user.Id);
                
                _logger.LogInformation("Updating user");

                user.AvatarUrl = request.AvatarUrl;
                user.Name = request.Name;

                await uow.SaveChangesAsync(cancellationToken);
                _logger.LogInformation("User updated successfully", user.Id);
            }

            return user.ToDto();
        }
        finally
        {
            _logger.LogInformation("Handling completed");
        }
    }
}
