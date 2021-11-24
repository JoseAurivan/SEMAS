using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using CadastroHabCMAS.Views.PessoaEndereco;
using Domain.Enums;
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
        public string Nis { get; set; }
        public TipoEndereco TipoEndereco { get; set; }
        public Residencia Residencia { get; set; }
        public string Familia { get; set; }
        public string Telefone { get; set; }
        public string Rg { get; set; }
        public string Cidade { get; set; }
        public bool Inseguranca { get; set; }
        public bool Beneficio { get; set; }
        public bool Sanitizacao { get; set; }
        
        public Sexo Sexo { get; set; }
    }
}