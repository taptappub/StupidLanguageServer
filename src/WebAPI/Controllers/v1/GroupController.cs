using Infrastructure.Exceptions;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Service.Commands.CreateOrUpdateGroup;
using Service.Commands.DeleteGroup;
using Service.Queries.GetAllGroups;
using WebAPI.Infrastructure.MVC;
using WebAPI.ViewModels;
using WebAPI.ViewModels.v1;

namespace WebAPI.Controllers.v1;

/// <summary>
/// Группы
/// </summary>
[ApiController]
[AuthorizeUser]
[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/[controller]")]
[ProducesResponseType(typeof(ValidationErrorViewModel), 400)]
[ProducesResponseType(401)]
[ProducesResponseType(500)]
public class GroupController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly ILogger<UserController> _logger;

        public GroupController(
        IMediator mediator,
        ILogger<UserController> logger)
    {
        _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    /// <summary>
    /// Постраничное получение групп
    /// </summary>
    [HttpGet]
    [Route("all")]
    public async Task<GroupsPageViewModel> GetAll(
        [FromQuery] GetAllGroupsQuery? query,
        CancellationToken cancellationToken)
    {
        if (query is null) throw new ValidationErrorException("Specify request parameters");

        var groupDtoList = await _mediator.Send(query, cancellationToken);

        return new GroupsPageViewModel
        {
            Groups = groupDtoList!.Select(x => new GroupViewModel(x)).ToList(),
            LastId = groupDtoList!.Count > 0 ? groupDtoList[^1].Id : (long?)null
        };
    }

    /// <summary>
    /// Создание или обновление группы
    /// </summary>
    [HttpPost]
    public async Task<GroupViewModel> CreateOrUpdate(
        [FromBody] CreateOrUpdateGroupCommand? commnad,
        CancellationToken cancellationToken)
    {
        if (commnad is null) throw new ValidationErrorException("Specify request arguments");

        var groupDto = await _mediator.Send(commnad, cancellationToken);

        return new GroupViewModel(groupDto);
    }

    /// <summary>
    /// Удаление групп
    /// </summary>
    [HttpDelete]
    public async Task DeleteGroups(
        [FromBody] DeleteGroupListCommand command,
        CancellationToken cancellationToken)
    {
        if (command is null)
            throw new ValidationErrorException("Specify request parameters");

        await _mediator.Send(command, cancellationToken);
    }
}
