using System.ComponentModel.DataAnnotations;
using CadastroHabCMAS.Views.PessoaEndereco;
using Domain.Models;

namespace CadastroHabCMAS.ViewModel.CestasBasicasViewModels
{
    public class AddCestaBasicaViewModel : BaseViewModel
    {
        [Required]
        public int Quant { get; set; }
        [Required]
        public string NumeroMeses { get; set; }

        public Pessoa Pessoa { get; set; }
        public int IdPessoa { get; set; }

        public CestaBasica ToModelCestaBasica(string numeroMeses, int quantidade )
        {
            return new CestaBasica()
            {
                NumeroMeses = numeroMeses,
                Quant = quantidade
            };
        }
    }
}