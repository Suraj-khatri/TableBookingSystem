﻿using Abp.AspNetCore;
using Abp.AspNetCore.TestBase;
using Abp.Modules;
using Abp.Reflection.Extensions;
using Restro.EntityFrameworkCore;
using Restro.Web.Startup;
using Microsoft.AspNetCore.Mvc.ApplicationParts;

namespace Restro.Web.Tests
{
    [DependsOn(
        typeof(RestroWebMvcModule),
        typeof(AbpAspNetCoreTestBaseModule)
    )]
    public class RestroWebTestModule : AbpModule
    {
        public RestroWebTestModule(RestroEntityFrameworkModule abpProjectNameEntityFrameworkModule)
        {
            abpProjectNameEntityFrameworkModule.SkipDbContextRegistration = true;
        } 
        
        public override void PreInitialize()
        {
            Configuration.UnitOfWork.IsTransactional = false; //EF Core InMemory DB does not support transactions.
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(typeof(RestroWebTestModule).GetAssembly());
        }
        
        public override void PostInitialize()
        {
            IocManager.Resolve<ApplicationPartManager>()
                .AddApplicationPartsIfNotAddedBefore(typeof(RestroWebMvcModule).Assembly);
        }
    }
}