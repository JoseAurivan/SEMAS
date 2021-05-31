namespace Domain.Models
{
    public class PessoaEndereco
    {
        public Pessoa Pessoa { get; set; }
        public int PessoaId { get; set; }
        public Endereco Endereco { get; set; }
        public int EnderecoId { get; set; }
    }
}