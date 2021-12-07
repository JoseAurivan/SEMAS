using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Database.FluentAPIConfiguration
{
    public class ExperienciaConfiguration:IEntityTypeConfiguration<Experiencias>
    {
        public void Configure(EntityTypeBuilder<Experiencias> builder)
        {
            builder.ToTable("experiencia");
        }
    }
}