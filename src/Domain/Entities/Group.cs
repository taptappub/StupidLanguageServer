namespace Domain.Entities;

public class Group : IOwnedEntity, ISoftDelete
{
    /// <summary>
    /// ID группы
    /// </summary>
    public long Id { get; }

    /// <summary>
    /// Внешний ID группы
    /// </summary>
    public Guid ExternalId { get; }

    /// <summary>
    /// Название группы
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

    public int RepetitionProgress
    {
        get => _repetitionProgress;
        set
        {
            if (value < 0) throw new ArgumentOutOfRangeException(nameof(RepetitionProgress));
            _repetitionProgress = value;
        }
    }
    private int _repetitionProgress;

    /// <summary>
    /// Признак удаления
    /// </summary>
    public bool IsDeleted { get; set; }

    /// <summary>
    /// Пользователь, владелец группы
    /// </summary>
    public virtual User User { get; } = default!;

    /// <summary>
    /// Ctor
    /// </summary>
    /// <param name="user">Пользователь, владелец группы</param>
    /// <param name="name">Название группы</param>
    public Group(User user, string name)
    {
        if (user is null) throw new ArgumentNullException(nameof(user));
        if (string.IsNullOrEmpty(name)) throw new ArgumentException(nameof(name));

        User = user;
        Name = name;
    }

    /// <summary>
    /// Ctor for ORM
    /// </summary>
    protected Group()
    {
    }
}
