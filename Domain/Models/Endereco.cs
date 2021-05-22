using System.Collections.Generic;
using Domain.Enums;
using Domain.Interfaces;

namespace Domain.Models
{
    public class Endereco:IModelBase
    {
        public Endereco()
        {
            Pessoa = new List<PessoaEndereco>();
        }

        public int Id { get; set; }
        public virtual ICollection<PessoaEndereco> Pessoa { get; set; }
        public CestaBasica Cesta { get; set; }
        public string Estado { get; set; }
        public string Cidade { get; set; }
        public string Cep { get; set; }
        public string Bairro { get; set; }
        public string Complemento { get; set; }
        public Residencia TipoResidencia { get; set; }
        public TipoEndereco TipoEndereco { get; set; }



        public override bool Equals(object obj)
        {
            if (obj is not Endereco outroEndereco) return false;
            return outroEndereco.Id == Id;
        }

        public override int GetHashCode() => Id.GetHashCode();
    }
}