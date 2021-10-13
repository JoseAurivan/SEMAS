using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using CadastroHabCMAS.Views.PessoaEndereco;
using Domain.Models;

namespace CadastroHabCMAS.ViewModel.CestasBasicasViewModels
{
    public class VisualizarCestaBasicaViewModel : BaseViewModel
    {
        public Endereco Endereco { get; set; }
        public string Nome { get; set; }
        public string Complemento { get; set; }
        public string Bairro { get; set; }
        public string Cep { get; set; }
        public string Cpf { get; set; }
        public List<Entrega> Entregas { get; set; }
        public string NomeDoAgente { get; set; }
        public string NomeDaUnidade { get; set; }
    }
}