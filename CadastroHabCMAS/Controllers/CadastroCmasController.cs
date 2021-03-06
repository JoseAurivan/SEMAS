using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Threading.Tasks;
using Application.Enums;
using Application.Services.Interfaces;
using CadastroHabCMAS.Base;
using CadastroHabCMAS.ViewModel.CadastroCMASViewModels;
using CadastroHabCMAS.ViewModel.EnderecoViewModels;
using CadastroHabCMAS.Views.PessoaEndereco;
using Domain.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Serilog;
using Services.DataStructures;

namespace CadastroHabCMAS.Controllers
{
    [Authorize]
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
            var identity = User.Identity.Name;

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
                await _cmasService.SaveCadastro(pessoa,cadastro,User.Identity.Name);
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
            var result = await _cmasService.ListCadastro(User.Identity.Name);
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

        [Route("[controller]/{id:int}")]
        public async Task<IActionResult> Lupa(int id)
        {
            var result = await _pessoaEnderecoService.SearchForCestaBasica(id);
            if (result is ServiceResult<CestaBasica> resultado && resultado.Type == ServiceResultType.Success)
            {
                LupaCadastradoViewModel lupaViewModel = new LupaCadastradoViewModel();
                lupaViewModel.CestaBasica = resultado.Result;
                lupaViewModel.Entregas = (List<Entrega>) resultado.Result.Entregas;
                return View(nameof(Lupa), lupaViewModel);
            }
            return LidarComErro(result, id, nameof(PV_ErroCadastro));

        }
        
        [HttpPost]
        [Route("[controller]/{id:int}")]
        public async Task<IActionResult> Lupa([Bind] LupaCadastradoViewModel lupaCadastradoViewModel)
        {

            var busca = await _pessoaEnderecoService.SearchForCestaBasica(lupaCadastradoViewModel.CestaBasica.Id);
            if (busca is ServiceResult<CestaBasica> resultado && resultado.Type == ServiceResultType.Success)
            {
                resultado.Result.Entregas = lupaCadastradoViewModel.Entregas;
                await _pessoaEnderecoService.UpdateCestaBasica(resultado.Result, User.Identity?.Name);
                return View(nameof(Sucesso));
            }

            return LidarComErro(busca,lupaCadastradoViewModel, nameof(PV_ErroCadastro));
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

        public IActionResult DownloadFile(string caminho)
        {
            if (caminho == null) return NotFound();
        
            var net = new System.Net.WebClient();
            var data = net.DownloadData(caminho);
            var content = new MemoryStream(data);
            var contentType = "APPLICATION/octet-stream";
            return File(content, contentType, caminho);
        }

    }
}