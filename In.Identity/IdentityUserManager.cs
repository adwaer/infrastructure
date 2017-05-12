using System;
using System.Data.Entity;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace In.Identity
{
    public class IdentityUserManager<T> : UserManager<T, Guid> where T : class, IUser<Guid>
    {
        public IdentityUserManager(IUserStore<T, Guid> store) 
            : base(store)
        {
            UserTokenProvider = new TotpSecurityStampBasedTokenProvider<T, Guid>();
            UserValidator = new UserValidator<T, Guid>(this)
            {
                AllowOnlyAlphanumericUserNames = false
            };
        }
        
        public static IdentityUserManager<TEntity> Get<TEntity, TRole>(DbContext dncontext, IOnCreateUserAction<TEntity> onCreateAction, IGetRolesAction<TEntity> onGetRolesAction) where TEntity : IdentityUser<Guid, IdentityUserLogin<Guid>, TRole, IdentityUserClaim<Guid>> where TRole : IdentityUserRole<Guid>, new()
        {
            return new IdentityUserManager<TEntity>(new IdentityUserStore<TEntity, TRole>(dncontext, onCreateAction, onGetRolesAction));
        }
    }
}
