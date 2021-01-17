﻿using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Logging;

namespace Web.Infrastructure
{
    public static class IocExtensions
    {
        public static IApplicationBuilder UseUnhandledExceptionLogger(this IApplicationBuilder builder,
            ILoggerFactory loggerFactory)
        {
            if (builder == null)
                throw new ArgumentNullException(nameof(builder));
            if (loggerFactory == null)
                throw new ArgumentNullException(nameof(loggerFactory));

            var logger = loggerFactory.CreateLogger("UnhandledExceptionLogger");
            TaskScheduler.UnobservedTaskException += (s, e) =>
            {
                logger.LogCritical(e.Exception.Flatten(), "Unobserved task exception, {exceptionType}",
                    e.Exception.GetType());
                e.SetObserved();
            };
            AppDomain.CurrentDomain.UnhandledException +=
                (s, e) => logger.LogCritical(e.ExceptionObject as Exception, "Unhandled exception, {exceptionType}",
                    e.ExceptionObject.GetType());

            return builder;
        }
    }
}