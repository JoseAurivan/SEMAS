using Application.Services;
using Application.Services.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace Application
{
    public static class DependencyInjectionExtensions
    {
        public static IServiceCollection AddServices(this IServiceCollection serviceCollection)
        {
            serviceCollection
                .AddScoped<IUserService, UserService>()
                .AddScoped<IEmailService, EmailService>()
                .AddScoped<IPessoaEnderecoService, PessoaEnderecoService>()
                .AddScoped<ICadastroCmasService, CadastroCmasService>();
            
            return serviceCollection;
        }
    }
}