

namespace Round_2.Application.DTOs;

public class AddContributionDto
{
    public Guid? Id { get; set; }
    public string? Name { get; set; } 
    public Guid? PersonId { get; set; }
    public Guid? ExpenseId { get; set; }
    public int? TargetCostCents { get; set; }
    public int? Weight { get; set; } 
    public double? Percentage { get; set; }
}

public class ContributionDto
{
    public Guid Id { get; set; }
    public string Name { get; set; } = "";
    public double Contribution { get; set; }
    public Guid PersonId { get; set; }
    public Guid ExpenseId { get; set; }
    public string? PersonName { get; set; }
    public string? ExpenseName { get; set; }
    public int? TargetCostCents { get; set; }
    public int? Weight { get; set; } 
    public double? Percentage { get; set; }
}