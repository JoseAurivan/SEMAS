using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using CadastroHabCMAS.Views.PessoaEndereco;
using Domain.Enums;
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
        
        public Demandas Demandas { get; set; }

        public Pessoa Pessoa { get; set; }
        public int IdPessoa { get; set; }
        
        
        public IFormFileCollection Anexos { get; set; }
        
        public string Caminhos { get; set; }

        public CestaBasica ToModelCestaBasica(string numeroMeses, int quantidade,
            string caminhos, Demandas demanda)
        {
            return new CestaBasica()
            {
                NumeroMeses = numeroMeses,
                Quant = quantidade,
                Caminhos = caminhos,
                Demandas = demanda

            };
        }
    }
}