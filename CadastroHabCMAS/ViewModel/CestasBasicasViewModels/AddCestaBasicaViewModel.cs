using System.ComponentModel.DataAnnotations;

namespace CadastroHabCMAS.ViewModel.CestasBasicasViewModels
{
    public class AddCestaBasicaViewModel
    {
        [Required]
        public int Quant { get; set; }
        [Required]
        public string NumeroMeses { get; set; }
    }
}