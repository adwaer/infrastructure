using System;
using System.Threading.Tasks;
using In.Cqrs.Events;
using In.Cqrs.Uow;
using In.Domain;

namespace In.Cqrs.Command
{
    public class DeleteCommandHandler<TEntity> : IMsgHandler<TEntity>
        where TEntity : class, IEntity, IMessage
    {
        private readonly IDataSetUow _dataSetUow;
        //private readonly ISubscriber<TEntity>[] _subscribers;

        public DeleteCommandHandler(IDataSetUow dataSetUow/*, ISubscriber<TEntity>[] subscribers*/)
        {
            _dataSetUow = dataSetUow;
            //_subscribers = subscribers;
        }


        //private void BeforeDelete(TEntity id)
        //{
        //    if (_subscribers == null || _subscribers.Length == 0) { return; }

        //    foreach (var subscriber in _subscribers)
        //    {
        //        subscriber.BeforeDelete(id);
        //    }
        //}
        //private void AfterDelete(TEntity id)
        //{
        //    if (_subscribers == null || _subscribers.Length == 0) { return; }

        //    foreach (var subscriber in _subscribers)
        //    {
        //        subscriber.AfterDelete(id);
        //    }
        //}

        public string Handle(TEntity message)
        {
            var entity = _dataSetUow.Find<TEntity>(message);
            if (entity == null)
            {
                throw new ArgumentException($"Entity {typeof(TEntity).Name} with id={message} doesn't exists");
            }

            _dataSetUow.Remove(entity);

            //BeforeDelete(message);
            _dataSetUow.Commit();
            //AfterDelete(message);

            return string.Empty;
        }

        public async Task<string> HandleAsync(TEntity message)
        {
            var entity = _dataSetUow.Find<TEntity>(message);
            if (entity == null)
            {
                throw new ArgumentException($"Entity {typeof(TEntity).Name} with id={message} doesn't exists");
            }

            _dataSetUow.Remove(entity);

            //BeforeDelete(message);
            await _dataSetUow.CommitAsync();
            //AfterDelete(message);

            return string.Empty;
        }
    }
}
