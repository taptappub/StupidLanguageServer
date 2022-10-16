using MediatR;
using Service.Dto;

namespace Service.Queries.GetUserByExternalId;

/// <summary>
/// Получение пользователя по идентификатору
/// </summary>
public class GetUserByExternalIdQuery : IRequest<UserDto>
{
    /// <summary>
    /// Идентифкатор пользователя
    /// </summary>
    public string ExternalId { get; }

    public GetUserByExternalIdQuery(string externalId)
    {
        if (string.IsNullOrEmpty(externalId)) throw new ArgumentException(nameof(externalId));
        
        ExternalId = externalId;
    }
}
