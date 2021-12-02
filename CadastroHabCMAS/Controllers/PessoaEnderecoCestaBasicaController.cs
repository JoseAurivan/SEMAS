using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Claims;
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
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Extensions.Logging;
using Serilog;
using Services.DataStructures;

namespace CadastroHabCMAS.Controllers
{
    [Authorize]
    public class PessoaEnderecoCestaBasicaController : CustomControllerBase
    {
        private readonly IPessoaEnderecoService _pessoaEnderecoService;
        private readonly ICadastroCmasService _cmasService;
        private readonly IWebHostEnvironment _environment;


        public PessoaEnderecoCestaBasicaController(IPessoaEnderecoService pessoaEnderecoService,
            ICadastroCmasService cmasService, IWebHostEnvironment environment)
        {
            _pessoaEnderecoService = pessoaEnderecoService;
            _cmasService = cmasService;
            _environment = environment;

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

            var result = await _pessoaEnderecoService.SavePessoa(pessoa, endereco, User.Identity.Name);
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
                    if (resultAlterar is ServiceResult<List<PessoaEndereco>> resultadoAlterar &&
                        resultAlterar.Type == ServiceResultType.Success)
                    {
                        viewModel.PessoaEnderecos = resultadoAlterar.Result;
                    }
                    else
                    {
                        ModelState.AddModelError("Erro", "CPF invalido");
                    }

                    return PartialView(nameof(PV_EnderecosAlterar), viewModel);
                }
                if (typeOf == "add")
                {
                    EnderecoAddViewModel viewModel = new EnderecoAddViewModel();
                    viewModel.Pessoa = resultado.Result;
                    return PartialView(nameof(PV_EnderecosAdd), viewModel);
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
                        viewModel.Endereco = end.Result;
                        viewModel.Nome = end.Result.Pessoa.ElementAt(0).Pessoa.Nome;
                        viewModel.Bairro = end.Result.Bairro;
                        viewModel.Cidade = end.Result.Cidade;
                        viewModel.Sexo = end.Result.Pessoa.ElementAt(0).Pessoa.Sexo;
                        viewModel.Cep = end.Result.Cep;
                        viewModel.Cpf = end.Result.Pessoa.ElementAt(0).Pessoa.Cpf;
                        viewModel.Complemento = end.Result.Complemento;
                        viewModel.Nis = end.Result.Pessoa.ElementAt(0).Pessoa.CadastroCmas.Nis;
                        viewModel.Familia = end.Result.Pessoa.ElementAt(0).Pessoa.CadastroCmas.Familia;
                        viewModel.Inseguranca = end.Result.Pessoa.ElementAt(0).Pessoa.CadastroCmas.Inseguranca;
                        viewModel.Beneficio = end.Result.Pessoa.ElementAt(0).Pessoa.CadastroCmas.Beneficio;
                        viewModel.Sanitizacao = end.Result.Pessoa.ElementAt(0).Pessoa.CadastroCmas.Sanitizacao;
                        viewModel.Telefone = end.Result.Pessoa.ElementAt(0).Pessoa.Telefone;
                        viewModel.Rg = end.Result.Pessoa.ElementAt(0).Pessoa.Rg;
                        viewModel.Entregas = (List<Entrega>)end.Result.Cesta.Entregas;

                        return View(nameof(PV_Visualizar), viewModel);
                    }

                    return LidarComErro(findResult, viewModel, nameof(PV_Erro));
                }

