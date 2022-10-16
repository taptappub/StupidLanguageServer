namespace Domain.Entities;

public class Sentence : ISoftDelete, IOwnedEntity
{
    /// <summary>
    /// ID предоложения
    /// </summary>
    public long Id { get; }

    /// <summary>
    /// Внешний ID предложения
    /// </summary>
    public Guid ExternalId { get; }

    /// <summary>
    /// Предложение
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

    /// <summary>
    /// Признак удаления
    /// </summary>
    public bool IsDeleted { get; set; }

    /// <summary>
    /// Пользователь, владелец предложения
    /// </summary>
    public virtual User User { get; }


    /// <summary>
    /// Слова предложения
    /// </summary>
    public virtual ICollection<SentenceWord> Words { get; protected set; } = new List<SentenceWord>(0);
    public virtual IEnumerable<SentenceWord> OrderedWords => Words.OrderBy(x => x.OrderedNumber);

    /// <summary>
    /// Ctor
    /// </summary>
    /// <param name="user">Пользователь, владелец предложения</param>
    /// <param name="value">Предложение</param>
    /// <param name="words">Слова предложения</param>
    public Sentence(User user, string value, IEnumerable<Word>? words = null)
    {
        if (user is null) throw new ArgumentNullException(nameof(user));

        User = user;
        Value = value;

        if (words is not null)
        {
            AddWords(words);
        }
    }

    /// <summary>
    /// Ctor for ORM
    /// </summary>
    protected Sentence()
    {
        User = default!;
    }

    public void AddWords(IEnumerable<Word> words)
    {
        var orderNumber = Words.Count > 0
            ? Words.Max(x => x.OrderedNumber)
            : 0;
        foreach (var word in words)
        {
            if (word is null) throw new ArgumentNullException(nameof(word));

            Words.Add(new SentenceWord(this, word, ++orderNumber));
        }
    }

    public void RemoveWords()
    {
        Words.Clear();
    }
}
