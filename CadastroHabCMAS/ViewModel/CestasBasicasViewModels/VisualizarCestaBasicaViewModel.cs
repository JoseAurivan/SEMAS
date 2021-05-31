using System.ComponentModel.DataAnnotations;
using CadastroHabCMAS.Views.PessoaEndereco;

namespace CadastroHabCMAS.ViewModel.CestasBasicasViewModels
{
    public class VisualizarCestaBasicaViewModel : BaseViewModel
    {
        [Required]
        public string PrxEntrega { get; set; }
        [Required]
        public int Quant { get; set; }
        [Required]
        public bool Status { get; set; }
    }
}