#nullable enable
using System;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using In.Common;
using In.Common.Exceptions;
using In.DataAccess.Repository.Abstract;
using In.FunctionalCSharp;
using Newtonsoft.Json.Linq;

namespace In.Cqrs.Command.Simple
{
    [SuppressMessage("ReSharper", "ClassNeverInstantiated.Global")]
    public class SimpleMsgBus : IMessageSender
    {
        private readonly IDiScope _diScope;
        private readonly IRepository<IMessageResult> _crudUow;

        public SimpleMsgBus(IDiScope diScope, IRepository<IMessageResult> crudUow)
        {
            _diScope = diScope;
            _crudUow = crudUow;
        }

        public Result Send<T>(T command) where T : IMessage
        {
            return AsyncHelpers.RunSync(() => SendAsync(command));
        }

        public async Task<Result> SendAsync(IMessage command)
        {
            var cmdType = command.GetType();

            var openGenericType = typeof(ICommandHandler<>);
            var closedGenericType = openGenericType.MakeGenericType(cmdType);

            var handler = _diScope.Resolve(closedGenericType);

            return await Execute(command, async () =>
            {
                var methods = handler
                    .GetType()
                    .GetTypeInfo()
                    .GetDeclaredMethods("Handle");

                foreach (var method in methods)
                {
                    var contains = method.GetParameters()
                        .FirstOrDefault()
                        ?.ToString()
                        .Contains(cmdType.ToString());

                    if (contains == true)
                    {
                        var handlerCall = method.Invoke(handler, new object?[] {command});
                        if (handlerCall == null)
                        {
                            throw new InternalException("Can't find handler method");
                        }
                        return await (Task<Result>) handlerCall;
                    }
                }

                return Result.Failure("Handler no found");
            });
        }

        public async Task<Result> SendAsync<TInput>(TInput command) where TInput : IMessage
        {
            return await Execute(command, async () =>
            {
                var handler = _diScope.Resolve<ICommandHandler<TInput>>();
                return await handler.Handle(command);
            });
        }

        public async Task<Result<TOutput>> SendAsync<TInput, TOutput>(TInput command) where TInput : IMessage
        {
            return await Execute(command, async () =>
            {
                var handler = _diScope.Resolve<ICommandHandler<TInput, TOutput>>();
                return await handler.Handle(command);
            });
        }

        #region private

        private async Task<Result<TOutput>> Execute<TOutput>(IMessage command, Func<Task<Result<TOutput>>> func)
        {
            try
            {
                var result = await func();

                var messageResult = GetLogFromResult(command, result);
                await SaveCommand(messageResult);
                return result;
            }
            catch (Exception ex)
            {
                var messageResult = GetLogFromError(command, ex);
                await SaveCommand(messageResult);
                return Result.Failure<TOutput>(ex.Message);
            }
        }

        private async Task<Result> Execute(IMessage command, Func<Task<Result>> func)
        {
            try
            {
                var result = await func();

                var messageResult = GetLogFromResult(command, result);
                await SaveCommand(messageResult);
                return result;
            }
            catch (Exception ex)
            {
                var messageResult = GetLogFromError(command, ex);
                await SaveCommand(messageResult);
                return Result.Failure(ex.Message);
            }
        }

        private Task SaveCommand(IMessageResult msgResult)
        {
            _crudUow.Add(msgResult);
            return _crudUow.Save();
        }

        private IMessageResult GetLogFromError(IMessage command, Exception ex)
        {
            var msgResult = _diScope.Resolve<IMessageResult>();
            msgResult.Body = JObject.FromObject(command).ToString();
            msgResult.Type = command.GetType().ToString();
            msgResult.Succeed = false;
            msgResult.Info = ex.Message;

            return msgResult;
        }

        private IMessageResult GetLogFromResult(IMessage command, Result result)
        {
            var msgResult = _diScope.Resolve<IMessageResult>();
            msgResult.Body = JObject.FromObject(command).ToString();
            msgResult.Type = command.GetType().ToString();
            msgResult.Succeed = result.IsSuccess;

            if (result.IsFailure) msgResult.Info = result.Error;

            return msgResult;
        }

        #endregion
    }
}