using CadastroHabCMAS.ViewModel.CadastroCMASViewModels;
using Microsoft.AspNetCore.Mvc;

namespace CadastroHabCMAS.Controllers
{
    public class CadastroCmas : Controller
    {
        // GET
        public IActionResult Adicionar()
        {
            return View();
        }
        
        [HttpPost]
        public IActionResult Adicionar(CadastroCmasAddViewModel cadastroCmasAddViewModel)
        {
            return View();
        }
    }
}