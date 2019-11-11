using System;

namespace In.Logs
{
    /// <summary>
    /// Logger service
    /// </summary>
    public interface ILogService
    {
        /// <summary>
        /// Write Debug
        /// </summary>
        /// <param name="category"></param>
        /// <param name="msg"></param>
        void LogDebug(string category, string msg);
        void LogDebug<T>(string msg);
        void LogInfo(string category, string msg);
        void LogInfo<T>(string msg);
        void LogError(string category, Exception exception, string message);
        void LogError<T>(Exception exception, string message);
        void LogError(string category, Exception exception);
        void LogError<T>(Exception exception);
        void LogError(string category, Exception exception, string template, string message, params object[] args);
        void LogError<T>(Exception exception, string template, string message, params object[] args);
        void LogError(string category, string text, params object[] data);
        void LogError<T>(string text, params object[] data);
    }
}