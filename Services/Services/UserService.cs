using System;
using System.Threading.Tasks;
using Domain.Models;
using Microsoft.Extensions.Logging;
using Services.DataStructures;
using Services.DataStructures.Interfaces;
using Services.Repositories;
using Services.Services.Interfaces;

namespace Services.Services
{
    internal class UserService:IUserService
    {
        private readonly IUserRepository _repository;
        private readonly ILogger<UserService> _logger;
        
        public UserService(IUserRepository repository, ILogger<UserService> logger)
        {
            _repository = repository;
            _logger = logger;
        }

        public async Task<IServiceResult> LoginAsync(string username, string password)
        {
            try
            {
                var user = await _repository.LoginAsync(username, password);
                if (user is null) return new FailResult()
                {
                    Errors = new []{ "Usuário não encontrado"}
                };
                return new SuccessResult<int>
                {
                    Result = user.Id
                };

            }
            catch (Exception e)
            {
                _logger.LogError(e, nameof(UserService));
                return new FailResult
                {
                    Errors = new []{ "Erro ao fazer Login"}
                };
            }
        }

        public  async Task<IServiceResult> Save(User user)
        {
            try
            {
                if (user is null)
                {
                    return new FailResult()
                    {
                        Errors = new[]
                        {
                            "Erro ao criar usuário"
                        }
                    };
                }
                _repository.Save(user);
                return new SuccessResult<int>
                {
                    Result = user.Id
                };
            }
            catch (Exception e)
            {
                _logger.LogError(e, nameof(UserService));
                return new FailResult
                {
                    Errors = new []{ "Erro ao fazer Login"}
                };
            }
            
        }
    }
}