
using Round_2.Application.Service.Interface;
using Microsoft.AspNetCore.Mvc;
using Round_2.Application.DTOs;

namespace Round_2.Api.Controller;

[ApiController]
[Route("api/")]
public class Controller(
    IMainService service
) : ControllerBase
{
    [HttpPost("people")]
    public async Task<IActionResult> AddPerson([FromBody] AddPersonDto dto, CancellationToken ct)
    {
        var userId = await service.AddPerson(dto, ct);

        return CreatedAtAction(
            nameof(AddPerson),
            new { id = userId },
            new { id = userId }
        );
    }

    [HttpGet("people")]
    public async Task<IActionResult> GetPeople(CancellationToken ct)
    {
        var people = await service.GetPeople(ct);

        return Ok(people);
    }

    [HttpGet("people/{id}")]
    public async Task<IActionResult> GetPerson(string id, CancellationToken ct)
    {
        var person = await service.GetPerson(Guid.Parse(id), ct);

        return Ok(person);
    }

    [HttpGet("people/name/{name}")]
    public async Task<IActionResult> GetPersonByName(string name, CancellationToken ct)
    {
        var people = await service.GetPersonByName(name, ct);
        

        return Ok(people);
    }

    [HttpDelete("people/{id}")]
    public async Task<IActionResult> DeletePerson(string id, CancellationToken ct)
    {
        Console.WriteLine($"Deleting person with id {id}");
        var personId = await service.DeletePerson(Guid.Parse(id), ct);

        return Ok(personId);
    }

    [HttpPost("expenses")]
    public async Task<IActionResult> AddExpense([FromBody] AddExpenseDto dto, CancellationToken ct)
    {
        var expenseId = await service.AddExpense(dto, ct);

        return CreatedAtAction(
            nameof(AddExpense),
            new { id = expenseId },
            new { id = expenseId }
        );
    }

    [HttpGet("expenses/{id}")]
    public async Task<IActionResult> GetExpense(string id, CancellationToken ct)
    {
        var expense = await service.GetExpense(Guid.Parse(id), ct);

        return Ok(expense);
    }

    [HttpGet("expenses")]
    public async Task<IActionResult> GetExpenses(CancellationToken ct)
    {
        var expenses = await service.GetExpenses(ct);

        return Ok(expenses);
    }

    [HttpDelete("expenses/{id}")]
    public async Task<IActionResult> DeleteExpense(string id, CancellationToken ct)
    {
        var expenseId = await service.DeleteExpense(Guid.Parse(id), ct);

        return Ok(expenseId);
    }

    [HttpPost("items")]
    public async Task<IActionResult> AddItem([FromBody] AddItemDto dto, CancellationToken ct)
    {
        var itemId = await service.AddItem(dto, ct);

        return CreatedAtAction(
            nameof(AddItem),
            new { id = itemId },
            new { id = itemId }
        );
    }

    [HttpGet("items")]
    public async Task<IActionResult> GetItems(string contributionId, CancellationToken ct)
    {
        var items = await service.GetItems(contributionId, ct);

        return Ok(items);
    }

    [HttpGet("items/{id}")]
    public async Task<IActionResult> GetItem(string id, CancellationToken ct)
    {
        var item = await service.GetItem(id, ct);

        return Ok(item);
    }

    [HttpDelete("items/{id}")]
    public async Task<IActionResult> DeleteItem(string id, CancellationToken ct)
    {
        var itemId = await service.DeleteItem(Guid.Parse(id), ct);

        return Ok(itemId);
    }

    [HttpPost("contributions")]
    public async Task<IActionResult> AddContribution([FromBody] AddContributionDto dto, CancellationToken ct)
    {
        var expenseId = await service.AddContribution(dto, ct);

        return CreatedAtAction(
            nameof(AddContribution),
            new { id = expenseId },
            new { id = expenseId }
        );
    }

    [HttpGet("contributions/{id}")]
    public async Task<IActionResult> GetContribution(string id, CancellationToken ct)
    {
        var expense = await service.GetContribution(Guid.Parse(id), ct);

        return Ok(expense);
    }

    [HttpGet("contributions/expense/{expenseId}")]
    public async Task<IActionResult> GetContributionByExpense(string expenseId, CancellationToken ct)
    {
        var expenses = await service.GetContributionsByExpense(Guid.Parse(expenseId), ct);
         
        return Ok(expenses);
    }

    [HttpGet("contributions")]
    public async Task<IActionResult> GetContributions(CancellationToken ct)
    {
        var expenses = await service.GetContributions(ct);

        return Ok(expenses);
    }

    [HttpDelete("contributions/{id}")]
    public async Task<IActionResult> DeleteContribution(string id, CancellationToken ct)
    {
        var expenseId = await service.DeleteContribution(Guid.Parse(id), ct);

        return Ok(expenseId);
    }
}
// - Add person.
//     - update if ID uncluded.
// - Get people.
// - Get person by Id.
// - Gets expenses.
// - Get expense by Id.
// - Add Expense.
// - Gets Items.
// - Add item
// - Add contribution
// - Get contributions