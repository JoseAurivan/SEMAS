using System.Threading.Tasks;
using Domain.Models;
using Services.DataStructures;

namespace Application.Services.Interfaces
{
    public interface IPessoaEnderecoService
    {
        Task<ServiceResult> SavePessoa(Pessoa pessoa, Endereco endereco);
        Task<ServiceResult> SearchForCpfAsync(string cpf);
        Task<ServiceResult> FindPessoaAsync(int id);
    }
}