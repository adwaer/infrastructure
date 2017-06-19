using System.Threading.Tasks;
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


        public async Task<string> Handle(params TEntity[] messages)
        {
            foreach (var msg in messages)
            {
                _ctx.FixupState(msg);
            }

            return (await _ctx.CommitAsync())
                .ToString();
        }
    }
}