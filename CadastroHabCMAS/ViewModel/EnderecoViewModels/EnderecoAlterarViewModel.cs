using System.ComponentModel.DataAnnotations;
using CadastroHabCMAS.Views.PessoaEndereco;
using Domain.Enums;
using Domain.Models;

namespace CadastroHabCMAS.ViewModel.EnderecoViewModels
{
    public class EnderecoAlterarViewModel : BaseViewModel
    {
        [Required(ErrorMessage = "O CPF inserido não é válido")]
        public Pessoa Pessoa { get; set; }
        
    }
}