using System.Collections;
using System.Collections.Generic;
using Domain.Interfaces;

namespace Services.DataStructures
{
    public struct PaginationResult<TModel> where TModel : IModelBase
    {
        public PaginationParameters Pagination { get; set; }
        public IEnumerable<TModel> Models { get; set; }
        public int Total { get; set; }
    }
}