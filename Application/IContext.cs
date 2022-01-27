using System;
using System.Threading.Tasks;
using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace Application
{
    public interface IContext
    {
        public DbSet<User> Users { get; }
        public DbSet<Pessoa> Pessoas { get;}
        public DbSet<CestaBasica> CestaBasicas { get;}
        public DbSet<Endereco> Enderecos { get;}
        public DbSet<CadastroCmas> Cadastros { get;}
        public DbSet<PessoaEndereco> PessoaEnderecos { get; }
        public DbSet<Entrega> Entregas { get; }
        
        
        public DbSet<Curriculo> Curriculos { get; }
        public DbSet<Experiencias> Experiencias { get; }
        public DbSet<Certificado> Certificados { get; set; }
        Task SaveChangesAsync();

        EntityEntry<TEntity> Entry<TEntity>(TEntity entry) where TEntity: class;
        EntityEntry Entry(object entry);
    }
}