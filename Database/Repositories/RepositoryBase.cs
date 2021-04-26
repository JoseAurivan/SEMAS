using System.Linq;
using System.Threading.Tasks;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using Services.DataStructures;
using Services.Repositories;

namespace Database.Repositories
{
    internal abstract class RepositoryBase<TModel>:IRepositoryBase<TModel> where TModel:class, IModelBase
    {
        protected readonly Context Context;
        protected readonly DbSet<TModel> Set;

        protected RepositoryBase(Context context)
        {
            Context = context;
            Set = Context.Set<TModel>();
        }

        public async Task<PaginationResult<TModel>> ListAsync(PaginationParameters pagination)
        {

            var list = await Set.AsNoTracking()
                .OrderBy(x => x.Id)
                .Skip((pagination.Page - 1) * pagination.PerPage)
                .Take(pagination.PerPage)
                .ToListAsync();
            return new PaginationResult<TModel>
            {
                Models = list,
                Pagination = pagination,
                Total = await Set.CountAsync()
            };

        }

        public async Task<TModel> GetAsync(int id)
        {
            return await Set.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
        }

        public void Save(TModel model)
        {
            if (model.Id == default) Context.Add(model);
            else Context.Entry(model).State = EntityState.Modified;
        }

        public async Task DeleteAsync(int id)
        {
            var model = await Set.FirstOrDefaultAsync(x => x.Id == id);
            if (model is null) return;

            Set.Remove(model);
        }
    }
}