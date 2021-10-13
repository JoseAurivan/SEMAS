using System.Collections.Generic;
using Domain.Models;

namespace CadastroHabCMAS.ViewModel.CadastroCMASViewModels
{
    public class EntregaViewModel
    {
        public CestaBasica CestaBasica { get; set; }
        public List<Entrega> Entregas { get; set; }
    }
}