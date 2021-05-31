using System.ComponentModel.DataAnnotations;
using CadastroHabCMAS.Views.PessoaEndereco;

namespace CadastroHabCMAS.ViewModel.CestasBasicasViewModels
{
    public class AddCestaBasicaViewModel : BaseViewModel
    {
        [Required]
        public int Quant { get; set; }
        [Required]
        public string NumeroMeses { get; set; }
    }
}