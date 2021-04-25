using System.Collections.Generic;
using Domain.Enums;

namespace Domain.Models
{
    
    public class Pessoa
    {
        public Pessoa()
        {
            Enderecos = new List<Endereco>();
        }

        public int Id { get; set; }
        public string Nome { get; set; }
        public string Cpf { get; set; }
        public string Rg { get; set; }
        public string Telefone { get; set; }
        public string Email { get; set; }

        public Sexo Sexo { get; set; }
        public virtual ICollection<Endereco> Enderecos { get; set; }

        public override bool Equals(object obj)
        {
            if (obj is not Pessoa outraPessoa) return false;
            return outraPessoa.Id == Id;
        }

        public override int GetHashCode() => Id.GetHashCode();
    }
}