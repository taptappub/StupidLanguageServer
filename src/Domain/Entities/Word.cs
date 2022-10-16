namespace Domain.Entities;

public class Word : IOwnedEntity, ISoftDelete
{
    /// <summary>
    /// ID слова
    /// </summary>
    public long Id { get; }

    /// <summary>
    /// Внешний ID слова
    /// </summary>
    public Guid ExternalId { get; }

    /// <summary>
    /// Значение слова
    /// </summary>
    public string Value
    {
        get => _value;
        set
        {
            if (string.IsNullOrEmpty(value)) throw new ArgumentException(nameof(Word));
            if (value != _value)
            {
                _value = value;
            }
        }
    }
    private string _value = string.Empty;

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
    /// Описание
    /// </summary>
    public string? Description { get; set; }

    /// <summary>
    /// Ссылка на изображение
    /// </summary>
    public string? ImageUrl { get; set; }

    /// <summary>
    /// Группа, к которой относится слово
    /// </summary>
    public virtual Group Group
    {
        get => _group;
        set
        {
            if (value is null) throw new ArgumentNullException(nameof(Group));
            _group = value;
        }
    }
    private Group _group = default!;

    /// <summary>
    /// Признак удаления
    /// </summary>
    public bool IsDeleted { get; set; }

    /// <summary>
    /// Пользователь, владелец слова
    /// </summary>
    public virtual User User { get; }

    /// <summary>
    /// Ctor
    /// </summary>
    /// <param name="user">Пользователь, владелец слова</param>
    /// <param name="group">Группа, к которой относится слово</param>
    /// <param name="value">Значение слова</param>
    public Word(User user, Group group, string value)
    {
        if (user is null) throw new ArgumentNullException(nameof(user));
        if (group is null) throw new ArgumentNullException(nameof(group));
        if (user.Id != group.User.Id) throw new InvalidOperationException("Word and group must be owned by one user");

        User = user;
        Group = group;
        Value = value;
    }

    /// <summary>
    /// Ctor for ORM
    /// </summary>
    protected Word()
    {
        User = default!;
    }
}
