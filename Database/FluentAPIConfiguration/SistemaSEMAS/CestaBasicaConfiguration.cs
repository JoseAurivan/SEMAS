using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Database.FluentAPIConfiguration
{
    public class CestaBasicaConfiguration:IEntityTypeConfiguration<CestaBasica>
    {
        public void Configure(EntityTypeBuilder<CestaBasica> builder)
        {
            builder.ToTable("cestaBasica")
                .HasMany(c => c.Entregas)
                .WithOne(x => x.CestaBasica);
        }
    }
}