using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Database.FluentAPIConfiguration
{
    public class PessoaEnderecoConfiguration:IEntityTypeConfiguration<PessoaEndereco>
    {
        public void Configure(EntityTypeBuilder<PessoaEndereco> builder)
        {
            builder
                .ToTable("PessoaEndereco")
                .HasKey(c => new {IdPessoa = c.PessoaId, IdEndereco = c.EnderecoId});

        }
    }
}