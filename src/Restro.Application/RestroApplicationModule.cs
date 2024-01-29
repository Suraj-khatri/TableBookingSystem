using Abp.AutoMapper;
using Abp.Modules;
using Abp.Reflection.Extensions;
using Restro.Authorization;

namespace Restro
{
    [DependsOn(
        typeof(RestroCoreModule), 
        typeof(AbpAutoMapperModule))]
    public class RestroApplicationModule : AbpModule
    {
        public override void PreInitialize()
        {
            Configuration.Authorization.Providers.Add<RestroAuthorizationProvider>();
        }

        public override void Initialize()
        {
            var thisAssembly = typeof(RestroApplicationModule).GetAssembly();

            IocManager.RegisterAssemblyByConvention(thisAssembly);

            Configuration.Modules.AbpAutoMapper().Configurators.Add(
                // Scan the assembly for classes which inherit from AutoMapper.Profile
                cfg => cfg.AddMaps(thisAssembly)
            );
        }
    }
}
