using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Database.FluentAPIConfiguration
{
    public class CadastroCMASConfiguration:IEntityTypeConfiguration<CadastroCmas>
    {
        public void Configure(EntityTypeBuilder<CadastroCmas> builder)
        {
            builder.ToTable("cadastroCMAS");
        }
    }
}