namespace Domain.Entities;

public class SentenceWord
{
    /// <summary>
    /// ID
    /// </summary>
    public long Id { get; }

    /// <summary>
    /// Предложение
    /// </summary>
    public virtual Sentence Sentence { get; }

    /// <summary>
    /// Слово
    /// </summary>
    public virtual Word Word { get; }

    /// <summary>
    /// Сортировочный номер
    /// </summary>
    public int OrderedNumber { get; }

    public SentenceWord(Sentence sentence, Word word, int orderNumber)
    {
        if (sentence is null) throw new ArgumentNullException(nameof(sentence));
        if (word is null) throw new ArgumentNullException(nameof(word));
        if (orderNumber < 0) throw new ArgumentOutOfRangeException(nameof(orderNumber));

        if (sentence.User.Id != word.User.Id) throw new InvalidOperationException("Sentence and word must be owned by one user");

        Sentence = sentence;
        Word = word;
        OrderedNumber = orderNumber;
    }

    /// <summary>
    /// Ctor for ORM
    /// </summary>
    protected SentenceWord()
    {
        Word = default!;
        Sentence = default!;
    }
}
