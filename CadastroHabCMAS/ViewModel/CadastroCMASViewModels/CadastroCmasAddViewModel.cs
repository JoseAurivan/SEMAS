using System;
using System.ComponentModel.DataAnnotations;
using CadastroHabCMAS.Views.PessoaEndereco;
using Domain.Models;

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
        [Required]
        public int Id { get; set; }
        
        public bool DeterminacaoJudicial { get; set; }
        public bool RecomendacaoTecnica { get; set; }

        public CadastroCmas ToModel(string nis, bool inseguranca, string residencia, string localidade, bool beneficio,
            string familia, bool sanitizacao)
        {
            return new CadastroCmas()
            {
                Nis = nis,
                Inseguranca = inseguranca,
                Residencia = residencia,
                Localidade = localidade,
                Beneficio = beneficio,
                Familia = familia,
                Sanitizacao = sanitizacao

            };
        }
    }
}