using System.Collections.Generic;
using System.Collections.ObjectModel;
using Domain.Models;

namespace CadastroHabCMAS.ViewModel.CadastroCMASViewModels
{
    public class LupaCadastradoViewModel
    {
        public PessoaEndereco PessoaEndereco { get; set; }
        public CestaBasica CestaBasica {get; set; }
        public List<Entrega> Entregas { get; set; }
    }
}