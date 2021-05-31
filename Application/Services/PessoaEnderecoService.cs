using System;
using System.Threading.Tasks;
using Application.Enums;
using Application.Services.Interfaces;
using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Services.DataStructures;

namespace Application.Services
{
    internal class PessoaEnderecoService : IPessoaEnderecoService
    {
        private readonly IContext _context;
        private readonly ILogger<PessoaEnderecoService> _logger;

        public PessoaEnderecoService(IContext context, ILogger<PessoaEnderecoService> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<ServiceResult> SavePessoa(Pessoa pessoa, Endereco endereco)
        {
            try
            {
                return await TrySavePessoa(pessoa, endereco);
            }
            catch (Exception e)
            {
                _logger.LogError(e, nameof(UserService));
                return new ServiceResult(ServiceResultType.InternalError)
                {
                    Messages = new[] {"Erro ao cadastrar pessoa"}
                };
            }
        }

        private async Task<ServiceResult> TrySavePessoa(Pessoa pessoa, Endereco endereco)
        {
            if (pessoa is null || endereco is null)
            {
                return new ServiceResult(ServiceResultType.NotFound)
                {
                    Messages = new[]
                    {
                        "Erro ao cadastrar pessoa"
                    }
                };
            }

            if (pessoa.Id == default) _context.Pessoas.Add(pessoa);
            else _context.Entry(pessoa).State = EntityState.Modified;

            if (endereco.Id == default) _context.Enderecos.Add(endereco);
            else _context.Entry(endereco).State = EntityState.Modified;

            PessoaEndereco pe = new PessoaEndereco();
            pe.Pessoa = pessoa;
            pe.Endereco = endereco;

            _context.PessoaEnderecos.Add(pe);
            
            await _context.SaveChangesAsync();
            return new ServiceResult<int>(ServiceResultType.Success)
            {
                Result = pessoa.Id
            };
        }

        public async Task<ServiceResult> SearchForCpfAsync(string cpf)
        {
            try
            {
                var pessoa = await _context.Pessoas
                    .Include(x =>x.Enderecos)
                    .FirstOrDefaultAsync(x => x.Cpf == cpf);
                if (pessoa is null)
                {
                    return new ServiceResult(ServiceResultType.NotFound)
                    {
                        Messages = new[]
                        {
                            "Pessoa não encontrada."
                        }
                    };
                }

                return new ServiceResult<Pessoa>(ServiceResultType.Success)
                {
                    Result = pessoa
                };
            }
            catch (Exception e)
            {
                _logger.LogError(e, nameof(UserService));
                return new ServiceResult(ServiceResultType.InternalError)
                {
                    Messages = new[] {"CPF não cadastrado"}
                };
            }
        }

        public async Task<ServiceResult> FindPessoaAsync(int id)
        {
            try
            {
                var pessoa = await _context.Pessoas
                    .Include(x =>x.Enderecos)
                    .FirstOrDefaultAsync(x => x.Id == id);
                if (pessoa is null)
                {
                    return new ServiceResult(ServiceResultType.NotFound)
                    {
                        Messages = new[]
                        {
                            "ID da pessoa não encontrada."
                        }
                    };
                }

                return new ServiceResult<Pessoa>(ServiceResultType.Success)
                {
                    Result = pessoa
                };
            }
            catch (Exception e)
            {
                _logger.LogError(e, nameof(UserService));
                return new ServiceResult(ServiceResultType.InternalError)
                {
                    Messages = new[] {"Pessoa não cadastrada"}
                };
            }
        }
    }
}