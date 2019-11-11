using System;
using System.Collections.Generic;
using Microsoft.Extensions.DependencyInjection;

namespace In.Common.Implementations
{
    public class ServiceLocator : IDiScope
    {
        private readonly IServiceProvider _serviceProvider;

        public ServiceLocator(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public TSvc Resolve<TSvc>()
        {
            return _serviceProvider.GetRequiredService<TSvc>();
        }

        public object Resolve(Type type)
        {
            return _serviceProvider.GetService(type);
        }

        public IEnumerable<T> ResolveAll<T>()
        {
            return _serviceProvider.GetServices<T>();
        }
    }
}