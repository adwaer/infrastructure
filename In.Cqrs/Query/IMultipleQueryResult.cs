using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace In.Cqrs.Query
{
    public interface IMultipleQueryResult<T>
    {
        T[] Data { get; set; }
    }

    public class MultipleQueryResult<T> : IMultipleQueryResult<T>
    {
        public T[] Data { get; set; }
    }
}
