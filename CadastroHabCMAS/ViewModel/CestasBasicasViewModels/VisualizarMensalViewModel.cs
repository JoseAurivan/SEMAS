using System;
using System.ComponentModel.DataAnnotations;
using CadastroHabCMAS.Views.PessoaEndereco;
using Domain.Enums;

namespace CadastroHabCMAS.ViewModel.CestasBasicasViewModels
{
    public class VisualizarMensalViewModel:BaseViewModel
    {
        [Required]
        public Unidade Unidade { get; set; }
        [Required]
        public int Mes { get; set; }
        [Required]
        public int Ano { get; set; }
    }
}