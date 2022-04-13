using System;

namespace Domain.Models
{
    public class PessoaEndereco:IEquatable<PessoaEndereco>
    {
        public Pessoa Pessoa { get; set; }
        public int PessoaId { get; set; }
        public Endereco Endereco { get; set; }
        public int EnderecoId { get; set; }

        public override bool Equals(object? obj)
        {
            if (obj is PessoaEndereco pessoaEndereco) return Equals(pessoaEndereco);
            return false;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(PessoaId,EnderecoId);
        }

        public bool Equals(PessoaEndereco other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            if (GetHashCode() != other.GetHashCode()) return false;
            
            return PessoaId == other.PessoaId
                   && Equals(Endereco, other.Endereco) 
                   && EnderecoId == other.EnderecoId;
        }
    }
}