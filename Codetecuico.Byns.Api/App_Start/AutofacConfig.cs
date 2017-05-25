using Autofac;
using Autofac.Integration.WebApi;
using Codetecuico.Byns.Data.Infrastructure;
using Codetecuico.Byns.Data.Repositories;
using Codetecuico.Byns.Service;
using System.Linq;
using System.Reflection;
using System.Web.Http;

namespace Codetecuico.Byns.Api
{
    public static class AutofacConfig
    {
        public static void Configure()
        {
            var builder = new ContainerBuilder();
            var config = GlobalConfiguration.Configuration;

            builder.RegisterApiControllers(Assembly.GetExecutingAssembly());
            builder.RegisterType<UnitOfWork>().As<IUnitOfWork>().InstancePerRequest();
            builder.RegisterType<DbFactory>().As<IDbFactory>().InstancePerRequest();

            builder.RegisterAssemblyTypes(typeof(UserRepository).Assembly)
                      .Where(t => t.Name.EndsWith("Repository"))
                      .AsImplementedInterfaces().InstancePerRequest();

            builder.RegisterAssemblyTypes(typeof(UserService).Assembly)
                      .Where(t => t.Name.EndsWith("Service"))
                      .AsImplementedInterfaces().InstancePerRequest();

            var container = builder.Build();
            config.DependencyResolver = new AutofacWebApiDependencyResolver(container);
        }
    }
}