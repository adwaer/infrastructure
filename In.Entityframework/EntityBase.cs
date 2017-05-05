using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using In.Domain;

namespace In.Entityframework
{
    public abstract class EntityBase<TId> : IEntity<TId>
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public virtual TId Id { get; set; }

        public bool IsNew()
        {
            return Id == null || Id.Equals(default(TId));
        }

        public object GetId()
        {
            return Id;
        }
    }
}
