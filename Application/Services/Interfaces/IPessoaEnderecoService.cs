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
        Task<ServiceResult> SaveCestaBasica( Endereco endereco, CestaBasica cestaBasica);
        Task<ServiceResult> SearchForEndereco(int idPessoa);
        //TODO Achar Endereco pela Pessoa
    }
}