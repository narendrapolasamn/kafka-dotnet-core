using Post.Query.Core.Queries;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Post.Query.Core.Infrastructure
{
    public interface IQueryDispatcher<TEntity>
    {
        void RegisterHandler<TQuery>(Func<TQuery, Task<List<TEntity>>> handler) where TQuery : BaseQuery;
        Task<List<TEntity>> SendAsync(BaseQuery query);
    }
}