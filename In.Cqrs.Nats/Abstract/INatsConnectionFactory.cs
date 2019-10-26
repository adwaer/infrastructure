using System;
using NATS.Client;

namespace In.Cqrs.Nats.Abstract
{
    public interface INatsConnectionFactory : IDisposable
    {
        IEncodedConnection Get<T>();
    }
}