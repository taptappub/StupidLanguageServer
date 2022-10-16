namespace Service.Dto;

public class SentenceDto
{
    public long Id { get; set; }
    public Guid ExternalId { get; set; }
    public string Value { get; set; } = string.Empty;
    public List<Guid> WordExternalIds { get; set; } = new List<Guid>(0);
}
