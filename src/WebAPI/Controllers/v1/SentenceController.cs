using Infrastructure.Exceptions;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Service.Commands.CreateOrUpdateSentence;
using Service.Commands.DeleteSentences;
using Service.Queries.GetSentences;
using WebAPI.Infrastructure.MVC;
using WebAPI.ViewModels;
using WebAPI.ViewModels.v1;

namespace WebAPI.Controllers;

/// <summary>
/// Предложения
/// </summary>
[ApiController]
[AuthorizeUser]
[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/[controller]")]
[ProducesResponseType(typeof(ValidationErrorViewModel), 400)]
[ProducesResponseType(401)]
[ProducesResponseType(500)]
public class SentenceController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly ILogger<SentenceController> _logger;

        public SentenceController(
        IMediator mediator,
        ILogger<SentenceController> logger)
    {
        _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    /// <summary>
    /// Создание или обновление предложений
    /// </summary>
    [HttpPost]
    [ProducesResponseType(typeof(ErrorResponseViewModel), 404)]
    public async Task<SentenceListViewModel> CreateOrUpdate(
        [FromBody] CreateOrUpdateSentenceListCommand? command,
        CancellationToken cancellationToken)
    {
        if (command is null)
            throw new ValidationErrorException("Specify request parameters");

        var sentenceDtoList = await _mediator.Send(command, cancellationToken);

        return new SentenceListViewModel(sentenceDtoList);
    }

    /// <summary>
    /// Создание или обновление предложений
    /// </summary>
    [HttpGet]
    [Route("all")]
    public async Task<SentencePageViewModel> GetSentences(
        [FromQuery] GetSentencesPageQuery? query,
        CancellationToken cancellationToken)
    {
        if (query is null)
            throw new ValidationErrorException("Specify request parameters");

        var sentences = await _mediator.Send(query, cancellationToken);

        return new SentencePageViewModel(sentences);
    }

     /// <summary>
    /// Удаление предложений
    /// </summary>
    [HttpDelete]
    [ProducesResponseType(typeof(ErrorResponseViewModel), 404)]
    public async Task DeleteSentence(
        [FromBody] DeleteSentenceListCommand? command,
        CancellationToken cancellationToken)
    {
        if (command is null)
            throw new ValidationErrorException("Specify request parameters");

        await _mediator.Send(command, cancellationToken);
    }
}
