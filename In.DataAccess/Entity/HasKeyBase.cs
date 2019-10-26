using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using In.DataAccess.Entity.Abstract;

namespace In.DataAccess.Entity
{
    public abstract class HasKey :  HasKeyBase<int>, IHasKey
    {
    }
    
    public abstract class HasKeyBase<TId> : IHasKey<TId>
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public virtual TId Id { get; set; }
    }
}
