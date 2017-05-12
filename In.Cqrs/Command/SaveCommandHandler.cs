using System.Threading.Tasks;
using In.Cqrs.Uow;
using In.Domain;

namespace In.Cqrs.Command
{
    public class SaveCommandHandler<TEntity> : IMsgHandler<TEntity>
        where TEntity : class, IEntity, IMessage
    {
        private readonly IDataSetUow _ctx;
        //private readonly ISubscriber<TEntity>[] _subscribers;

        public SaveCommandHandler(IDataSetUow ctx/*, ISubscriber<TEntity>[] subscribers*/)
        {
            _ctx = ctx;
            //_subscribers = subscribers;
        }
        
        //private void BeforeSave(TEntity data)
        //{
        //    if (_subscribers == null || _subscribers.Length == 0) { return; }

        //    foreach (ISubscriber<TEntity> subscriber in _subscribers)
        //    {
        //        subscriber.BeforeSave(data);
        //    }
        //}
        //private void AfterSave(TEntity data)
        //{
        //    if (_subscribers == null || _subscribers.Length == 0) { return; }

        //    foreach (ISubscriber<TEntity> subscriber in _subscribers)
        //    {
        //        subscriber.AfterSave(data);
        //    }
        //}

        public string Handle(TEntity message)
        {
            //FixState(message);
            
            //BeforeSave(message);
            _ctx.Commit();
            //AfterSave(message);

            return string.Empty;
        }

        //private void FixState(TEntity message)
        //{
        //    var type = message.GetType();
        //    var ipProp = type.GetProperty("Id");
        //    if (!ipProp.PropertyType.IsValueType)
        //    {
        //        throw new NotImplementedException();
        //    }

        //    //if (ipProp.GetValue(message).Equals(Activator.CreateInstance(ipProp.PropertyType)))
        //    //{
        //    //    _ctx.Entry(message).State = EntityState.Added;
        //    //}
        //    //else
        //    //{
        //    //    _ctx.Entry(message).State = EntityState.Modified;
        //    //}
        //}

        public async Task<string> HandleAsync(TEntity message)
        {
            //FixState(message);

            //BeforeSave(message);
            await _ctx.CommitAsync();
            //AfterSave(message);

            return string.Empty;
        }
    }
}
