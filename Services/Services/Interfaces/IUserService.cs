using System.Threading.Tasks;
using Domain.Models;
using Services.DataStructures.Interfaces;

namespace Services.Services.Interfaces
{
    public interface IUserService
    {
        Task<IServiceResult> LoginAsync(string username, string password);
    }
}