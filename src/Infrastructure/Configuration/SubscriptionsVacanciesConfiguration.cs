using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configuration;

public class SubscriptionsVacanciesConfiguration : IEntityTypeConfiguration<SubscriptionsVacancies>
{
    public void Configure(EntityTypeBuilder<SubscriptionsVacancies> builder)
    {
        builder
            .HasKey(sv => new { sv.SubscriptionsId, sv.VacanciesId });  
        
        builder
            .HasOne(s => s.Subscription)
            .WithMany(v => v.SubscriptionsVacancies)
            .HasForeignKey(f => f.SubscriptionsId)
            .OnDelete(DeleteBehavior.Restrict);
        
        builder
            .HasOne(v => v.Vacancy)
            .WithMany(sv => sv.SubscriptionsVacancies)
            .HasForeignKey(f => f.VacanciesId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}