namespace Round_2.Database.Entity;

public class People : IDbEntity
{
    public Guid Id { get; set; }
    public required string Name { get; set; }

    public List<Contributions> Contributions { get; set; } = [];
}