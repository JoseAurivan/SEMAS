using System.Threading.Tasks;
using Domain.Models;
using Services.DataStructures;

namespace Application.Services.Interfaces
{
    public interface IEnderecoService
    {
        Task<ServiceResult> SaveEndereco(Endereco endereco);
        
    }
}