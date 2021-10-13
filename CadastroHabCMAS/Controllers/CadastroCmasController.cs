using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Application.Enums;
using Application.Services.Interfaces;
using CadastroHabCMAS.Base;
using CadastroHabCMAS.ViewModel.CadastroCMASViewModels;
using CadastroHabCMAS.ViewModel.EnderecoViewModels;
using CadastroHabCMAS.Views.PessoaEndereco;
using Domain.Models;
using Microsoft.AspNetCore.Mvc;
using Services.DataStructures;

namespace CadastroHabCMAS.Controllers
{
    public class CadastroCmasController : CustomControllerBase
    {
        private readonly ICadastroCmasService _cmasService;
        private readonly IPessoaEnderecoService _pessoaEnderecoService;

        public CadastroCmasController(ICadastroCmasService cmasService, IPessoaEnderecoService pessoaEnderecoService)
        {
            _cmasService = cmasService;
            _pessoaEnderecoService = pessoaEnderecoService;
        }

        // GET
        public IActionResult Adicionar()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> PV_AddCadastro(CadastroCmasAddViewModel cadastroCmasAddViewModel)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("Error", "Cadastro incompleto, campos estao faltando");
                return View(nameof(Adicionar), cadastroCmasAddViewModel);
            }

            var result = await _pessoaEnderecoService.GetPessoaAsync(cadastroCmasAddViewModel.Id);
            if (result is ServiceResult<Pessoa> resultado && resultado.Type == ServiceResultType.Success)
            {
                var pessoa = resultado.Result;
                var cadastro = cadastroCmasAddViewModel.ToModel(cadastroCmasAddViewModel.Nis,
                    cadastroCmasAddViewModel.Inseguranca
                    , cadastroCmasAddViewModel.Residencia, cadastroCmasAddViewModel.Localidade,
                    cadastroCmasAddViewModel.Beneficio,
                    cadastroCmasAddViewModel.Familia, cadastroCmasAddViewModel.Sanitizacao);
                await _cmasService.SaveCadastro(pessoa,cadastro);
                return View(nameof(Sucesso));
            }


            return PartialView();
        }

        public IActionResult PV_AddCadastro()
        {
            return PartialView();
        }

        public async Task<IActionResult> ListarCadastros()
        {
            ListarCadastrosViewModel listarCadastrosViewModel = new ListarCadastrosViewModel();
            var result = await _cmasService.ListCadastro();
            if (result is ServiceResult<List<PessoaEndereco>> cadastros && result.Type == ServiceResultType.Success)
            {
                listarCadastrosViewModel.CadastroCmasList = cadastros.Result;
                return View(listarCadastrosViewModel);
            }

            return LidarComErro(result, listarCadastrosViewModel);
        }
        
        [HttpPost]
        public async Task<IActionResult> ListarCadastros(ListarCadastrosViewModel listarCadastrosViewModel)
        {
            var result = await _pessoaEnderecoService.SearchForPessoaEndereco(listarCadastrosViewModel.PessoaID);
            if (result is ServiceResult<PessoaEndereco> resultado && resultado.Type == ServiceResultType.Success)
            {
                LupaCadastradoViewModel lupaCadastradoViewModel = new LupaCadastradoViewModel();
                lupaCadastradoViewModel.PessoaEndereco = resultado.Result;

                return View(nameof(Lupa), lupaCadastradoViewModel);
            }
            return LidarComErro(result, listarCadastrosViewModel, nameof(PV_ErroCadastro));
        }
        
        
        [HttpPost]
        public async Task<IActionResult> EncontrarCpf(string cpf)
        {
            var result = await _pessoaEnderecoService.SearchForCpfAsync(cpf);
            if (result is ServiceResult<Pessoa> resultado && resultado.Type == ServiceResultType.Success)
            {
                CadastroCmasAddViewModel viewModel = new CadastroCmasAddViewModel();
                viewModel.Id = resultado.Result.Id;
                return PartialView(nameof(PV_AddCadastro), viewModel);
            }

            return LidarComErro(result, null, nameof(PV_ErroCadastro));
        }

        public IActionResult PV_ErroCadastro(BaseViewModel viewModel)
        {
            return PartialView(viewModel);
        }

        public IActionResult Sucesso()
        {
            return View();
        }

        public IActionResult Lupa()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Lupa([Bind] EntregaViewModel entregaViewModel)
        {
            var result = await _pessoaEnderecoService.SearchForCestaBasica(entregaViewModel.CestaBasica.Id);
            if (result is ServiceResult<CestaBasica> resultado && resultado.Type == ServiceResultType.Success)
            {
                CestaBasica cestaBasica = resultado.Result;
                cestaBasica.Entregas = entregaViewModel.Entregas;
                foreach (var ent in cestaBasica.Entregas)
                {
                    ent.CestaBasica = cestaBasica;
                }

                await _pessoaEnderecoService.UpdateCestaBasica(cestaBasica);
                return View(nameof(Sucesso));
            }

            return View();
        }

        public IActionResult PV_Lupa()
        {
            return PartialView();
        }
        
        [HttpPost]
        public async Task<IActionResult> PV_Lupa(string CestaId)
        {
            var result = await _pessoaEnderecoService.SearchForCestaBasica(int.Parse(CestaId));
            if (result is ServiceResult<CestaBasica> resultado && resultado.Type == ServiceResultType.Success)
            {
                EntregaViewModel entregaViewModel = new EntregaViewModel();
                entregaViewModel.CestaBasica = resultado.Result;
                entregaViewModel.Entregas = (List<Entrega>)resultado.Result.Entregas;
                return PartialView(nameof(PV_Lupa),entregaViewModel);
            }
            return LidarComErro(result, null, nameof(PV_ErroCadastro));
        }

    }
}