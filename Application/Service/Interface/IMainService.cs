using Round_2.Application.DTOs;

namespace Round_2.Application.Service.Interface;

/// <summary>
/// Service for anything user related.
/// </summary>
public interface IMainService
{
    Task<Guid> AddPerson(AddPersonDto dto, CancellationToken ct);
    Task<IEnumerable<PersonDto>> GetPeople(CancellationToken ct);
    Task<PersonDto> GetPerson(Guid id, CancellationToken ct);
    Task<IEnumerable<PersonDto>> GetPersonByName(string name, CancellationToken ct);
    Task<Guid?> DeletePerson(Guid id, CancellationToken ct);

    Task<Guid> AddExpense(AddExpenseDto dto, CancellationToken ct);
    Task<ExpenseDto> GetExpense(Guid id, CancellationToken ct);
    Task<IEnumerable<ExpenseDto>> GetExpenses(CancellationToken ct);
    Task<Guid?> DeleteExpense(Guid id, CancellationToken ct);

    Task<Guid> AddItem(AddItemDto dto, CancellationToken ct);
    Task<ItemDto> GetItem(string id, CancellationToken ct);
    Task<IEnumerable<ItemDto>> GetItems(string contributionId, CancellationToken ct);
    Task<Guid?> DeleteItem(Guid id, CancellationToken ct);

    Task<Guid> AddContribution(AddContributionDto dto, CancellationToken ct);
    Task<ContributionDto> GetContribution(Guid id, CancellationToken ct);
    Task<IEnumerable<ContributionDto>> GetContributions(CancellationToken ct);
    Task<Guid?> DeleteContribution(Guid id, CancellationToken ct);

}