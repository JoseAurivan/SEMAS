using System.ComponentModel.DataAnnotations;
using CadastroHabCMAS.Views.PessoaEndereco;
using Domain.Enums;

namespace CadastroHabCMAS.ViewModel.CestasBasicasViewModels
{
    public class VisualizarAnualViewModel:BaseViewModel
    {
        [Required]
        public Unidade Unidade { get; set; }

        [Required]
        public int Ano { get; set; } 
    }
}