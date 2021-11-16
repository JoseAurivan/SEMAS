using System.Collections.Generic;
using Domain.Enums;
using Domain.Models;

namespace CadastroHabCMAS.ViewModel.CadastroCMASViewModels
{
    public class RelatorioControleViewModel
    {
        public List<PessoaEndereco> PessoaEnderecos { get; set; }

        public Unidade Unidade { get; set; }

        public int Mes { get; set; }

        public int Ano { get; set; }
    }
}