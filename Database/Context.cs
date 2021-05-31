using System.Threading.Tasks;
using Application;
using Database.FluentAPIConfiguration;
using Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Database
{
    public class Context:DbContext, IContext
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
        public DbSet<PessoaEndereco> PessoaEnderecos { get; set; }
        public Task SaveChangesAsync() => SaveChangesAsync(default);

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
           modelBuilder.ApplyConfiguration(new UserConfiguration());
           modelBuilder.ApplyConfiguration(new PessoaConfiguration());
           modelBuilder.ApplyConfiguration(new EnderecoConfiguration());
           modelBuilder.ApplyConfiguration(new CadastroCmasConfiguration());
           modelBuilder.ApplyConfiguration(new CestaBasicaConfiguration());
           modelBuilder.ApplyConfiguration(new PessoaEnderecoConfiguration());
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (optionsBuilder.IsConfigured) return;
            optionsBuilder.UseSqlServer("Server=localhost;Database=teste05;" +
                                        "Trusted_Connection=True;MultipleActiveResultSets=true");
        }
    }
}