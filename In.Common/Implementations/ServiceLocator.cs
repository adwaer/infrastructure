using System;
using System.Collections.Generic;
using Microsoft.Extensions.DependencyInjection;

namespace In.Common.Implementations
{
    public class ServiceLocator : IDiScope
    {
        private readonly IServiceProvider _serviceProvider;
        private IServiceScope? _scope;

        public ServiceLocator(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }
        
        public void CreateScope()
        {
            _scope = _serviceProvider.CreateScope();
        }
    
        public void DropScope()
        {
            _scope?.Dispose();
        }

        public TSvc Resolve<TSvc>()
        {
            if (_scope != null)
            {
                return _scope.ServiceProvider.GetService<TSvc>();
            }
        
            using var scope = _serviceProvider.CreateScope();

            return scope.ServiceProvider.GetService<TSvc>();
        }

        public object Resolve(Type type)
        {
            if (_scope != null)
            {
                return _scope.ServiceProvider.GetService(type);
            }
        
            using var scope = _serviceProvider.CreateScope();
        
            return scope.ServiceProvider.GetService(type);
        }

        public IEnumerable<T> ResolveAll<T>()
        {
            if (_scope != null)
            {
                return _scope.ServiceProvider.GetServices<T>();
            }
        
            using var scope = _serviceProvider.CreateScope();
        
            return scope.ServiceProvider.GetServices<T>();
        }
    }
}