using CadastroHabCMAS.Base;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Formatters.Xml;

namespace CadastroHabCMAS.Controllers
{
    public class DiretoriaTrabalhoController:CustomControllerBase
    {
        public IActionResult CriarCurriculo()
        {
            return View();
        }

        public IActionResult PV_CriarCertificado()
        {
            return View();
        }

        public IActionResult PV_CriarExperiencia()
        {
            return View();
        }
    }
}