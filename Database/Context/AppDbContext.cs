using Microsoft.EntityFrameworkCore;
using Round_2.Database.Entity;

namespace Round_2.Database.Context;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    // Entities 
    public DbSet<Contributions> Contributions { get; set; }
    public DbSet<Expenses> Expenses { get; set; }
    public DbSet<Items> Items { get; set; }
    public DbSet<People> People { get; set; }

    // Constraints
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<People>(entity =>
        {
            entity.HasKey(u => u.Id);

            entity.HasIndex(u => u.Name)
                .IsUnique();

        });

        modelBuilder.Entity<Expenses>(entity =>
        {
            entity.HasKey(r => r.Id);
            
            entity.HasIndex(u => u.Name)
                .IsUnique();
            
            entity.Property(e => e.Type)
            .HasConversion<string>();
        });

        modelBuilder.Entity<Items>(entity =>
        {
            entity.HasKey(r => r.Id);

            // // composite primary key
            // entity.HasKey(ur => new { ur.ContributionId });

            // foreign key: User
            entity.HasOne(ur => ur.Contribution)
                .WithMany(u => u.Items)
                .HasForeignKey(ur => ur.ContributionId)
                .OnDelete(DeleteBehavior.Cascade);
        });

         modelBuilder.Entity<Contributions>(entity =>
        {
            entity.HasKey(r => r.Id);

            entity.HasOne(ur => ur.Person)
                .WithMany(u => u.Contributions)
                .HasForeignKey(ur => ur.PersonId)
                .OnDelete(DeleteBehavior.Cascade);

            entity.HasOne(ur => ur.Expense)
                .WithMany(u => u.Contributions)
                .HasForeignKey(ur => ur.ExpenseId)
                .OnDelete(DeleteBehavior.Cascade);
        });
    }

}