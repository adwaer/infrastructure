using System;

namespace In.Domain
{
    public interface ILog
    {
        void Error(Exception exception, string logSection = null);
        void Trace(string msg, string logSection = null);
    }
}