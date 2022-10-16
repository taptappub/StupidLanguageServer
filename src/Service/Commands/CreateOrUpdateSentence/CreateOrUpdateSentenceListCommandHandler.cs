using System.Text.Encodings.Web;
using System.Text.Json;
using DataAccess.EF;
using Domain.Entities;
using Infrastructure.Exceptions;
using MediatR;
using Microsoft.Extensions.Logging;
using Service.DomainExtensions;
using Service.Dto;

namespace Service.Commands.CreateOrUpdateSentence;

public class CreateOrUpdateSentenceListCommandHandler : IRequestHandler<CreateOrUpdateSentenceListCommand, List<SentenceDto>>
{
    private readonly IUnitOfWorkFactory _unitOfWorkFactory;
    private readonly ILogger<CreateOrUpdateSentenceListCommandHandler> _logger;
    private readonly CreateOrUpdateSentenceListCommandHandlerValidator _validator;

    public CreateOrUpdateSentenceListCommandHandler(
        IUnitOfWorkFactory unitOfWorkFactory,
        ILogger<CreateOrUpdateSentenceListCommandHandler> logger,
        CreateOrUpdateSentenceListCommandHandlerValidator validator)
    {
        _unitOfWorkFactory = unitOfWorkFactory ?? throw new ArgumentNullException(nameof(unitOfWorkFactory));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        _validator = validator ?? throw new ArgumentNullException(nameof(validator));
    }

    public async Task<List<SentenceDto>> Handle(CreateOrUpdateSentenceListCommand request, CancellationToken cancellationToken)
    {
        request.Validate(_validator, _logger);
        _logger.LogInformation("Start handling");
        try
        {
            var createSentenceCmds = request.Sentences.Where(x => x.ExternalId is null).ToList();
            var updateSentenceCmds = request.Sentences.Where(x => x.ExternalId is not null).ToList();

            await using var uow = _unitOfWorkFactory.Create();

            var user = await uow.GetAuthorizedUser(_logger, cancellationToken);
            var wordByExternalId = await GetWordByExternalId(request.Sentences, uow.Repositories, cancellationToken);

            var newSentences = CreateSentences(createSentenceCmds, wordByExternalId, user);
            var updatedSentences = await UpdateSentences(updateSentenceCmds, wordByExternalId, uow.Repositories, cancellationToken);

            uow.Repositories.Sentence.AddRange(newSentences);
            await uow.SaveChangesAsync(cancellationToken);

            return newSentences.Union(updatedSentences)
                .Select(x => x.ToDto())
                .ToList();
        }
        finally
        {
            _logger.LogInformation("Handling completed");
        }
    }

    private async Task<Dictionary<Guid, Word>> GetWordByExternalId(
        IReadOnlyCollection<CreateOrUpdateSentenceCommand> requests,
        IRepositoryFactory repositoryFactory,
        CancellationToken cancellationToken)
    {
        _logger.LogTrace("Start method {MethodName}", nameof(GetWordByExternalId));

        var wordExternalIds = requests
            .SelectMany(x => x.WordExternalIds)
            .ToHashSet();

        var words = await repositoryFactory.Word.GetWordsByExternalIds(wordExternalIds, cancellationToken);
        if (words.Count != wordExternalIds.Count)
        {
            var existsWordIds = words.Select(x => x.ExternalId).ToHashSet();
            var missingWordIds = wordExternalIds.Where(x => !existsWordIds.Contains(x));
            var message = $"Some {nameof(Word)} not found.";

            _logger.LogWarning(message);
            var ex = new ObjectNotFoundException(message);
            ex.Data.Add($"{nameof(Word)}.{nameof(Word.ExternalId)}", JsonSerializer.Serialize(missingWordIds, new JsonSerializerOptions()
            {
                WriteIndented = true
            }));
            throw ex;
        }
        var wordByExternalId = words.ToDictionary(x => x.ExternalId);

        _logger.LogTrace("Finish method {MethodName}", nameof(GetWordByExternalId));
        return wordByExternalId;
    }

    private List<Sentence> CreateSentences(
        IReadOnlyCollection<CreateOrUpdateSentenceCommand> requests,
        IReadOnlyDictionary<Guid, Word> wordByExternalId,
        User user)
    {
        _logger.LogTrace("Start method {MethodName}", nameof(CreateSentences));

        var result = new List<Sentence>(requests.Count);
        foreach (var request in requests)
        {
            var words = request.WordExternalIds.Select(w => wordByExternalId[w]);
            var sentence = new Sentence(user, request.Value, words);

            result.Add(sentence);
        }

        _logger.LogTrace("Finish method {MethodName}", nameof(CreateSentences));
        return result;
    }

    private async Task<List<Sentence>> UpdateSentences(
        IReadOnlyCollection<CreateOrUpdateSentenceCommand> requests,
        IReadOnlyDictionary<Guid, Word> wordByExternalId,
        IRepositoryFactory repositoryFactory,
        CancellationToken cancellationToken
    )
    {
        _logger.LogTrace("Start method {MethodName}", nameof(UpdateSentences));

        var cmdById = requests
            .GroupBy(x => x.ExternalId!.Value)
            .ToDictionary(g => g.Key, g => g.First());
        var sentences = await repositoryFactory.Sentence.GetSentencesByExternalIds(cmdById.Keys, cancellationToken);
        if (sentences.Count != cmdById.Count)
        {
            var existsSentenceIds = sentences.Select(x => x.ExternalId).ToHashSet();
            var missingSentences = cmdById.Keys.Where(x => !existsSentenceIds.Contains(x)).ToList();
            var message = $"Some {nameof(Sentence)} not found";

            _logger.LogWarning(message);
            var ex = new ObjectNotFoundException(message);
            ex.Data.Add($"{nameof(Sentence)}.{nameof(Sentence.ExternalId)}", JsonSerializer.Serialize(missingSentences));
            throw ex;
        }

        foreach (var sentence in sentences)
        {
            var cmd = cmdById[sentence.ExternalId];
            var words = cmd.WordExternalIds.Select(x => wordByExternalId[x]);

            sentence.RemoveWords();
            sentence.Value = cmd.Value;
            sentence.AddWords(words);
        }

        _logger.LogTrace("Finish method {MethodName}", nameof(UpdateSentences));
        return sentences;
    }
}
