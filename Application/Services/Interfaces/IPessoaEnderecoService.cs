using System.Threading.Tasks;
using Domain.Enums;
using Domain.Models;
using Services.DataStructures;

namespace Application.Services.Interfaces
{
    public interface IPessoaEnderecoService
    {
        Task<ServiceResult> SavePessoa(Pessoa pessoa, Endereco endereco, string? username );
        Task<ServiceResult> SearchForCpfAsync(string cpf);
        Task<ServiceResult> FindPessoaAsync(int id);
        Task<ServiceResult> GetPessoaAsync(int id);
        Task<ServiceResult> SaveCestaBasica( Endereco endereco, CestaBasica cestaBasica, string? username);
        Task<ServiceResult> SearchForEndereco(int idPessoa);
        Task<ServiceResult> SearchForCpfPessoaEnderecoAsync(string cpf);
        Task<ServiceResult> SavePessoaEndereco(PessoaEndereco pessoaEndereco, string? username);
        Task<ServiceResult> SearchForPessoaEndereco(int idPessoa);
        Task<ServiceResult> SearchForCestaBasica(int idCestaBasica);
        Task<ServiceResult> UpdateCestaBasica(CestaBasica cestaBasica, string? username);
        Task<ServiceResult> ListControlMonth(Unidade unidade, int mes, int ano);
        Task<ServiceResult> ListControlAllMonth(int mes, int ano);
        Task<ServiceResult> ListControlYear(Unidade unidade, int ano);
        Task<ServiceResult> ListControlAllYear(int ano);
   



    }
}