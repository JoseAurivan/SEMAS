using System.Threading.Tasks;
using Domain.Models;
using Services.DataStructures;

namespace Application.Services.Interfaces
{
    public interface IUserService
    {
        Task<ServiceResult> LoginAsync(string username, string password);
        Task<ServiceResult> Save(User user);
        Task<ServiceResult> FindCpfAsync(string username);
        Task<ServiceResult> FindEmailAsync(string email);
        Task<ServiceResult> CheckOldPasswordAsync(string oldPassword, int id);
        Task<ServiceResult> FindUserAsync(int id);
    }
}