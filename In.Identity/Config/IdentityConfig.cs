using System;
using System.Web;
using Autofac;
using Autofac.Integration.WebApi;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;

namespace In.Identity.Config
{
    public static class IdentityConfig<T, TRole> where T : IdentityUser<Guid, IdentityUserLogin<Guid>, TRole, IdentityUserClaim<Guid>> where TRole : IdentityUserRole<Guid>, new()
    {
        public static void Ioc<TStore>(ContainerBuilder builder)
        {
            builder.RegisterType<TStore>()
                .As<IUserStore<T, Guid>>()
                .AsSelf()
                .InstancePerLifetimeScope();

            builder.RegisterType<IdentityUserManager<T>>()
                .As<UserManager<T, Guid>>()
                .AsSelf()
                .InstancePerLifetimeScope();

            builder.RegisterType<SignInManager<T, Guid>>()
                .AsSelf()
                .InstancePerLifetimeScope();

            builder.Register(c => HttpContext.Current.GetOwinContext().Authentication)
                .InstancePerLifetimeScope();

            builder.RegisterApiControllers(typeof(IdentityConfig<T, TRole>).Assembly);
        }


        public static void Ioc(ContainerBuilder builder)
        {
            builder.RegisterType<IdentityUserStore<T, TRole>>()
                .As<IUserStore<T, Guid>>()
                .AsSelf()
                .InstancePerLifetimeScope();

            builder.RegisterType<IdentityUserManager<T>>()
                .As<UserManager<T, Guid>>()
                .AsSelf()
                .InstancePerLifetimeScope();

            builder.RegisterType<SignInManager<T, Guid>>()
                .AsSelf()
                .InstancePerLifetimeScope();

            builder.Register(c => HttpContext.Current.GetOwinContext().Authentication)
                .InstancePerLifetimeScope();

            builder.RegisterApiControllers(typeof(IdentityConfig<T, TRole>).Assembly);
        }
    }
}
