using System.Runtime.Serialization;
using In.Cqrs.Condition.Abstract;

namespace In.Cqrs.Condition
{
    public class PagingContition : IPagingContition
    {
        [DataMember(Name = "page")]
        public int Page { get; set; }
        [DataMember(Name = "count")]
        public int Count { get; set; }
    }
}
