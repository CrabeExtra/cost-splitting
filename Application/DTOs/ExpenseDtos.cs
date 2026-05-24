using Round_2.Database.Entity;

namespace Round_2.Application.DTOs;

public class AddExpenseDto
{
    public Guid? Id { get; set; }
    public string? Name { get; set; }
    public int? Cost { get; set; }
    public string? Type { get; set; }
}

public class ExpenseDto
{
    public Guid Id { get; set; }
    public string Name { get; set; } = "";
    public double Cost { get; set; }
    public ExpenseType Type { get; set; }
}