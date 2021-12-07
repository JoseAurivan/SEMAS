using Application.Services.Interfaces;
using Domain.Models;
using Microsoft.Extensions.Logging;
using Serilog;

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
    }
}