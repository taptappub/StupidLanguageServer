using System.Text.Json;
using DataAccess.EF;
using Domain.Entities;
using Infrastructure.Authorization;
using Infrastructure.Exceptions;
using MediatR;
using Microsoft.Extensions.Logging;
using Service.DomainExtensions;
using Service.Dto;

namespace Service.Commands.CreateOrUpdateWords;

public class CreateOrUpdateWordListCommnadHandler : IRequestHandler<CreateOrUpdateWordListCommnad, List<WordDto>>
{
    private readonly IUnitOfWorkFactory _unitOfWorkFactory;
    private readonly ILogger<CreateOrUpdateWordListCommnadHandler> _logger;
    private readonly CreateOrUpdateWordListCommandValidator _validator;

    public CreateOrUpdateWordListCommnadHandler(
        IUnitOfWorkFactory unitOfWorkFactory,
        ILogger<CreateOrUpdateWordListCommnadHandler> logger,
        CreateOrUpdateWordListCommandValidator validator)
    {
        _unitOfWorkFactory = unitOfWorkFactory ?? throw new ArgumentNullException(nameof(unitOfWorkFactory));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        _validator = validator ?? throw new ArgumentNullException(nameof(validator));
    }

    public async Task<List<WordDto>> Handle(CreateOrUpdateWordListCommnad request, CancellationToken cancellationToken)
    {
        request.Validate(_validator, _logger);
        _logger.LogInformation("Start handling");
        try
        {
            await using var uow = _unitOfWorkFactory.Create();

            var user = await uow.GetAuthorizedUser(_logger, cancellationToken);
            var groups = await LoadGroups(uow.Repositories, request.Words, cancellationToken);
            var groupByExternalIds = groups.ToDictionary(x => x.ExternalId);

            var createWordCmds = request.Words
                .Where(x => x.ExternalId is null)
                .ToList();
            var updateWordCmds = request.Words
                .Where(x => x.ExternalId is not null)
                .ToList();

            var newWords = CreateWords(createWordCmds, groupByExternalIds, user);
            uow.Repositories.Word.AddRange(newWords);
            var existsWords = await UpdateWords(uow.Repositories, updateWordCmds, groupByExternalIds, cancellationToken);

            await uow.SaveChangesAsync(cancellationToken);

            var result = newWords.Union(existsWords)
                .Select(w => w.ToDto())
                .ToList();

            return result;
        }
        finally
        {
            _logger.LogInformation("Handling completed");
        }
    }

    private async Task<List<Group>> LoadGroups(
        IRepositoryFactory repositoryFactory,
        ICollection<CreateOrUpdateWordCommnad> words,
        CancellationToken cancellationToken)
    {
        _logger.LogTrace("Start method {MethodName}", nameof(LoadGroups));
        var groupExternalIds = words
                    .Select(x => x.GroupId)
                    .Distinct()
                    .ToList();
        var groups = await repositoryFactory.Group.GetByExternalIds(groupExternalIds, cancellationToken);

        if (groups.Count != groupExternalIds.Count)
        {
            var missingGroupExternalIds = groupExternalIds.Except(groups.Select(x => x.ExternalId));
            var message = $"Some {nameof(Group)} not found";

            _logger.LogWarning(message);
            var ex = new ObjectNotFoundException(message);
            ex.Data.Add($"{nameof(Group)}.{nameof(Group.ExternalId)}", JsonSerializer.Serialize(missingGroupExternalIds));
            throw ex;
        }

        _logger.LogTrace("Finish method {MethodName}", nameof(LoadGroups));
        return groups;
    }

    private async Task<List<Word>> UpdateWords(
        IRepositoryFactory repositoryFactory,
        ICollection<CreateOrUpdateWordCommnad> wordCmds,
        IDictionary<Guid, Group> groupByExternalId,
        CancellationToken cancellationToken)
    {
        _logger.LogTrace("Start method {MethodName}", nameof(UpdateWords));

        var wordByExternalId = wordCmds.ToDictionary(x => x.ExternalId!.Value);
        var wordEntities = await repositoryFactory.Word.GetWordsByExternalIds(wordByExternalId.Keys, cancellationToken);

        if (wordEntities.Count != wordCmds.Count)
        {
            var existsWordIds = wordEntities.Select(x => x.ExternalId).ToHashSet();
            var missingExternalIds = wordByExternalId.Keys.Where(x => !existsWordIds.Contains(x));
            var message = $"Some {nameof(Word)} not found";

            _logger.LogWarning(message);
            var ex = new ObjectNotFoundException(message);
            ex.Data.Add($"{nameof(Word)}.{nameof(Word.ExternalId)}", JsonSerializer.Serialize(missingExternalIds));
            throw ex;
        }

        foreach (var e in wordEntities)
        {
            var word = wordByExternalId[e.ExternalId];

            e.Description = word.Description;
            e.Group = groupByExternalId[word.GroupId];
            e.ImageUrl = word.ImageUrl;
            e.RepetitionProgress = word.RepetitionProgress;
        }

        _logger.LogTrace("Finish method {MethodName}", nameof(CreateWords));
        return wordEntities;
    }

    private List<Word> CreateWords(
        ICollection<CreateOrUpdateWordCommnad> wordCmds,
        IDictionary<Guid, Group> groupByExternalId,
        User user)
    {
        _logger.LogTrace("Start method {MethodName}", nameof(CreateWords));

        var result = wordCmds
            .Select(w => new Word(user, groupByExternalId[w.GroupId], w.Value)
            {
                Description = w.Description,
                ImageUrl = w.ImageUrl,
                RepetitionProgress = w.RepetitionProgress
            })
            .ToList();

        _logger.LogTrace("Finish method {MethodName}", nameof(CreateWords));
        return result;
    }
}
