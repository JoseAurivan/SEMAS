using CadastroHabCMAS.ViewModel.EnderecoViewModels;
using Microsoft.AspNetCore.Mvc;

namespace CadastroHabCMAS.Controllers
{
    public class EnderecoController : Controller
    {
        // GET
        public IActionResult Adicionar()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Adicionar(EnderecoAddViewModel enderecoAddViewModel)
        {
            return View();
        }

        public IActionResult Alterar()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Alterar(AlterarEnderecoViewModel alterarEnderecoViewModel)
        {
            return View();
        }
        
    }
}