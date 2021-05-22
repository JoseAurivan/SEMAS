using System.Threading.Tasks;
using Domain.Models;
using Services.DataStructures;

namespace Application.Services.Interfaces
{
    public interface IUserService
    {
        Task<ServiceResult> LoginAsync(string username, string password);
        Task<ServiceResult> Save(User user);

        Task<ServiceResult> FindEmailAsync(string email);
    }
}