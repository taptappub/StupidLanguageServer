namespace Service.Dto;

public class WordDto
{
    public long Id { get; set; }

    public Guid ExternalId { get; set; }

    public string Value{ get; set; } = string.Empty;

    public int RepetitionProgress{ get; set; }

    public string? Description { get; set; }

    public string? ImageUrl { get; set; }

    public Guid GroupId { get; set; }
}
