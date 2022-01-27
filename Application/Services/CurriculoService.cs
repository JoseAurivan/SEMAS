using System;
using System.Threading.Tasks;
using Application.Enums;
using Application.Services.Interfaces;
using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Serilog;
using Services.DataStructures;

namespace Application.Services
{
    public class CurriculoService:ICurriculoService
    {
        private readonly IContext _context;
        private readonly ILogger<CurriculoService> _logger;

        public CurriculoService(ILogger<CurriculoService> logger, IContext context)
        {
            _logger = logger;
            _context = context;
        }

        public async Task<ServiceResult> SalvarCurriculo(Curriculo curriculo, Certificado certificado, Experiencias experiencias)
        {
            try
            {
                return await TentarSalvarCurriculo(curriculo,certificado,experiencias);
            }
            catch (Exception e)
            {
                _logger.LogError(e, nameof(UserService));
                return new ServiceResult(ServiceResultType.InternalError)
                {
                    Messages = new[] {"Erro ao salvar usuário"}
                };
            }
        }
        private async Task<ServiceResult> TentarSalvarCurriculo(Curriculo curriculo, Certificado certificado, Experiencias experiencias)
        {
            if (curriculo is null || certificado is null || experiencias is null)
            {
                return new ServiceResult(ServiceResultType.NotFound)
                {
                    Messages = new[]
                    {
                        "Erro ao cadastrar curriculo"
                    }
                };
            }
            
            
            if (certificado.Id == default) _context.Certificados.Add(certificado);
            else _context.Entry(certificado).State = EntityState.Modified;
            
            if (experiencias.Id == default) _context.Experiencias.Add(experiencias);
            else _context.Entry(experiencias).State = EntityState.Modified;
            
            if (curriculo.Id == default) _context.Curriculos.Add(curriculo);
            else _context.Entry(certificado).State = EntityState.Modified;

            await _context.SaveChangesAsync();

            return new ServiceResult<int>(ServiceResultType.Success)
            {
                Result = curriculo.Id
            };
        }


    }
}