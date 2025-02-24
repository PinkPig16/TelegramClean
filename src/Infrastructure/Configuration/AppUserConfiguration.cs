using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configuration;

public class AppUserConfiguration : IEntityTypeConfiguration<AppUser>
{
    public void Configure(EntityTypeBuilder<AppUser> builder)
    {
        builder.Property(ln => ln.LastName).HasMaxLength(100);
        builder.Property(fn => fn.FirstName).HasMaxLength(100);
        builder.Property(us => us.Username).HasMaxLength(100);
        builder.Property(lc => lc.LanguageCode).HasMaxLength(100);
    }
}