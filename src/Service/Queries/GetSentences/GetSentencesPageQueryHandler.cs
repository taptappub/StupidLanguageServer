using DataAccess.EF;
using MediatR;
using Microsoft.Extensions.Logging;
using Service.DomainExtensions;
using Service.Dto;

namespace Service.Queries.GetSentences;

public class GetSentencesPageQueryHandler : IRequestHandler<GetSentencesPageQuery, List<SentenceDto>>
{
    private readonly IUnitOfWorkFactory _unitOfWorkFactory;
    private readonly ILogger<GetSentencesPageQueryHandler> _logger;

    public GetSentencesPageQueryHandler(
        IUnitOfWorkFactory unitOfWorkFactory,
        ILogger<GetSentencesPageQueryHandler> logger)
    {
        _unitOfWorkFactory = unitOfWorkFactory ?? throw new ArgumentNullException(nameof(unitOfWorkFactory));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    public async Task<List<SentenceDto>> Handle(GetSentencesPageQuery request, CancellationToken cancellationToken)
    {
        if (request is null) throw new ArgumentNullException(nameof(request));

        _logger.LogInformation("Start handling");
        try
        {
            await using var uow = _unitOfWorkFactory.CreateReadOnly();
            var sentences = await uow.Repositories.Sentence
                .GetSentencePage(request.LastId, request.Limit, cancellationToken);

            return sentences.Select(x => x.ToDto()).ToList();
        }
        finally
        {
            _logger.LogInformation("Handling completed");
        }
    }
}
