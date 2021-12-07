using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Database.FluentAPIConfiguration
{
    public class CurriculoConfiguration:IEntityTypeConfiguration<Curriculo>
    {
        public void Configure(EntityTypeBuilder<Curriculo> builder)
        {
            builder.ToTable("curriculo")
                .HasMany(c => c.Certificados)
                .WithOne(c => c.Curriculo);
            builder.HasMany(c => c.Experiencias)
                .WithOne(c => c.Curriculo);
        }
    }
}