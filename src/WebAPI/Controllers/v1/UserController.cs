using Infrastructure.Exceptions;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Service.Commands;
using Service.Commands.CreateOrUpdateUser;
using WebAPI.ViewModels;
using WebAPI.ViewModels.v1;

namespace WebAPI.Controllers.v1;

/// <summary>
/// Пользователи
/// </summary>
[ApiController]
[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/[controller]")]
[ProducesResponseType(typeof(ValidationErrorViewModel), 400)]
[ProducesResponseType(500)]
public class UserController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly ILogger<UserController> _logger;
        public UserController(IMediator mediator, ILogger<UserController> logger)
    {
        _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    /// <summary>
    /// Создание или обновление пользователя
    /// </summary>
    [HttpPost]
    [ProducesResponseType(typeof(UserViewModel), 200)]
    public async Task<UserViewModel> CreateOrUpdate([FromBody] CreateOrUpdateUserCommand command)
    {
        if (command is null) throw new ValidationErrorException("Specify request arguments");

        var userDto = await _mediator.Send(command);

        return new UserViewModel(userDto);
    }
}
