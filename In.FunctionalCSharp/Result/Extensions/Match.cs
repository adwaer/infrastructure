using System;

namespace In.FunctionalCSharp
{
    public static partial class ResultExtensions
    {
        public static K Match<T, K, E>(this Result<T, E> result, Func<T, K> onSuccess, Func<E, K> onFailure)
        {
            return result.IsSuccess
                ? onSuccess(result.Value)
                : onFailure(result.Error);
        }

        public static K Match<K, T>(this Result<T> result, Func<T, K> onSuccess, Func<string, K> onFailure)
        {
            return result.IsSuccess
                ? onSuccess(result.Value)
                : onFailure(result.Error);
        }

        public static T Match<T>(this Result result, Func<T> onSuccess, Func<string, T> onFailure)
        {
            return result.IsSuccess
                ? onSuccess()
                : onFailure(result.Error);
        }

        public static void Match<T, E>(this Result<T, E> result, Action<T> onSuccess, Action<E> onFailure)
        {
            if (result.IsSuccess)
                onSuccess(result.Value);
            else
                onFailure(result.Error);
        }

        public static void Match<T>(this Result<T> result, Action<T> onSuccess, Action<string> onFailure)
        {
            if (result.IsSuccess)
                onSuccess(result.Value);
            else
                onFailure(result.Error);
        }

        public static void Match(this Result result, Action onSuccess, Action<string> onFailure)
        {
            if (result.IsSuccess)
                onSuccess();
            else
                onFailure(result.Error);
        }
    }
}
