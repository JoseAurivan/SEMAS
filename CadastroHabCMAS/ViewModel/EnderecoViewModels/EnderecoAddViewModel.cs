using System;
using System.ComponentModel.DataAnnotations;
using Domain.Enums;
using Domain.Models;

namespace CadastroHabCMAS.ViewModel.EnderecoViewModels
{
    public class EnderecoAddViewModel
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
        
        public Endereco ToModelEndereco(string estado, string cidade, string cep, string bairro, string complemento, TipoEndereco tipoEndereco)
        {
            return new Endereco()
            {
                Estado = estado,
                Cidade = cidade,
                Cep = cep,
                Bairro = bairro,
                Complemento = complemento,
                TipoEndereco = tipoEndereco
            };
        }
    }
}