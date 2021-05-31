using System;
using System.Threading.Tasks;
using Application.Enums;
using Application.Services.Interfaces;
using CadastroHabCMAS.Base;
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
    public class PessoaEnderecoController : CustomControllerBase
    {
        private readonly IPessoaEnderecoService _pessoaEnderecoService;
        public PessoaEnderecoController(IPessoaEnderecoService pessoaEnderecoService)
        {
            _pessoaEnderecoService = pessoaEnderecoService;
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
            
            await _pessoaEnderecoService.SavePessoa(pessoa, endereco);
            return View();
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


            return LidarComErro(result, enderecoAddViewModel);
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
        
    }
}