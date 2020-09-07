using System;
using In.Common.Exceptions;

namespace In.FunctionalCSharp
{
    public partial struct Result
    {
        public Result<K> ConvertFailure<K>()
        {
            if (IsSuccess)
                throw new BadRequestException(Messages.ConvertFailureExceptionOnSuccess);

            return Failure<K>(Error);
        }
    }

    public partial struct Result<T>
    {
        public Result ConvertFailure()
        {
            if (IsSuccess)
                throw new BadRequestException(Result.Messages.ConvertFailureExceptionOnSuccess);

            return Result.Failure(Error);
        }

        public Result<K> ConvertFailure<K>()
        {
            if (IsSuccess)
                throw new BadRequestException(Result.Messages.ConvertFailureExceptionOnSuccess);

            return Result.Failure<K>(Error);
        }
    }

    public partial struct Result<T, E>
    {
        public Result<K, E> ConvertFailure<K>()
        {
            if (IsSuccess)
                throw new BadRequestException(Result.Messages.ConvertFailureExceptionOnSuccess);

            return Result.Failure<K, E>(Error);
        }
    }
}
