using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
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
            return _serviceProvider
                .GetRequiredService<IHttpContextAccessor>()
                .HttpContext.RequestServices.GetService<TSvc>();
        }

        public object Resolve(Type type)
        {
            return _serviceProvider
                .GetRequiredService<IHttpContextAccessor>()
                .HttpContext.RequestServices.GetService(type);
        }

        public IEnumerable<T> ResolveAll<T>()
        {
            return _serviceProvider
                .GetRequiredService<IHttpContextAccessor>()
                .HttpContext.RequestServices.GetServices<T>();
        }
    }
}