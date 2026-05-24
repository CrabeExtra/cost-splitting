namespace Round_2.Database.Entity;

public class Items : IDbEntity
{
    public Guid Id { get; set; }
    public required string Name { get; set; }
    public required Guid ContributionId { get; set; }
    public Contributions Contribution { get; set; } = null!;
}