                if (typeOf == "addPessoa")
                {
                    ModelState.AddModelError("Erro", "CPF ja cadastrado");
                    return View(nameof(PV_Erro));
                }
            }

            if (typeOf == "addPessoa")
            {
                return PartialView(nameof(Cadastro));
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

        public IActionResult AdicionarPessoa()
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


            var result = await _pessoaEnderecoService.FindPessoaAsync(enderecoAddViewModel.Id);
            if (result is ServiceResult<Pessoa> resultado && resultado.Type == ServiceResultType.Success)
            {
                var pessoa = resultado.Result;
                var endereco = enderecoAddViewModel.ToModelEndereco(enderecoAddViewModel.Estado,
                    enderecoAddViewModel.Cidade, enderecoAddViewModel.Cep
                    , enderecoAddViewModel.Bairro, enderecoAddViewModel.Complemento, enderecoAddViewModel.TipoEndereco);
                await _pessoaEnderecoService.SavePessoa(pessoa, endereco, User.Identity.Name);
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
                    var pessoa = alterar.Pessoa;
                    pessoa.Id = resultado.Result.Id;
                    pessoa.Cpf = resultado.Result.Cpf;
                    var pessoaEndereco = enderecoAlterarViewModel.ToModel(alterar.Endereco);
                    pessoaEndereco.EnderecoId = pessoaEndereco.Endereco.Id;
                    pessoaEndereco.Pessoa = pessoa;
                    pessoaEndereco.PessoaId = pessoaEndereco.Pessoa.Id;
                    await _pessoaEnderecoService.SavePessoaEndereco(pessoaEndereco,User.Identity.Name);
                }
            }

            return View(nameof(Sucesso));
        }

        public IActionResult Visualizar()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Visualizar(VisualizarCestaBasicaViewModel viewModel)
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
            viewModel.Caminhos = "";
            if (!ModelState.IsValid)
            {
                return View(nameof(PV_CestaAdd), viewModel);
            }

            var result = await _pessoaEnderecoService.SearchForEndereco(viewModel.IdPessoa);
            if (result is ServiceResult<Endereco> resultado && resultado.Type == ServiceResultType.Success)
            {
                var endereco = resultado.Result;
                if (viewModel.Anexos != null)
                {
                    foreach (var arquivo in viewModel.Anexos)
                    {
                        if (arquivo != null || arquivo.Length != 0)
                        {
                            // Define um nome para o arquivo enviado incluindo o sufixo obtido de milissegundos
                            string nomeArquivo = "Usuario_arquivo_" + viewModel.IdPessoa + "_" +
                                                 endereco.Pessoa.ElementAt(0).Pessoa.Nome;
                            //verifica qual o tipo de arquivo : jpg, gif, png, pdf ou tmp
                            if (arquivo.FileName.Contains(".jpg"))
                                nomeArquivo += ".jpg";
                            else if (arquivo.FileName.Contains(".gif"))
                                nomeArquivo += ".gif";
                            else if (arquivo.FileName.Contains(".png"))
                                nomeArquivo += ".png";
                            else if (arquivo.FileName.Contains(".pdf"))
                                nomeArquivo += ".pdf";
                            else
                                nomeArquivo += ".tmp";

                            //string caminhoDestinoArquivoOriginal="C:\\Users\\auriv\\OneDrive\\Área de Trabalho\\Teste\\" + nomeArquivo;

                            var caminho = Path.Combine("Anexos", nomeArquivo);
                            using (var stream = new FileStream(caminho, FileMode.Create))
                            {
                                await arquivo.CopyToAsync(stream);
                                viewModel.Caminhos += caminho + "|";
                            }
                        }
                    }
                }

                var cestaBasica = viewModel.ToModelCestaBasica(viewModel.NumeroMeses, viewModel.Quant,
                    viewModel.DeterminacaoJuridica, viewModel.RecomendacaoTecnica, viewModel.Caminhos);
                for (int i = 0; i < int.Parse(cestaBasica.NumeroMeses); i++)
                {
                    Entrega entrega = new Entrega();
                    cestaBasica.Entregas.Add(entrega);
                }

                await _pessoaEnderecoService.SaveCestaBasica(endereco, cestaBasica, User.Identity.Name);
                return View(nameof(Sucesso));
            }

            return LidarComErro(result, viewModel, nameof(AdicionarCestaBasica));
        }

        public IActionResult VisualizarMensal()
        {
            return View();
        }

        public IActionResult VisualizarAnual()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> VisualizarAnual(VisualizarAnualViewModel viewModel)
        {
            var result = await _pessoaEnderecoService.ListControlYear(viewModel.Unidade,
                viewModel.Ano);
            if (result is ServiceResult<List<PessoaEndereco>> resultado && resultado.Type == ServiceResultType.Success)
            {
                RelatorioControleViewModel relatorioControleViewModel = new RelatorioControleViewModel();
                relatorioControleViewModel.PessoaEnderecos = resultado.Result;
                relatorioControleViewModel.Ano = viewModel.Ano;
                relatorioControleViewModel.Unidade = viewModel.Unidade;
                return View(nameof(RelatorioControleAnual), relatorioControleViewModel);
            }

            return LidarComErro(result, viewModel, nameof(VisualizarAnual));
        }
            [HttpPost]
        public async Task<IActionResult> VisualizarMensal(VisualizarMensalViewModel visualizarMensalViewModel)
        {
            var result = await _pessoaEnderecoService.ListControlMonth(visualizarMensalViewModel.Unidade,
                visualizarMensalViewModel.Mes,
                visualizarMensalViewModel.Ano);
            if (result is ServiceResult<List<PessoaEndereco>> resultado && resultado.Type == ServiceResultType.Success)
            {
                RelatorioControleViewModel relatorioControleViewModel = new RelatorioControleViewModel();
                relatorioControleViewModel.PessoaEnderecos = new List<PessoaEndereco>();
                relatorioControleViewModel.PessoaEnderecos = resultado.Result;
                relatorioControleViewModel.Mes = visualizarMensalViewModel.Mes;
                relatorioControleViewModel.Ano = visualizarMensalViewModel.Ano;
                relatorioControleViewModel.Unidade = visualizarMensalViewModel.Unidade;
                return View(nameof(RelatorioControle), relatorioControleViewModel);
            }

            return LidarComErro(result, visualizarMensalViewModel, nameof(VisualizarMensal));
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
        
        public IActionResult RelatorioControleAnual()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> RelatorioGeralMensal(VisualizarMensalViewModel viewModel)
        {

            var result =await _pessoaEnderecoService.ListControlAllMonth(viewModel.Mes,viewModel.Ano);
            if (result is ServiceResult<List<PessoaEndereco>> resultado && resultado.Type == ServiceResultType.Success)
            {
                RelatorioControleViewModel relatorioControleViewModel = new RelatorioControleViewModel();
                relatorioControleViewModel.PessoaEnderecos = resultado.Result;
                relatorioControleViewModel.Ano = viewModel.Ano;
                relatorioControleViewModel.Mes = viewModel.Mes;
                return View(nameof(RelatorioGeralMensal), relatorioControleViewModel);
            }

            VisualizarMensalViewModel visualizarMensalViewModel = new VisualizarMensalViewModel();
            return LidarComErro(result, visualizarMensalViewModel, nameof(VisualizarMensal));
        }

        [HttpPost]
        public async Task<IActionResult> RelatorioGeralAnual(VisualizarAnualViewModel viewModel)
        {
            
            var result = await _pessoaEnderecoService.ListControlAllYear(viewModel.Ano);
            if (result is ServiceResult<List<PessoaEndereco>> resultado && resultado.Type == ServiceResultType.Success)
            {
                RelatorioControleViewModel relatorioControleViewModel = new RelatorioControleViewModel();
                relatorioControleViewModel.PessoaEnderecos = resultado.Result;
                relatorioControleViewModel.Ano = viewModel.Ano;
                return View(nameof(RelatorioGeralAnual), relatorioControleViewModel);
            }

            return LidarComErro(result, viewModel, nameof(VisualizarAnual));
        }

        [HttpPost]
        public IActionResult FichaCadasto(VisualizarCestaBasicaViewModel viewModel)
        {
            return View(nameof(FichaCadasto),viewModel);
        }

    }
}