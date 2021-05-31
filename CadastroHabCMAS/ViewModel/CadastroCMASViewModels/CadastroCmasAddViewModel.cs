using System.ComponentModel.DataAnnotations;
using CadastroHabCMAS.Views.PessoaEndereco;

namespace CadastroHabCMAS.ViewModel.CadastroCMASViewModels
{
    public class CadastroCmasAddViewModel : BaseViewModel
    {
        [Required]
        public string Nis { get; set; }
        [Required]
        public bool Inseguranca { get; set; }
        [Required]
        public string Residencia { get; set; }
        [Required]
        public string Localidade { get; set; }
        [Required]
        public bool Beneficio { get; set; }
        [Required]
        public string Familia { get; set; }
        [Required]
        public bool Sanitizacao { get; set; }
    }
}