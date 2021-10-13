using System.Collections.Generic;
using CadastroHabCMAS.Views.PessoaEndereco;
using Domain.Models;

namespace CadastroHabCMAS.ViewModel.CadastroCMASViewModels
{
    public class ListarCadastrosViewModel : BaseViewModel
    {
        public ListarCadastrosViewModel()
        {
            CadastroCmasList = new List<PessoaEndereco>();
        }
        
        public List<PessoaEndereco> CadastroCmasList { get; set; }
        public int PessoaID { get; set; }
    }
}