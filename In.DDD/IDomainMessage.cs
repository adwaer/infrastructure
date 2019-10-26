using System;
using System.Collections.Generic;
using In.Common;

namespace In.DDD
{
    public interface IDomainMessage
    {
        string Type { get; }
        DateTime Created { get; }
        Dictionary<string, Object> Args { get; }
        string CorrelationId { get; set; }
        void Flatten();
    }
}