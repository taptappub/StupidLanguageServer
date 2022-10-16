namespace Infrastructure.Authorization;

public class AuthorizedUser
{
    /// <summary>
    /// Внешний идентификатор
    /// </summary>
    public string ExternalId { get; }

    /// <summary>
    /// Идентификатор
    /// </summary>
    public long Id { get; }

    /// <summary>
    /// Анонимный пользователь
    /// </summary>
    public static AuthorizedUser Anonymous { get; } = new AuthorizedUser();

    public AuthorizedUser(long id, string externalId)
    {
        if (id <= 0) throw new ArgumentException(nameof(id));
        if (string.IsNullOrEmpty(externalId)) throw new ArgumentException(nameof(externalId));

        Id = id;
        ExternalId = externalId;
    }

    private AuthorizedUser()
    {
        ExternalId = string.Empty;
        Id = default;
    }


}
