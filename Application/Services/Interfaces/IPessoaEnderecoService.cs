using System.Threading.Tasks;
using Domain.Enums;
using Domain.Models;
using Services.DataStructures;

namespace Application.Services.Interfaces
{
    public interface IPessoaEnderecoService
    {
        Task<ServiceResult> SavePessoa(Pessoa pessoa, Endereco endereco);
        Task<ServiceResult> SearchForCpfAsync(string cpf);
        Task<ServiceResult> FindPessoaAsync(int id);
        Task<ServiceResult> GetPessoaAsync(int id);
        Task<ServiceResult> SaveCestaBasica( Endereco endereco, CestaBasica cestaBasica);
        Task<ServiceResult> SearchForEndereco(int idPessoa);
        Task<ServiceResult> SearchForCpfPessoaEnderecoAsync(string cpf);
        Task<ServiceResult> SavePessoaEndereco(PessoaEndereco pessoaEndereco);
        Task<ServiceResult> SearchForPessoaEndereco(int idPessoa);
        Task<ServiceResult> SearchForCestaBasica(int idCestaBasica);
        Task<ServiceResult> UpdateCestaBasica(CestaBasica cestaBasica);
        Task<ServiceResult> ListControl(Unidade unidade, int mes, int ano);


    }
}