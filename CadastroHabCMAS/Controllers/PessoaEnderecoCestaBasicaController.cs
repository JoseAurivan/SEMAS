﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Enums;
using Application.Services.Interfaces;
using CadastroHabCMAS.Base;
using CadastroHabCMAS.ViewModel.CadastroCMASViewModels;
using CadastroHabCMAS.ViewModel.CestasBasicasViewModels;
using CadastroHabCMAS.ViewModel.EnderecoViewModels;
using CadastroHabCMAS.ViewModel.PessoaEnderecoViewModel;
using CadastroHabCMAS.Views.PessoaEndereco;
using Domain.Models;
using Microsoft.AspNetCore.Http;
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
                return View(nameof(Sucesso));
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
                    var resultAlterar = await _pessoaEnderecoService.SearchForCpfPessoaEnderecoAsync(cpf);
                    if (resultAlterar is ServiceResult<List<PessoaEndereco>> resultadoAlterar && resultAlterar.Type == ServiceResultType.Success)
                    {
                        viewModel.PessoaEnderecos = resultadoAlterar.Result;
                    }
                    else
                    {
                        ModelState.AddModelError("Erro", "CPF invalido");
                    }
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

                if (typeOf == "visualizar")
                {
                    VisualizarCestaBasicaViewModel viewModel = new VisualizarCestaBasicaViewModel();
                    var pessoa = resultado.Result;
                    var findResult = await _pessoaEnderecoService.SearchForEndereco(pessoa.Id);
                    if (findResult is ServiceResult<Endereco> end && end.Type == ServiceResultType.Success)
                    {
                       viewModel.Endereco =  end.Result;
                       viewModel.Nome = end.Result.Pessoa.ElementAt(0).Pessoa.Nome;
                       viewModel.Bairro = end.Result.Bairro;
                       viewModel.Cep = end.Result.Cep;
                       viewModel.Cpf = end.Result.Pessoa.ElementAt(0).Pessoa.Cpf;
                       viewModel.Complemento = end.Result.Complemento;
                       viewModel.Entregas = (List<Entrega>)end.Result.Cesta.Entregas;
                       
                       return View(nameof(PV_Visualizar), viewModel);
                    }

                    return LidarComErro(findResult, viewModel, nameof(PV_Erro));
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
                return View(nameof(Sucesso));
            }


            return LidarComErro(result, enderecoAddViewModel, nameof(Adicionar));
        }

        public IActionResult Alterar()
        {
            
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Alterar(EnderecoAlterarViewModel enderecoAlterarViewModel)
        {
            foreach (var alterar in enderecoAlterarViewModel.PessoaEnderecos)
            {
                var result = await _pessoaEnderecoService.FindPessoaAsync(alterar.Pessoa.Id);
                if (result is ServiceResult<Pessoa> resultado && resultado.Type == ServiceResultType.Success)
                {
                    var pessoa = resultado.Result;
                    var pessoaEndereco = enderecoAlterarViewModel.ToModel(alterar.Endereco);
                    pessoaEndereco.EnderecoId = pessoaEndereco.Endereco.Id;
                    pessoaEndereco.Pessoa = pessoa;
                    pessoaEndereco.PessoaId = pessoaEndereco.Pessoa.Id;
                    await _pessoaEnderecoService.SavePessoaEndereco(pessoaEndereco);

                } 
            }
            return View(nameof(Sucesso));
        }
        
        public IActionResult Visualizar()
        {
            return View();
        }
        
        [HttpPost]
        public  IActionResult Visualizar(VisualizarCestaBasicaViewModel viewModel)
        {
            
            return View(nameof(Relatorio), viewModel); 
            
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
                for (int i = 0; i < int.Parse(cestaBasica.NumeroMeses); i++)
                {
                    Entrega entrega = new Entrega();
                    cestaBasica.Entregas.Add(entrega);
                }
                await _pessoaEnderecoService.SaveCestaBasica(endereco, cestaBasica);
                return View(nameof(Sucesso));
            }
            
            return LidarComErro(result,viewModel,nameof(AdicionarCestaBasica));
        }

        public IActionResult VisualizarMensal()
        {
            return View();
        }
        
        [HttpPost]
        public async Task<IActionResult> VisualizarMensal(VisualizarMensalViewModel visualizarMensalViewModel)
        {
            var result = await  _pessoaEnderecoService.ListControl(visualizarMensalViewModel.Unidade,
                visualizarMensalViewModel.Mes,
                visualizarMensalViewModel.Ano);
            if (result is ServiceResult<List<PessoaEndereco>> resultado && resultado.Type == ServiceResultType.Success)
            {
                RelatorioControleViewModel relatorioControleViewModel = new RelatorioControleViewModel();
                relatorioControleViewModel.PessoaEnderecos = resultado.Result;
                return View(nameof(RelatorioControle), relatorioControleViewModel);
            }

            return LidarComErro(result,visualizarMensalViewModel,nameof(VisualizarMensal));
        }

        public IActionResult PV_Visualizar()
        {
            return PartialView();
        }

        public IActionResult Sucesso()
        {
            return View();
        }

        public IActionResult Relatorio()
        {
            return View();
        }

        public IActionResult RelatorioControle()
        {
            return View();
        }
    }
}