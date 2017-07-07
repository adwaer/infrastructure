using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace In.Cqrs.Query
{
    public interface ISingleQueryResult<T>
    {
        T Data { get; set; }
    }

    public class SingleQueryResult<T> : ISingleQueryResult<T>
    {
        public T Data { get; set; }
    }
}
