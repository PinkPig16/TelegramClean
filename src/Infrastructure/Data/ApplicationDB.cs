using Microsoft.EntityFrameworkCore;
using Domain.Entities;
using Infrastructure.Configuration;

namespace Infrastructure.Data;
public class ApplicationDB : DbContext
{
    public DbSet<AppUser> appUsers { get; set; } = null!;
    public DbSet<Subscriptions> subscriptions { get; set; } = null!;
    public DbSet<Vacancies> vacancies { get; set; } = null!;
    public DbSet<City> citys { get; set; } = null!;
    public DbSet<SubscriptionsVacancies> subscriptionsVacancies { get; set; } = null!;
    public ApplicationDB(DbContextOptions<ApplicationDB> options) : base(options)
    {

    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Vacancies>()
            .HasIndex(v => new { v.URL, v.SiteName })
            .IsUnique();
        modelBuilder.ApplyConfiguration(new AppUserConfiguration());
        modelBuilder.ApplyConfiguration(new SubscriptionsVacanciesConfiguration());
    }
}

