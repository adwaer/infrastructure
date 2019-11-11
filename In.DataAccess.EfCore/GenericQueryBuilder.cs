using System;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using In.Cqrs.Query.Queries.Generic;
using In.DataAccess.Entity.Abstract;
using In.DataAccess.Repository.Abstract;
using In.Specifications;
using Microsoft.EntityFrameworkCore;

namespace In.DataAccess.EfCore
{
	public class GenericQueryBuilder<TSource> : GenericQuery<TSource>, IGenericQueryBuilder<TSource>
		where TSource : class, IHasKey
	{
		private readonly IConfigurationProvider _mapperConfiguration;

		private GenericQueryBuilder(IQueryable<TSource> queryable, IConfigurationProvider mapperConfigurationProvider)
			: base(queryable)
		{
			_mapperConfiguration = mapperConfigurationProvider;
		}

		[SuppressMessage("ReSharper", "UnusedMember.Global")]
		public GenericQueryBuilder(ILinqProvider linqProvider, IConfigurationProvider mapperConfigurationProvider)
			: base(linqProvider.GetQuery<TSource>())
		{
			_mapperConfiguration = mapperConfigurationProvider;
		}

		public IGenericQuery<TDest> ProjectTo<TDest>()
			where TDest : class
		{
			return new GenericQuery<TDest>(Queryable.ProjectTo<TDest>(_mapperConfiguration));
		}

		public IGenericQueryBuilder<TDest> SwitchEntity<TDest>(Expression<Func<TSource, TDest>> switchExpression)
			where TDest : class, IHasKey
		{
			var queryable = Queryable.Select(switchExpression);
			return new GenericQueryBuilder<TDest>(queryable, _mapperConfiguration);
		}

		public IGenericQuery<TDest> Select<TDest>(Expression<Func<TSource, TDest>> selector)
		{
			var queryable = Queryable.Select(selector);
			return new GenericQuery<TDest>(queryable);
		}

		public IGenericQueryBuilder<TSource> Where(Specification<TSource> specification)
		{
			Queryable = Queryable.Where(specification);
			return this;
		}

		public IGenericQueryBuilder<TSource> OrderBy<TKey>(Expression<Func<TSource, TKey>> keySelector,
			bool descending = false)
		{
			Queryable = descending ? Queryable.OrderByDescending(keySelector) : Queryable.OrderBy(keySelector);
			return this;
		}

		public IGenericQueryBuilder<TSource> ThenBy<TKey>(Expression<Func<TSource, TKey>> keySelector,
			bool descending = false)
		{
			var quaryable = (IOrderedQueryable<TSource>) Queryable;
			Queryable = descending ? quaryable.ThenByDescending(keySelector) : quaryable.ThenBy(keySelector);
			return this;
		}

		public IGenericQueryBuilder<TResult> OfType<TResult>()
			where TResult : class, TSource
		{
			var queryable = Queryable.OfType<TResult>();
			return new GenericQueryBuilder<TResult>(queryable, _mapperConfiguration);
		}

		public async Task<int> CountAsync()
		{
			return await Queryable.CountAsync();
		}

		public Task<TSource> MinAsync()
		{
			return Queryable.MinAsync();
		}

		public Task<TSource> MaxAsync()
		{
			return Queryable.MaxAsync();
		}
	}
}