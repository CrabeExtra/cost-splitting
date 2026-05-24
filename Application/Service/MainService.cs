using Round_2.Application.DTOs;
using Round_2.Application.Service.Interface;
using Round_2.Database.Repository.Interface;
using Round_2.Database.Entity;
using Round_2.Application.Exceptions;

namespace Round_2.Application.Service;

public class MainService(
    IPeopleRepository peopleRepository,
    IExpenseRepository expenseRepository,
    IContributionRepository contributionRepository,
    IItemRepository itemRepository
) : IMainService
{
    public async Task<Guid> AddPerson(AddPersonDto dto, CancellationToken ct)
    {
        People existingPerson = null;

        if (dto.Id != null)
            existingPerson = await peopleRepository.GetById(dto.Id ?? Guid.Empty);

        var dbPerson = new People
        {
            Name = dto.Name
                ?? existingPerson?.Name
                ?? throw new ServiceException("Field 'Name' was not provided and must exist."),

            Contributions = []
        };

        if(dto.Id != null)
        {
            dbPerson.Id = dto.Id ?? new Guid();

            return await peopleRepository.Update(dbPerson);
        }

        return await peopleRepository.Create(dbPerson);
    }

    public async Task<IEnumerable<PersonDto>> GetPeople(CancellationToken ct)
    {
        var people = await peopleRepository.GetAll();

        return people.Select(p => new PersonDto
        {
            Id = p.Id,
            Name = p.Name
        });
    }

    public async Task<IEnumerable<PersonDto>> GetPersonByName(string name, CancellationToken ct)
    {
        var people = await peopleRepository.SearchByField("Name", name)
            ?? throw new InvalidOperationException("Person does not exist.");

        return people.Select(person => new PersonDto
        {
            Id = person.Id,
            Name = person.Name
        });
    }

    public async Task<PersonDto> GetPerson(Guid id, CancellationToken ct)
    {
        var person = await peopleRepository.GetById(id)
            ?? throw new InvalidOperationException("Person does not exist.");

        return new PersonDto
        {
            Id = person.Id,
            Name = person.Name
        };
    }

    public async Task<Guid?> DeletePerson(Guid id, CancellationToken ct)
    {
        return await peopleRepository.DeleteById(id);
    }


    public async Task<Guid> AddExpense(AddExpenseDto dto, CancellationToken ct)
    {
        Expenses existingExpense = null;

        if (dto.Id != null)
            existingExpense = await expenseRepository.GetById(dto.Id ?? Guid.Empty);

        var dbExpense = new Expenses
        {
            Name = dto.Name
                ?? existingExpense?.Name
                ?? throw new ServiceException("Field 'Name' was not provided and must exist."),
            CostCents = dto.Cost != null
                ? dto.Cost.Value
                : existingExpense?.CostCents
                ?? throw new ServiceException("Field 'Cost' was not provided and must exist."),

            Type = !string.IsNullOrWhiteSpace(dto.Type)
                ? Enum.Parse<ExpenseType>(dto.Type)
                : existingExpense?.Type
                ?? throw new ServiceException("Field 'Type' was not provided and must exist."),

            Contributions = []
        };

        if(dto.Id != null)
        {
            dbExpense.Id = dto.Id ?? new Guid();

            return await expenseRepository.Update(dbExpense);
        }

        return await expenseRepository.Create(dbExpense);
    }

    public async Task<ExpenseDto> GetExpense(Guid id, CancellationToken ct)
    {
        var expense = await expenseRepository.GetById(id)
            ?? throw new InvalidOperationException("Expense does not exist.");

        return new ExpenseDto
        {
            Id = expense.Id,
            Cost = expense.Cost,
            Type = expense.Type
        };
    }

    public async Task<IEnumerable<ExpenseDto>> GetExpenses(CancellationToken ct)
    {
        var expenses = await expenseRepository.GetAll();

        return expenses.Select(e => new ExpenseDto
        {
            Name = e.Name,
            Id = e.Id,
            Cost = e.Cost,
            Type = e.Type
        });
    }

    public async Task<Guid?> DeleteExpense(Guid id, CancellationToken ct)
    {
        return await expenseRepository.DeleteById(id);
    }


    public async Task<Guid> AddItem(AddItemDto dto, CancellationToken ct)
    {
        Items existingItem = null;

        if (dto.Id != null)
            existingItem = await itemRepository.GetById(dto.Id ?? Guid.Empty);

        var dbItem = new Items
        {
            Name = dto.Name
                ?? existingItem?.Name
                ?? throw new ServiceException("Field 'Name' was not provided and must exist."),

            ContributionId = dto.ContributionId
                ?? existingItem?.ContributionId
                ?? throw new ServiceException("Field 'ContributionId' was not provided and must exist."),

            Contribution = null!
        };

        if(dto.Id != null)
        {
            dbItem.Id = dto.Id ?? new Guid();

            return await itemRepository.Update(dbItem);
        }

        return await itemRepository.Create(dbItem);
    }

    public async Task<ItemDto> GetItem(string id, CancellationToken ct)
    {
        var guid = Guid.Parse(id);

        var item = await itemRepository.GetById(guid)
            ?? throw new InvalidOperationException("Item does not exist.");

        return new ItemDto
        {
            Id = item.Id,
            Name = item.Name,
            ContributionId = item.ContributionId
        };
    }

    public async Task<IEnumerable<ItemDto>> GetItems(string contributionId, CancellationToken ct)
    {
        var guid = Guid.Parse(contributionId);

        var items = await itemRepository.GetByContributionId(guid);

        return items.Select(i => new ItemDto
        {
            Id = i.Id,
            Name = i.Name,
            ContributionId = i.ContributionId
        });
    }

    public async Task<Guid?> DeleteItem(Guid id, CancellationToken ct)
    {
        return await itemRepository.DeleteById(id);
    }


    public async Task<Guid> AddContribution(AddContributionDto dto, CancellationToken ct)
    {
        Contributions existingContribution = null; 

        if(dto.Id != null)
            existingContribution = await contributionRepository.GetById(dto.Id ?? Guid.Empty);

        var dbContribution = new Contributions
        {
            Name = dto.Name
                ?? existingContribution?.Name
                ?? throw new ServiceException("Field 'Name' was not provided and must exist."),

            ContributionCents = dto.Contribution != null
                ? (int)(dto.Contribution.Value * 100)
                : existingContribution?.ContributionCents
                ?? throw new ServiceException("Field 'Contribution' was not provided and must exist."),

            PersonId = dto.PersonId
                ?? existingContribution?.PersonId
                ?? throw new ServiceException("Field 'PersonId' was not provided and must exist."),

            ExpenseId = dto.ExpenseId
                ?? existingContribution?.ExpenseId
                ?? throw new ServiceException("Field 'ExpenseId' was not provided and must exist."),

            Person = null!,
            Expense = null!,
            Items = []
        };

        if(dto.Id != null)
        {
            dbContribution.Id = dto.Id ?? new Guid();

            return await contributionRepository.Update(dbContribution);
        }

        return await contributionRepository.Create(dbContribution);
    }

    public async Task<ContributionDto> GetContribution(Guid id, CancellationToken ct)
    {
        var contribution = await contributionRepository.GetById(id)
            ?? throw new InvalidOperationException("Contribution does not exist.");

        return new ContributionDto
        {
            Id = contribution.Id,
            Contribution = contribution.Contribution,
            PersonId = contribution.PersonId,
            ExpenseId = contribution.ExpenseId
        };
    }

    public async Task<IEnumerable<ContributionDto>> GetContributions(CancellationToken ct)
    {
        var contributions = await contributionRepository.GetAll();

        return contributions.Select(c => new ContributionDto
        {
            Id = c.Id,
            Contribution = c.Contribution,
            PersonId = c.PersonId,
            ExpenseId = c.ExpenseId
        });
    }

    public async Task<Guid?> DeleteContribution(Guid id, CancellationToken ct)
    {
        return await contributionRepository.DeleteById(id);
    }
}