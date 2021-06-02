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
            cadastroCmas.Pessoa = pessoa;

            _context.Cadastros.Add(cadastroCmas);

            await _context.SaveChangesAsync();
            return new ServiceResult<int>(ServiceResultType.Success)
            {
                Result = cadastroCmas.Id
            };


        }
    }
}