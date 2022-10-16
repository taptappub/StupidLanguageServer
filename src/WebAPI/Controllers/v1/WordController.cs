using Infrastructure.Exceptions;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Service.Commands.CreateOrUpdateWords;
using Service.Commands.DeleteWords;
using WebAPI.Infrastructure.MVC;
using WebAPI.ViewModels;
using WebAPI.ViewModels.v1;

namespace WebAPI.Controllers.v1;

/// <summary>
/// Слова
/// </summary>
[ApiController]
[AuthorizeUser]
[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/[controller]")]
[ProducesResponseType(typeof(ValidationErrorViewModel), 400)]
[ProducesResponseType(401)]
[ProducesResponseType(500)]
public class WordController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly ILogger<WordController> _logger;

        public WordController(
        IMediator mediator,
        ILogger<WordController> logger)
    {
        _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    /// <summary>
    /// Постраничное получение слов из группы
    /// </summary>
    [HttpGet]
    [Route("group/{groupId}/all")]
    [ProducesResponseType(typeof(ErrorResponseViewModel), 404)]
    public async Task<WordPageViewModel> GetWords(
        Guid groupId,
        [FromQuery] GetGroupWordsQueryViewModel queryViewModel,
        CancellationToken cancellationToken)
    {
        if (queryViewModel is null)
            throw new ValidationErrorException("Specify request parameters");

        var wordDtoList = await _mediator.Send(queryViewModel.ToQuery(groupId), cancellationToken);

        return new WordPageViewModel(wordDtoList);
    }

    /// <summary>
    /// Создание или обновление слов
    /// </summary>
    [HttpPost]
    [ProducesResponseType(typeof(ErrorResponseViewModel), 404)]
    public async Task<WordListViewModel> CreateOrUpdateWords(
        [FromBody] CreateOrUpdateWordListCommnad command,
        CancellationToken cancellationToken)
    {
        if (command is null)
            throw new ValidationErrorException("Specify request parameters");

        var wordDtoList = await _mediator.Send(command, cancellationToken);

        return new WordListViewModel
        {
            Words = wordDtoList!.Select(x => new WordViewModel(x)).ToList()
        };
    }

    /// <summary>
    /// Удаление слов
    /// </summary>
    [HttpDelete]
    public async Task DeleteWords(
        [FromBody] DeleteWordListCommand command,
        CancellationToken cancellationToken)
    {
        if (command is null)
            throw new ValidationErrorException("Specify request parameters");

        await _mediator.Send(command, cancellationToken);
    }
}
