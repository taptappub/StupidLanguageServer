namespace Service.Dto;

public class GroupDto
{
    public long Id { get; set; }
    
    public Guid ExternalId { get; set;}

    public string? Name { get; set; }

    public int RepetitionProgress { get; set; }
}
