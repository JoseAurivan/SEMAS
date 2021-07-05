using System;
using System.Threading.Tasks;
using Application.Enums;
using Application.Services.Interfaces;
using CadastroHabCMAS.Base;
using CadastroHabCMAS.ViewModel.CestasBasicasViewModels;
using CadastroHabCMAS.ViewModel.EnderecoViewModels;
using CadastroHabCMAS.ViewModel.PessoaEnderecoViewModel;
using CadastroHabCMAS.Views.PessoaEndereco;
using Domain.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Services.DataStructures;

namespace CadastroHabCMAS.Controllers
{
    public class PessoaEnderecoCestaBasicaController : CustomControllerBase
    {
        private readonly IPessoaEnderecoService _pessoaEnderecoService;
        private readonly ICadastroCmasService _cmasService;
        public PessoaEnderecoCestaBasicaController(IPessoaEnderecoService pessoaEnderecoService, ICadastroCmasService cmasService)
        {
            _pessoaEnderecoService = pessoaEnderecoService;
            _cmasService = cmasService;
        }
        public IActionResult Cadastro()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Cadastro(PessoaEnderecoAddViewModel pessoaEndereco)
        {
            if (!ModelState.IsValid)
            {
                return View(nameof(Cadastro), pessoaEndereco);
            }
            var pessoa = pessoaEndereco.ToModelPessoa(pessoaEndereco.Nome, pessoaEndereco.Cpf, pessoaEndereco.Rg,
                pessoaEndereco.Telefone, pessoaEndereco.Email, pessoaEndereco.Sexo);
            var endereco = pessoaEndereco.ToModelEndereco(pessoaEndereco.Estado, pessoaEndereco.Cidade,
                pessoaEndereco.Cep, pessoaEndereco.Bairro, pessoaEndereco.Complemento);
            
            var result = await _pessoaEnderecoService.SavePessoa(pessoa, endereco);
            if (result.Type is ServiceResultType.Success)
            {
                return View();
            }
            return LidarComErro(result, pessoaEndereco, nameof(Cadastro));
        }

        [HttpPost]
        public async Task<IActionResult> EncontrarCpf(string cpf, string typeOf)
        {
            var result = await _pessoaEnderecoService.SearchForCpfAsync(cpf);
            
            if (result is ServiceResult<Pessoa> resultado && resultado.Type == ServiceResultType.Success)
            {

                if (typeOf == "alterar")
                {
                    EnderecoAlterarViewModel viewModel = new EnderecoAlterarViewModel();
                    viewModel.Pessoa = resultado.Result;
                    return PartialView(nameof(PV_EnderecosAlterar),viewModel);
                }
                if (typeOf == "add")
                {
                    EnderecoAddViewModel viewModel = new EnderecoAddViewModel();
                    viewModel.Pessoa = resultado.Result;
                    return PartialView(nameof(PV_EnderecosAdd),viewModel);
                }

                if (typeOf == "addCestaBasica")
                {
                    AddCestaBasicaViewModel viewModel = new AddCestaBasicaViewModel();
                    viewModel.Pessoa = resultado.Result;
                    return PartialView(nameof(PV_CestaAdd), viewModel);

                }

            }
            
            ModelState.AddModelError("Erro", "CPF invalido");
            return PartialView(nameof(PV_Erro));
        }
        
        public ActionResult PV_EnderecosAlterar()
        {
            return PartialView();
        }

        public ActionResult PV_Erro(BaseViewModel viewModel)
        {
            return PartialView(viewModel);
        }
        public ActionResult PV_EnderecosAdd()
        {
            return PartialView();
        }
        
        public IActionResult Adicionar()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> PV_EnderecosAdd(EnderecoAddViewModel enderecoAddViewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(nameof(Adicionar), enderecoAddViewModel);
            }

            var result = await  _pessoaEnderecoService.FindPessoaAsync(enderecoAddViewModel.Id);
            if (result is ServiceResult<Pessoa> resultado && resultado.Type == ServiceResultType.Success)
            {
                var pessoa = resultado.Result;
                var endereco = enderecoAddViewModel.ToModelEndereco(enderecoAddViewModel.Estado,
                    enderecoAddViewModel.Cidade, enderecoAddViewModel.Cep
                    , enderecoAddViewModel.Bairro, enderecoAddViewModel.Complemento, enderecoAddViewModel.TipoEndereco);
                await _pessoaEnderecoService.SavePessoa(pessoa, endereco);
                return View(nameof(Adicionar));
            }


            return LidarComErro(result, enderecoAddViewModel, nameof(Adicionar));
        }

        public IActionResult Alterar()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Alterar(EnderecoAlterarViewModel enderecoAlterarViewModel)
        {
            //TODO parte de commitar mudanças
            return View();
        }
        
        public IActionResult Visualizar()
        {
            return View();
        }
        
        public IActionResult AdicionarCestaBasica()
        {
            return View();
        }

        public IActionResult PV_CestaAdd()
        {
            return PartialView();
        }
        
        [HttpPost]
        public async Task<IActionResult> PV_CestaAdd(AddCestaBasicaViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(nameof(PV_CestaAdd), viewModel);
            }
            
            var result = await _pessoaEnderecoService.SearchForEndereco(viewModel.IdPessoa);
            if (result is ServiceResult<Endereco> resultado && resultado.Type == ServiceResultType.Success)
            {
                var endereco = resultado.Result;
                var cestaBasica = viewModel.ToModelCestaBasica(viewModel.NumeroMeses, viewModel.Quant);

                await _pessoaEnderecoService.SaveCestaBasica(endereco, cestaBasica);
                return View(nameof(AdicionarCestaBasica));
            }
            return LidarComErro(result,viewModel,nameof(AdicionarCestaBasica));
        }
    }
}