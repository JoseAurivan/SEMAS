using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Services.Repositories;

namespace Database.Repositories
{
    internal class UnitOfWork:IUnitOfWork
    {
        private readonly Context _context;
        private readonly ILogger<UnitOfWork> _logger;

        public UnitOfWork(Context context, ILogger<UnitOfWork> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<bool> SaveChangesAsync()
        {
            try
            {
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception e)
            {
                _logger.LogError(e, nameof(UnitOfWork));
                return false;
            }
             
        }
    }
}