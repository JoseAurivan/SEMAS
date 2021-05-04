using System.Threading.Tasks;
using Domain.Models;

namespace Services.Repositories
{
    public interface IUserRepository:IRepositoryBase<User>
    {
        Task<bool> CheckOldPassword(string oldPassword, int id);

        Task<User> LoginAsync(string username, string password);

    }
}