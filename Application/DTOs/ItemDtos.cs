namespace Round_2.Application.DTOs;

public class AddItemDto
{
    public Guid? Id { get; set; }
    public string? Name { get; set; }
    public Guid? ContributionId { get; set; }
}

public class ItemDto
{
    public Guid Id { get; set; }
    public required string Name { get; set; }
    public required Guid ContributionId { get; set; }
}