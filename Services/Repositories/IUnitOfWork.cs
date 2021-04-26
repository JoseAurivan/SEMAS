using System.Threading.Tasks;

namespace Services.Repositories
{
    public interface IUnitOfWork
    {
        Task<bool> SaveChangesAsync();
    }
}