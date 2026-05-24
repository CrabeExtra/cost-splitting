using Round_2.Application.Service.Interface;
using Round_2.Database.Entity;

namespace Round_2.Database.Repository.Interface;

public interface IExpenseRepository : IEntityRepository<Expenses>
{
}