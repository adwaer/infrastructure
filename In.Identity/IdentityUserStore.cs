using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity.EntityFramework;

namespace In.Identity
{
    public class IdentityUserStore<T, TUserRole> :
        UserStore
            <T, IdentityRole<Guid, TUserRole>, Guid, IdentityUserLogin<Guid>, TUserRole,
                IdentityUserClaim<Guid>> where T : IdentityUser<Guid, IdentityUserLogin<Guid>, TUserRole, IdentityUserClaim<Guid>> where TUserRole : IdentityUserRole<Guid>, new()
    {
        private readonly IOnCreateUserAction<T> _onCreateAction;
        private readonly IGetRolesAction<T> _onGetRolesAction;

        public IdentityUserStore(DbContext ctx, IOnCreateUserAction<T> onCreateAction, IGetRolesAction<T> onGetRolesAction) 
            : base(ctx)
        {
            _onCreateAction = onCreateAction;
            _onGetRolesAction = onGetRolesAction;
        }


        /// <summary>
        /// Insert an entity
        /// </summary>
        /// <param name="user"/>
        public override Task CreateAsync(T user)
        {
            _onCreateAction.Execute(user);

            Context
                .Set<T>()
                .Add(user);

            //if (!user.Roles.Any())
            //{
            //    user.Roles.Add(new TUserRole
            //    {
            //        RoleId = Guid.Empty
            //    });
            //}

            Context.Entry(user).State = EntityState.Added;
            return Context.SaveChangesAsync();
        }

        /// <summary>
        /// Update an entity
        /// </summary>
        /// <param name="user"/>
        public override Task UpdateAsync(T user)
        {
            return Context.SaveChangesAsync();
        }

        /// <summary>
        /// Mark an entity for deletion
        /// </summary>
        /// <param name="user"/>
        public override Task DeleteAsync(T user)
        {
            throw new NotImplementedException();
            //user.IsDeleted = true;

            return UpdateAsync(user);
        }

        /// <summary>
        /// Find a user by id
        /// </summary>
        /// <param name="userId"/>
        /// <returns/>
        public override async Task<T> FindByIdAsync(Guid userId)
        {
            T user =
                await Context.Set<T>()
                .FirstOrDefaultAsync(u => u.Id == userId);
            return user;
        }

        /// <summary>
        /// Find a user by name
        /// </summary>
        /// <param name="userName"/>
        /// <returns/>
        public override async Task<T> FindByNameAsync(string userName)
        {
            return await Context
                .Set<T>()
                .FirstOrDefaultAsync(ac => ac.Email == userName);
        }

        public override Task<T> FindByEmailAsync(string email)
        {
            return FindByNameAsync(email);
        }

        public override Task<IList<string>> GetRolesAsync(T user)
        {
            if (user == null) throw new ArgumentNullException(nameof(user));

            return Task.FromResult(_onGetRolesAction.Execute(user));
        }

        public override Task<IList<Claim>> GetClaimsAsync(T user)
        {
            if (user == null) throw new ArgumentNullException(nameof(user));

            IList<Claim> claims = new Claim[0];
            return Task.FromResult(claims);
        }
    }
}
