using System;
using System.Collections.Generic;
using In.Common.Exceptions;

namespace In.FunctionalCSharp
{
    public static class ParametersValidation
    {
        public static void ThrowIfNullArg(object value, string name)
        {
            if (value == null)
                throw new BadRequestException(name);
        }

        public static void ThrowIfEmptyOrWhitespace(string value, string name)
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new BadRequestException($"parameter should be a not empty string", name);
        }

        public static Result[] Validate(params Result[] validators)
        {
            return validators;
        }

        public static Result NotNull(params (object value, string name)[] parameters)
        {
            var results = new List<Result>();

            foreach (var param in parameters)
            {
                Result res;
                if (param.value == null)
                    res = Result.Failure($"parameter {param.name} should be not null");
                else res = Result.Success();
                results.Add(res);
            }

            return Result.Combine(results.ToArray());
        }

        public static Result NotNullOrWhiteSpace(string value, string name)
        {
            if (String.IsNullOrWhiteSpace(value))
                return Result.Failure($"parameter {name} should contain not empty string");
            return Result.Success();
        }

        public static Result NotDefaultValue(int value, string name)
        {
            if (value == default(int))
                return Result.Failure($"parameter {name} should be not a default value ({value})");
            return Result.Success();
        }

        public static Result NotDefaultValue(long value, string name)
        {
            if (value == default(long))
                return Result.Failure($"parameter {name} should be not a default value ({value})");
            return Result.Success();
        }

        public static Result NotDefaultValue(Guid value, string name)
        {
            if (value == Guid.Empty)
                return Result.Failure($"parameter {name} should be not a default value ({value})");
            return Result.Success();
        }

        public static Result NotDefaultValue(DateTimeOffset value, string name)
        {
            if (value == default(DateTimeOffset))
                return Result.Failure($"parameter {name} should be not a default value ({value})");
            return Result.Success();
        }

        public static Result NotDefaultValue(decimal value, string name)
        {
            if (value == default(decimal))
                return Result.Failure($"parameter {name} should be not a default value ({value})");
            return Result.Success();
        }

        public static Result GreaterOrEqual(int value, int valueToCompare, string name)
        {
            if (value < valueToCompare)
                return Result.Failure($"parameter {name} should be greater or equal to {valueToCompare}");
            return Result.Success();
        }

        public static Result GreaterOrEqual(long value, int valueToCompare, string name)
        {
            if (value < valueToCompare)
                return Result.Failure($"parameter {name} should be greater or equal to {valueToCompare}");
            return Result.Success();
        }

        public static Result GreaterOrEqual(decimal value, int valueToCompare, string name)
        {
            if (value < valueToCompare)
                return Result.Failure($"parameter {name} should be greater or equal to {valueToCompare}");
            return Result.Success();
        }

        public static Result NotNull(object value, string name)
        {
            if (value == null)
                return Result.Failure($"parameter {name} should be not null");
            return Result.Success();
        }

        public static Result Ensure<T>(T value, Func<T, bool> predicate, string name, string errorText = null)
        {
            if (!predicate(value))
                return Result.Failure(errorText ?? $"parameter {name} did not pass parameter validation");
            return Result.Success();
        }

        public static Result Ensure(Func<bool> predicate, string name, string errorText = null)
        {
            if (!predicate())
                return Result.Failure(errorText ?? $"parameter {name} did not pass parameter validation");
            return Result.Success();
        }
    }
}
