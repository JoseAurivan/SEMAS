using System;
using System.ComponentModel.DataAnnotations;
using CadastroHabCMAS.Views.PessoaEndereco;
using Domain.Models;

namespace CadastroHabCMAS.ViewModel.DiretoriaDoTrabalhoViewModels
{
    public class CadastroCurriculoViewModel:BaseViewModel
    {
        [Required]
        public string Resumo { get; set; }
        [Required]
        public string Competencias { get; set; }
        [Required]
        public string Titulo { get; set; }
        [Required]
        public string Expeditor { get; set; }
        [Required]
        public string Local { get; set; }
        [Required]
        public string Descricao { get; set; }
        [Required]
        public DateTime DataInicio { get; set; }
        [Required]
        public DateTime DataFinal { get; set; }

        public Curriculo ToModelCurriculo(string resumo, string competencia)
        {
            return new Curriculo()
            {
                Resumo = resumo,
                Competencias = competencia
            };
        }

        public Certificado ToModelCertificado(string titulo, string expeditor)
        {
            return new Certificado()
            {
                Titulo = titulo,
                Expeditor = expeditor
            };
        }

        public Experiencias ToModelExperiencia(string local, string descricao, DateTime dataInicio, DateTime dataFinal)
        {
            return new Experiencias()
            {
                Descricao = descricao,
                Local = local,
                DataInicio = dataInicio,
                DataFinal = dataFinal
            };
        }
    }
}