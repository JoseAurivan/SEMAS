using System.ComponentModel.DataAnnotations;
using Domain.Enums;

namespace CadastroHabCMAS.ViewModel.EnderecoViewModels
{
    public class AlterarEnderecoViewModel
    {
        [Required]
        public string Estado { get; set; }
        [Required]
        public string Cidade { get; set; }
        [Required]
        public string Cep { get; set; }
        [Required]
        public string Bairro { get; set; }
        [Required]
        public string Complemento { get; set; }
        
        public TipoEndereco TipoEndereco { get; set; }
        

    }
}