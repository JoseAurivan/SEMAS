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
    internal class UserService : IUserService
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
            
            _logger.LogInformation("Tentando acessar login");
            try
            {
                var user = await _context.Users.FirstOrDefaultAsync(x =>
                    x.Username == username && x.Password == password);

                if (user is null)
                    return new ServiceResult(ServiceResultType.NotFound)
                    {
                        Messages = new[] {"Usuário não encontrado"}
                    };
                _logger.LogInformation("Login foi um sucesso: usuario {@user} foi logado no sistema",user);
                return new ServiceResult<User>(ServiceResultType.Success)
                {
                    Result = user
                };

            }
            catch (Exception e)
            {
                _logger.LogError(e, nameof(UserService));
                return new ServiceResult(ServiceResultType.InternalError)
                {
                    Messages = new[] {"Erro ao fazer Login"}
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
                return new ServiceResult(ServiceResultType.InternalError)
                {
                    Messages = new[] {"Erro ao fazer Login"}
                };
            }

        }
        

        private async Task<ServiceResult> TrySaveUser(User user)
        {
            if (user is null)
            {
                return new ServiceResult(ServiceResultType.NotFound)
                {
                    Messages = new[]
                    {
                        "Erro ao criar usuário"
                    }
                };
            }

            if (user.Id == default) _context.Users.Add(user);
            else _context.Entry(user).State = EntityState.Modified;

            await _context.SaveChangesAsync();

            return new ServiceResult<int>(ServiceResultType.Success)
            {
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
                    return new ServiceResult(ServiceResultType.NotFound)
                    {
                        Messages = new[] {"Usuário não encontrado"}
                    };
                }

                return new ServiceResult<int>(ServiceResultType.Success)
                {
                    Result = user.Id
                };
            }
            catch (Exception e)
            {
                _logger.LogError(e, nameof(UserService));
                return new ServiceResult(ServiceResultType.InternalError)
                {
                    Messages = new[] {"Email nao cadastrado"}
                };
            }

        }

        public async Task<ServiceResult> CheckOldPasswordAsync(string oldPassword, int id)
        {
            try
            {
                var task = await _context.Users.AnyAsync(x => x.Password == oldPassword && x.Id == id);
                if (task)
                {
                    return new ServiceResult<int>(ServiceResultType.Success)
                    {
                        Result = id
                    };
                }

                return new ServiceResult(ServiceResultType.InternalError)
                {
                    Messages = new[] {"Não foi possivel alterar a senha"}
                };
            }
            catch (Exception e)
            {
                _logger.LogError(e, nameof(UserService));
                return new ServiceResult(ServiceResultType.InternalError)
                {
                    Messages = new[] {"Email nao cadastrado"}
                };
            }
        }

        public async Task<ServiceResult> FindUserAsync(int id)
        {
            try
            {
                var user = await _context.Users.FirstOrDefaultAsync(x => x.Id == id);
                if (user is null)
                {
                    return new ServiceResult(ServiceResultType.NotFound)
                    {
                        Messages = new[] {"Usuário não encontrado"}
                    };
                }

                return new ServiceResult<User>(ServiceResultType.Success)
                {
                    Result = user
                };
            }
            catch (Exception e)
            {
                _logger.LogError(e, nameof(UserService));
                return new ServiceResult(ServiceResultType.InternalError)
                {
                    Messages = new[] {"Usuario nao cadastrado"}
                };
            }

        }

        public async Task<ServiceResult> FindCpfAsync(string username)
        {
            _logger.LogInformation("Buscando pelo CPF: "+username);
            try
            {
                var user = await _context.Users.FirstOrDefaultAsync(x => x.Username == username);
                if (user is null)
                {
                    return new ServiceResult(ServiceResultType.NotFound)
                    {
                        Messages = new[] {"Usuário não encontrado"}
                    };
                }
                _logger.LogInformation("CPF ("+username+") encontrado");
                return new ServiceResult<User>(ServiceResultType.Success)
                {
                    Result = user
                };
            }
            catch (Exception e)
            {
                _logger.LogError(e, nameof(UserService));
                return new ServiceResult(ServiceResultType.InternalError)
                {
                    Messages = new[] {"CPF nao cadastrado"}
                };
            }
        }
    }
}
