using Round_2.Application.Service;
using Round_2.Database.Context;
using Round_2.Database.Entity;
using Round_2.Database.Repository.Interface;

namespace Round_2.Database.Repository;

#pragma warning disable CS9107
public class ExpenseRepository(
    AppDbContext db
) : EntityRepository<Expenses>(db), IExpenseRepository
{
}