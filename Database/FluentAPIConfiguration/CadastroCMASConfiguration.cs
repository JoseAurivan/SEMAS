using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Database.FluentAPIConfiguration
{
    public class CadastroCmasConfiguration:IEntityTypeConfiguration<CadastroCmas>
    {
        public void Configure(EntityTypeBuilder<CadastroCmas> builder)
        {
            //TODO mapear pelo Pessoa ID pra ser unico
            builder.ToTable("cadastroCMAS")
                .HasIndex(c=>c.Nis)
                .IsUnique();
            builder.Property(c => c.Nis)
                .IsRequired();
        }
    }
}