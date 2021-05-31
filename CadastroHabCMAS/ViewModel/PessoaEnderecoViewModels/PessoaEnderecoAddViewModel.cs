using System.ComponentModel.DataAnnotations;
using CadastroHabCMAS.Views.PessoaEndereco;
using Domain.Enums;
using Domain.Models;

namespace CadastroHabCMAS.ViewModel.PessoaEnderecoViewModel
{
    public class PessoaEnderecoAddViewModel : BaseViewModel
    {
        [Required(ErrorMessage = "Campo NOME é obrigatorio")]
        public string Nome { get; set; }
        [Required(ErrorMessage = "Campo CPF  é obrigatorio")]
        [MaxLength(11, ErrorMessage = "CPF deve ter 11 caracteres")]
        [MinLength(11, ErrorMessage= "CPF deve ter 11 caracteres")]
        [RegularExpression("^[0-9]*$", ErrorMessage = "CPF deve ser numerico")]
        public string Cpf { get; set; }
        [Required(ErrorMessage = "Campo RG  é obrigatorio")]
        public string Rg { get; set; }
        [Required(ErrorMessage = "Campo TELEFONE  é obrigatorio")]
        public string Telefone { get; set; }
        
        [Required(ErrorMessage = "Campo EMAIL  é obrigatorio")]
        [EmailAddress(ErrorMessage = "Campo email deve ser válido")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Campo SEXO  é obrigatorio")]
        public Sexo Sexo { get; set; }
        
        [Required(ErrorMessage = "Campo ESTADO  é obrigatorio")]
        public string Estado { get; set; }
        [Required(ErrorMessage = "Campo CIDADE  é obrigatorio")]
        public string Cidade { get; set; }
        [Required(ErrorMessage = "Campo CEP  é obrigatorio")]
        public string Cep { get; set; }
        [Required(ErrorMessage = "Campo BAIRRO  é obrigatorio")]
        public string Bairro { get; set; }
        [Required(ErrorMessage = "Campo COMPLEMENTO  é obrigatorio")]
        public string Complemento { get; set; }

        public Pessoa ToModelPessoa(string nome, string cpf, string rg, string telefone, string email, Sexo sexo)
        {
            return new Pessoa()
            {
                Nome = nome,
                Cpf = cpf,
                Rg = rg,
                Telefone = telefone,
                Email = email,
                Sexo = sexo
            };
        }

        public Endereco ToModelEndereco(string estado, string cidade, string cep, string bairro, string complemento)
        {
            return new Endereco()
            {
                Estado = estado,
                Cidade = cidade,
                Cep = cep,
                Bairro = bairro,
                Complemento = complemento,
                TipoEndereco = TipoEndereco.Correspondencia
            };
        }
    }
}