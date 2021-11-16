using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using CadastroHabCMAS.Views.PessoaEndereco;
using Domain.Models;
using Microsoft.AspNetCore.Http;

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
        
        public bool DeterminacaoJuridica { get; set; }
        public bool RecomendacaoTecnica { get; set; }
        
        public IFormFileCollection Anexos { get; set; }
        
        public string Caminhos { get; set; }

        public CestaBasica ToModelCestaBasica(string numeroMeses, int quantidade,
            bool determinacaoJuridica, bool recomendacaoTecnica, string caminhos)
        {
            return new CestaBasica()
            {
                NumeroMeses = numeroMeses,
                Quant = quantidade,
                DeterminacaoJuridica = determinacaoJuridica,
                RecomendacaoTecnica = recomendacaoTecnica,
                Caminhos = caminhos
                
            };
        }
    }
}