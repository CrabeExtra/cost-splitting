
using Round_2.Database.Repository.Interface;
using Round_2.Database.Repository;

namespace Round_2.Database;

public static class DatabaseCollectionExtensions
{
    public static IServiceCollection AddDatabase(this IServiceCollection services, string connectionString)
    {
        services.AddScoped<IPeopleRepository, PeopleRepository>();
        services.AddScoped<IItemRepository, ItemRepository>();
        services.AddScoped<IExpenseRepository, ExpenseRepository>();
        services.AddScoped<IContributionRepository, ContributionRepository>();
        return services;
    }
}