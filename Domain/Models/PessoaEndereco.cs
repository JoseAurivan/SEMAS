namespace Domain.Models
{
    public class PessoaEndereco
    {
        public Pessoa Pessoa { get; set; }
        public int IdPessoa { get; set; }
        public Endereco Endereco { get; set; }
        public int IdEndereco { get; set; }
    }
}