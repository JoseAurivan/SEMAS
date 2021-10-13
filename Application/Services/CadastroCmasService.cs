using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Enums;
using Application.Services.Interfaces;
using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Services.DataStructures;

namespace Application.Services
{
    internal class CadastroCmasService : ICadastroCmasService
    {
        private readonly IContext _context;
        private readonly ILogger<CadastroCmasService> _logger;

        public CadastroCmasService(ILogger<CadastroCmasService> logger, IContext context)
        {
            _logger = logger;
            _context = context;
        }

        public async Task<ServiceResult> SaveCadastro(Pessoa pessoa, CadastroCmas cadastroCmas)
        {
            try
            {
                return await TrySaveCadastro(pessoa, cadastroCmas);
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

        public async Task<ServiceResult> FindCmas(Pessoa pessoa)
        {
            try
            {
                if (pessoa is null)
                {
                    return new ServiceResult(ServiceResultType.NotFound)
                    {
                        Messages = new[]
                        {
                            "Pessoa não cadastrada"
                        }
                    };
                }

                var cadastro = await _context.Pessoas
                    .AsNoTracking()
                    .Include(x => x.CadastroCmas)
                    .FirstOrDefaultAsync(x => x.Id == pessoa.Id);
                
                if (cadastro is null)
                {
                    return new ServiceResult(ServiceResultType.NotFound)
                    {
                        Messages = new[]
                        {
                            "Pessoa não cadastrada no CMAS"
                        }
                    };
                }
                return new ServiceResult<CadastroCmas>(ServiceResultType.Success)
                {
                    Result =  cadastro.CadastroCmas
                };
            }
            catch (Exception e)
            {
                _logger.LogError(e, nameof(UserService));
                return new ServiceResult(ServiceResultType.InternalError)
                {
                    Messages = new[] {"Pessoa não cadastrada no nosso Banco de Dados ou sem cadastro CMAS"}
                };
            }

        }

        public async Task<ServiceResult> ListCadastro()
        {
            try
            {
                var cadastros = await _context.PessoaEnderecos
                    .Include(x => x.Pessoa)
                    .ThenInclude(x => x.CadastroCmas)
                    .Include(x => x.Endereco)
                    .ThenInclude(x => x.Cesta)
                    .ThenInclude(x => x.Entregas)
                    .ToListAsync();

                if (cadastros is null)
                {
                    return new ServiceResult(ServiceResultType.InternalError)
                    {
                        Messages = new []{"Nenhuma Pessoa foi cadastrada na base de dados da SEMAS"}
                    };
                }

                return new ServiceResult<List<PessoaEndereco>>(ServiceResultType.Success)
                {
                    Result = cadastros
                };
            }
            catch(Exception e)
            {
                _logger.LogError(e, nameof(UserService));
                return new ServiceResult(ServiceResultType.InternalError)
                {
                    Messages = new[] {"Pessoa não cadastrada no nosso Banco de Dados ou sem cadastro CMAS"}
                };
            }
        }

        private async Task<ServiceResult> TrySaveCadastro(Pessoa pessoa, CadastroCmas cadastroCmas)
        {
            
            if (pessoa is null || cadastroCmas is null)
            {
                return new ServiceResult(ServiceResultType.NotFound)
                {
                    Messages = new[]
                    {
                        "Erro ao cadastrar pessoa"
                    }
                };
            }
            
            pessoa.CadastroCmas= cadastroCmas;
            if (cadastroCmas.Id == default)  _context.Cadastros.Add(cadastroCmas);
            else _context.Entry(cadastroCmas).State = EntityState.Modified;

            _context.Entry(pessoa).State = EntityState.Modified;
            
            await _context.SaveChangesAsync();
            return new ServiceResult<int>(ServiceResultType.Success)
            {
                Result = cadastroCmas.Id
            };


        }
    }
}