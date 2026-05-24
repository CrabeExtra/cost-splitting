using Round_2.Application.Service.Interface;
using Round_2.Database.Context;
using Microsoft.EntityFrameworkCore;
using Round_2.Database.Entity;
using Round_2.Database.Exceptions;

namespace Round_2.Application.Service;


/// <summary>
/// Base repository for all entities. Contains common methods for all repositories.
/// </summary>
public class EntityRepository<T>(AppDbContext db) 
    : IEntityRepository<T>
    where T : class, IDbEntity
{
    public async Task<T?> GetById(Guid id) => await db.Set<T>().FindAsync(id);
    
    public async Task<T?> GetByField(string fieldName, string value) =>
        await db.Set<T>()
            .FirstOrDefaultAsync(e =>
                EF.Property<string>(e, fieldName) == value);
    
    public async Task<IEnumerable<T>> SearchByField(string fieldName, string value) =>
        await db.Set<T>()
            .Where(e =>
                EF.Property<string>(e, fieldName).Contains(value))
            .ToListAsync();

    public async Task<List<T>> GetPagedAsync(int offset, int limit)
    {
        return await db.Set<T>()
            .Skip(offset)
            .Take(limit)
            .ToListAsync();
    }

    public async Task<List<T>> GetAll()
    {
        return await db.Set<T>()
            .ToListAsync();
    }

    public async Task<Guid> Create(T entity) 
    {
        await db.Set<T>().AddAsync(entity);
        await db.SaveChangesAsync();
        return entity.Id;
    }

    public async Task<Guid> Update(T entity)
    {
        db.Set<T>().Update(entity);
        await db.SaveChangesAsync();
        return entity.Id;
    }

    public async Task<Guid?> DeleteById(Guid id)
    {
        var entity = await db.Set<T>().FindAsync(id);

        if (entity == null)
        {
            Console.WriteLine($"Error: {typeof(T).Name} null when attempting to delete. Throwing.");
            throw new RepositoryException($"Specified '{typeof(T).Name}' entry does not exist in the database.");
        }

        db.Set<T>().Remove(entity);
        await db.SaveChangesAsync();
        return id;
    }
    
}

