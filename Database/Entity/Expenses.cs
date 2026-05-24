namespace Round_2.Database.Entity;

public enum ExpenseType
{
    Shared,
    Consolidated,
    Weighted,
    Percentage,
    Item
}

public class Expenses : IDbEntity {
    public Guid Id { get; set; }
    public required string Name { get; set; }
    public int CostCents { get; set; }
    public ExpenseType Type { get; set; }

    public List<Contributions> Contributions { get; set; } = [];

    public double Cost => CostCents / 100.0;
}