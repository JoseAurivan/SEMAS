using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Database.FluentAPIConfiguration
{
    public class PessoaConfiguration:IEntityTypeConfiguration<Pessoa>
    {
        public void Configure(EntityTypeBuilder<Pessoa> builder)
        {
            builder
                .ToTable("pessoa")
                .HasIndex(p => p.Cpf)
                .IsUnique();
            builder
                .Property(p => p.Cpf)
                .IsRequired();

        }
    }
}