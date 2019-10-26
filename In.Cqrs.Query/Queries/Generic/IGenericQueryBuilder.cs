using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using In.DataAccess.Entity.Abstract;
using In.Specifications;
using X.PagedList;

namespace In.Cqrs.Query.Queries.Generic
{
	public interface IGenericQueryBuilder<TSource> where TSource : class, IHasKey
	{
		IGenericQuery<TDest> ProjectTo<TDest>()
			where TDest : class;

		/// <summary>
		/// Switches query building to another source entity specified by <paramref name="switchExpression"/>
		/// </summary>
		/// <typeparam name="TDest">The type of destination entity to switch to</typeparam>
		/// <param name="switchExpression">Expression specifying how to access destination entity from 
		/// current query source entity</param>
		/// <returns></returns>
		IGenericQueryBuilder<TDest> SwitchEntity<TDest>(Expression<Func<TSource, TDest>> switchExpression)
			where TDest : class, IHasKey;

		/// <summary>
		/// Selects the final query target.
		/// </summary>
		/// <typeparam name="TDest">The type of final query target</typeparam>
		/// <param name="selector"></param>
		/// <returns></returns>
		IGenericQuery<TDest> Select<TDest>(Expression<Func<TSource, TDest>> selector);

		IGenericQueryBuilder<TSource> Where(Specification<TSource> specification);

		IGenericQueryBuilder<TSource> OrderBy<TKey>(Expression<Func<TSource, TKey>> keySelector,
			bool descending = false);

		IGenericQueryBuilder<TSource> ThenBy<TKey>(Expression<Func<TSource, TKey>> keySelector,
			bool descending = false);

		IGenericQueryBuilder<TResult> OfType<TResult>()
			where TResult : class, TSource;

		Task<int> CountAsync();
		Task<TSource> MinAsync();
		Task<TSource> MaxAsync();
		Task<TSource> FirstOrDefaultAsync();
		Task<IEnumerable<TSource>> AllAsync();
		Task<IPagedList<TSource>> PagedAsync(int pageNumber, int pageSize);
	}
}