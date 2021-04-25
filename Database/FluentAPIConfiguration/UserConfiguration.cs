using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Database.FluentAPIConfiguration
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> modelBuilder)
        {
            modelBuilder
                .ToTable("user")
                .HasIndex(x => x.Username)
                .IsUnique();

            modelBuilder
                .Property(x => x.Username)
                .IsRequired();
        }
    }
}