using System.Collections.Generic;
using Domain.Enums;
using Domain.Interfaces;

namespace Domain.Models
{

    public class Pessoa:IModelBase
    {
        public Pessoa()
        {
            Enderecos = new List<PessoaEndereco>();
        }
        public int Id {get; set;}
        public string Nome { get; set; }
        public string Cpf { get; set; }
        public string Rg { get; set; }
        public string Telefone { get; set; }
        public string Email { get; set; }
        public Sexo Sexo { get; set; }
        public CadastroCmas CadastroCmas { get; set; }
        public virtual ICollection<PessoaEndereco> Enderecos { get; set; }
        public Curriculo? Curriculo { get; set; }

        public override bool Equals(object obj)
        {
            if (obj is not Pessoa outraPessoa) return false;
            return outraPessoa.Id == Id;
        }

        public override int GetHashCode() => Id.GetHashCode();
    }
}