using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using In.Cqrs.Query.Queries.Generic;
using Microsoft.EntityFrameworkCore;
using X.PagedList;

namespace In.Cqrs.Query.EfCore
{
    public class GenericQuery<TSource> : IGenericQuery<TSource>
    {
        protected IQueryable<TSource> Queryable;

        internal GenericQuery(IQueryable<TSource> queryable)
        {
            Queryable = queryable;
        }

        public Task<TSource> FirstOrDefaultAsync()
        {
            return Queryable.FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<TSource>> AllAsync()
        {
            return await Queryable.ToArrayAsync();
        }

        public async Task<IPagedList<TSource>> PagedAsync(int pageNumber, int pageSize)
        {
            return await Queryable.ToPagedListAsync(pageNumber, pageSize);
        }

        public async Task<decimal> SumAsync(Expression<Func<TSource, decimal>> expression)
        {
            return await Queryable.SumAsync(expression);
        }
    }
}
