using System.Linq;
using In.Cqrs.Query.Criterion.Abstract;
using In.Entity.Uow;

namespace In.Cqrs.Query
{
    public class ExpressionQuery
    {
        private readonly IDataSetUow _dataSetUow;

        public ExpressionQuery(IDataSetUow dataSetUow)
        {
            _dataSetUow = dataSetUow;
        }

        public IQueryable<T> Ask<T>(IExpressionCriterion<T> criterion) where T : class
        {
            return _dataSetUow
                .Query<T>()
                .Where(criterion.Get());
        }
    }
}