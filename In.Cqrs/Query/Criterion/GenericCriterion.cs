using In.Cqrs.Query.Criterion.Abstract;

namespace In.Cqrs.Query.Criterion
{
    public class GenericCriterion<T> : IGenericCriterion<T>
    {
        public T Value { get; set; }
        public GenericCriterion(T value)
        {
            Value = value;
        }
    }
}
