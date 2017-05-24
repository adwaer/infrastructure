using System.Runtime.Serialization;
using In.Cqrs.Query.Criterion.Abstract;

namespace In.Cqrs.Query.Criterion
{
    [DataContract]
    public class PagingCriterion : IPagingCriterion
    {
        [DataMember(Name = "page")]
        public int Page { get; set; }
        [DataMember(Name = "count")]
        public int Count { get; set; }
    }
}
