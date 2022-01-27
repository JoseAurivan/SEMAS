using System.Threading.Tasks;
using Application.Services.Interfaces;
using CadastroHabCMAS.Base;
using CadastroHabCMAS.ViewModel.DiretoriaDoTrabalhoViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Formatters.Xml;

namespace CadastroHabCMAS.Controllers
{
    public class DiretoriaTrabalhoController:CustomControllerBase
    {
        private readonly ICurriculoService _curriculoService;

        public DiretoriaTrabalhoController(ICurriculoService curriculoService)
        {
            _curriculoService = curriculoService;
        }

        public IActionResult CriarCurriculo()
        {
            return View();
        }
        
        [HttpPost]
        public async Task<IActionResult> CriarCurriculo(CadastroCurriculoViewModel viewModel)
        {
            var curriculo = viewModel.ToModelCurriculo(viewModel.Resumo, viewModel.Competencias);
            var certificado = viewModel.ToModelCertificado(viewModel.Titulo, viewModel.Expeditor);
            var experiencia= viewModel.ToModelExperiencia(viewModel.Local, viewModel.Descricao,
                viewModel.DataInicio, viewModel.DataFinal);
            
            curriculo.Certificados.Add(certificado);
            curriculo.Experiencias.Add(experiencia);
            certificado.Curriculo = curriculo;
            experiencia.Curriculo = curriculo;
            
            await _curriculoService.SalvarCurriculo(curriculo, certificado, experiencia);
            
            return View(new CadastroCurriculoViewModel());
        }

        public IActionResult PV_CriarCertificado()
        {
            return View();
        }

        public IActionResult PV_CriarExperiencia()
        {
            return View();
        }

        [HttpPost]
        public IActionResult GerarPaginasCertificado()
        {
            return  PartialView(nameof(PV_CriarCertificado)) ;
        }

        [HttpPost]
        public IActionResult GerarPaginasExperiencia()
        {
            return PartialView(nameof(PV_CriarExperiencia));
        }
    }
}