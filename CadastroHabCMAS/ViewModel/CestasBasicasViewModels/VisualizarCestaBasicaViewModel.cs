using System.ComponentModel.DataAnnotations;

namespace CadastroHabCMAS.ViewModel.CestasBasicasViewModels
{
    public class VisualizarCestaBasicaViewModel
    {
        [Required]
        public string PrxEntrega { get; set; }
        [Required]
        public int Quant { get; set; }
        [Required]
        public bool Status { get; set; }
    }
}