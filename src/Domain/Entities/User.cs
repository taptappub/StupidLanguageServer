namespace Domain.Entities;

public class User
{
    /// <summary>
    /// ID пользователя
    /// </summary>
    public long Id { get; init; } = default!;

    /// <summary>
    /// Внешний ID пользователя
    /// </summary>
    public string ExternalId => _externalId;
    private string _externalId;

    /// <summary>
    /// Имя пользователя
    /// </summary>
    public string Name
    {
        get => _name;
        set
        {
            if (string.IsNullOrEmpty(value)) throw new ArgumentException(nameof(Name));
            _name = value;
        }
    }
    private string _name = string.Empty;

    /// <summary>
    /// Ссылка на аватар пользователя
    /// </summary>
    public string? AvatarUrl { get; set; }

    /// <summary>
    /// Ctor
    /// </summary>
    /// <param name="externalId">Внешний ID пользователя</param>
    /// <param name="name">Имя пользователя</param>
    /// <param name="avatarUrl">Ссылка на аватар пользователя</param>
    public User(string externalId, string name, string? avatarUrl = null)
    {
        if (string.IsNullOrEmpty(externalId)) throw new ArgumentException(nameof(externalId));

        _externalId = externalId;
        Name = name;
        AvatarUrl = avatarUrl;
    }

    /// <summary>
    /// Ctor for ORM
    /// </summary>
    protected User()
    {
        _externalId = default!;
    }
}
