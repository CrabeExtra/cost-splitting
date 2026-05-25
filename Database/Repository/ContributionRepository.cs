using System.Data.Common;
using Microsoft.EntityFrameworkCore;
using Round_2.Application.DTOs;
using Round_2.Application.Service;
using Round_2.Database.Context;
using Round_2.Database.Entity;
using Round_2.Database.Repository.Interface;

namespace Round_2.Database.Repository;

#pragma warning disable CS9107
public class ContributionRepository(
    AppDbContext db
) : EntityRepository<Contributions>(db), IContributionRepository
{
    public async Task<IEnumerable<Contributions>> GetContributionsByExpense(Guid expenseId)
    {
        return await db.Contributions
            .Where(e => e.ExpenseId == expenseId)
            .Include(e => e.Person)
            .Include(e => e.Expense)
            .Include(e => e.Expense.Contributions)
            .Include(e => e.Items)
            .ToListAsync();
    }
}