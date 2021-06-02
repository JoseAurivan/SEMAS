using System.Threading.Tasks;
using Domain.Models;
using Services.DataStructures;

namespace Application.Services.Interfaces
{
    public interface ICadastroCmasService
    {
        Task<ServiceResult> SaveCadastro(Pessoa pessoa, CadastroCmas cadastroCmas);
    }
}