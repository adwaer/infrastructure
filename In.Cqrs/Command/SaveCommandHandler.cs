using In.Domain;
using In.Entity.Uow;

namespace In.Cqrs.Command
{
    public class SaveCommandHandler<TEntity>
        where TEntity : class, IEntity
    {
        private readonly IDataSetUow _ctx;

        public SaveCommandHandler(IDataSetUow ctx)
        {
            _ctx = ctx;
        }
        

        public string Handle(TEntity message)
        {
            _ctx.FixupState(message);
            return _ctx.Commit().ToString();
        }
        
    }
}
