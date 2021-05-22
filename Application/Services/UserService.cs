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
    internal class UserService:IUserService
    {
        private readonly IContext _context;
        private readonly ILogger<UserService> _logger;
        
        public UserService(IContext context, ILogger<UserService> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<ServiceResult> LoginAsync(string username, string password)
        {
            try
            {
                var user = await _context.Users.FirstOrDefaultAsync(x => x.Username == username && x.Password == password);
                if (user is null) return new ServiceResult
                {
                    Type = ServiceResultType.NotFound,
                    Messages = new []{ "Usuário não encontrado"}
                };
                return new ServiceResult<int>
                {
                    Type = ServiceResultType.Success,
                    Result = user.Id
                };

            }
            catch (Exception e)
            {
                _logger.LogError(e, nameof(UserService));
                return new ServiceResult
                {
                    Type = ServiceResultType.InternalError,
                    Messages = new []{ "Erro ao fazer Login"}
                };
            }
        }

        public async Task<ServiceResult> Save(User user)
        {
            try
            {
                return await TrySaveUser(user);
            }
            catch (Exception e)
            {
                _logger.LogError(e, nameof(UserService));
                return new ServiceResult
                {
                    Type = ServiceResultType.InternalError,
                    Messages = new []{ "Erro ao fazer Login"}
                };
            }
            
        }

        private async Task<ServiceResult> TrySaveUser(User user)
        {
            if (user is null)
            {
                return new ServiceResult
                {
                    Type = ServiceResultType.NotFound,
                    Messages = new[]
                    {
                        "Erro ao criar usuário"
                    }
                };
            }

            if (user.Id == default) _context.Users.Add(user);
            else _context.Entry(user).State = EntityState.Modified;

            await _context.SaveChangesAsync();

            return new ServiceResult<int>
            {
                Type = ServiceResultType.Success,
                Result = user.Id
            };
        }

        public async Task<ServiceResult> FindEmailAsync(string email)
        {
            try
            {
                var user = await _context.Users.FirstOrDefaultAsync(x => x.Email == email);
                if (user is null)
                {
                    return new ServiceResult
                    {
                        Type = ServiceResultType.NotFound,
                        Messages = new[] {"Usuário não encontrado"}
                    };
                }
                return new ServiceResult<int>
                {
                    Type = ServiceResultType.Success,
                    Result = user.Id
                };
            }
            catch (Exception e)
            {
                _logger.LogError(e, nameof(UserService));
                return new ServiceResult
                {
                    Type = ServiceResultType.InternalError,
                    Messages = new []{"Email nao cadastrado"}
                };
            }
            
        }
        
    }
}