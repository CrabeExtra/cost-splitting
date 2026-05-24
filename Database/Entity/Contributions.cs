namespace Round_2.Database.Entity;

public class Contributions : IDbEntity
{
    public Guid Id { get; set; }
    public required string Name { get; set; } 
    public int ContributionCents { get; set; }
    public double Contribution => ContributionCents / 100.0;
    public required Guid PersonId { get; set; }
    public required Guid ExpenseId { get; set; }

    public People Person { get; set; } = null!;
    public Expenses Expense { get; set; } = null!;
    public List<Items> Items { get; set; } = [];
}