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

            var result = await _pessoaEnderecoService.FindPessoaAsync(cadastroCmasAddViewModel.Id);
            if (result is ServiceResult<Pessoa> resultado && resultado.Type == ServiceResultType.Success)
            {
                var pessoa = resultado.Result;
                var cadastro = cadastroCmasAddViewModel.ToModel(cadastroCmasAddViewModel.Nis,
                    cadastroCmasAddViewModel.Inseguranca
                    , cadastroCmasAddViewModel.Residencia, cadastroCmasAddViewModel.Localidade,
                    cadastroCmasAddViewModel.Beneficio,
                    cadastroCmasAddViewModel.Familia, cadastroCmasAddViewModel.Sanitizacao);
                await _cmasService.SaveCadastro(pessoa,cadastro);
                return View(nameof(Adicionar));
            }


            return PartialView();
        }

        public IActionResult PV_AddCadastro()
        {
            return PartialView();
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
    }
}