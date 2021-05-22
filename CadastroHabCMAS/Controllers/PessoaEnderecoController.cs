using CadastroHabCMAS.ViewModel.PessoaEnderecoViewModel;
using Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace CadastroHabCMAS.Controllers
{
    public class PessoaEnderecoController : Controller
    {
        
        public IActionResult Cadastro()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Cadastro(PessoaEnderecoAddViewModel pessoaEndereco)
        {
            var pessoa = pessoaEndereco.ToModelPessoa(pessoaEndereco.Nome, pessoaEndereco.Cpf, pessoaEndereco.Rg,
                pessoaEndereco.Telefone, pessoaEndereco.Email, pessoaEndereco.Sexo);
            var endereco = pessoaEndereco.ToModelEndereco(pessoaEndereco.Estado, pessoaEndereco.Cidade,
                pessoaEndereco.Cep, pessoaEndereco.Bairro, pessoaEndereco.Complemento);
            PessoaEndereco pe = new PessoaEndereco();
            pe.Pessoa = pessoa;
            pe.Endereco = endereco;
            
            
            //TODO operacoes no banco
            return View();
        }
        
    }
}