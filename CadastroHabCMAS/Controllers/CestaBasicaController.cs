using CadastroHabCMAS.ViewModel.CestasBasicasViewModels;
using Microsoft.AspNetCore.Mvc;

namespace CadastroHabCMAS.Controllers
{
    public class CestaBasicaController : Controller
    {
        // GET
        public IActionResult Visualizar()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Visualizar(VisualizarCestaBasicaViewModel cestaBasica)
        {
            return View();
        }
        
        public IActionResult Adicionar()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Adicionar(AddCestaBasicaViewModel cestaBasica)
        {
            return View();
        }
    }
}