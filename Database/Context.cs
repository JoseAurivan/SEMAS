using Database.FluentAPIConfiguration;
using Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Database
{
    public class Context:DbContext
    {
        public Context()
        {
            
        }
        
        public Context(DbContextOptions<Context> options) : base(options)
        {
        }
        
        public DbSet<User> Users { get; set; }
        public DbSet<Pessoa> Pessoas { get; set; }
        public DbSet<CestaBasica> CestaBasicas { get; set; }
        public DbSet<Endereco> Enderecos { get; set; }
        public DbSet<CadastroCmas> Cadastros { get; set; }
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
           modelBuilder.ApplyConfiguration(new UserConfiguration());
           modelBuilder.ApplyConfiguration(new PessoaConfiguration());
           modelBuilder.ApplyConfiguration(new EnderecoConfiguration());
           modelBuilder.ApplyConfiguration(new CadastroCMASConfiguration());
           modelBuilder.ApplyConfiguration(new CestaBasicaConfiguration());
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (optionsBuilder.IsConfigured) return;
            optionsBuilder.UseSqlServer("Server=localhost;Database=teste05;" +
                                        "Trusted_Connection=True;MultipleActiveResultSets=true");
        }
    }
}