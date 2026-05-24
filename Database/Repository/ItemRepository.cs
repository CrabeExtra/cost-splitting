using System.Data.Common;
using Microsoft.EntityFrameworkCore;
using Round_2.Application.Service;
using Round_2.Database.Context;
using Round_2.Database.Entity;
using Round_2.Database.Repository.Interface;

namespace Round_2.Database.Repository;

#pragma warning disable CS9107
public class ItemRepository(
    AppDbContext db
) : EntityRepository<Items>(db), IItemRepository
{
    
    public async Task<IEnumerable<Items>> GetByContributionId(Guid contributionId)
        => await db.Items.Where(e => e.ContributionId == contributionId).ToListAsync();
}