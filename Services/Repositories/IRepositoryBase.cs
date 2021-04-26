using System.Collections;
using System.Threading.Tasks;
using Domain.Interfaces;
using Services.DataStructures;

namespace Services.Repositories
{
    public interface IRepositoryBase<TModel>where TModel:IModelBase
    {
        Task<PaginationResult<TModel>> ListAsync(PaginationParameters pagination);
        Task<TModel> GetAsync(int id);
        void Save(TModel model);
        Task DeleteAsync(int id);
    }
}