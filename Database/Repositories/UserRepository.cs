using System.Threading.Tasks;
using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Services.Repositories;

namespace Database.Repositories
{
    internal class UserRepository:RepositoryBase<User>,IUserRepository
    {
        public UserRepository(Context context) : base(context)
        {
        }

        public async Task<bool> CheckOldPassword(string oldPassword, int id)
        {
            return await Set.AnyAsync(x => x.Password == oldPassword && x.Id == id);
        }

        public async Task<User> LoginAsync(string username, string password)
        {
            return await Set.AsNoTracking().
                FirstOrDefaultAsync(x => x.Username == username && x.Password == password);
        }
    }
}