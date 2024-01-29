using Microsoft.Extensions.DependencyInjection;
using Castle.Windsor.MsDependencyInjection;
using Abp.Dependency;
using Restro.Identity;

namespace Restro.Migrator.DependencyInjection
{
    public static class ServiceCollectionRegistrar
    {
        public static void Register(IIocManager iocManager)
        {
            var services = new ServiceCollection();

            IdentityRegistrar.Register(services);

            WindsorRegistrationHelper.CreateServiceProvider(iocManager.IocContainer, services);
        }
    }
}
