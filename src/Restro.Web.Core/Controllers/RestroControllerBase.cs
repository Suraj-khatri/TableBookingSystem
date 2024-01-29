using Abp.AspNetCore.Mvc.Controllers;
using Abp.IdentityFramework;
using Microsoft.AspNetCore.Identity;

namespace Restro.Controllers
{
    public abstract class RestroControllerBase: AbpController
    {
        protected RestroControllerBase()
        {
            LocalizationSourceName = RestroConsts.LocalizationSourceName;
        }

        protected void CheckErrors(IdentityResult identityResult)
        {
            identityResult.CheckErrors(LocalizationManager);
        }
    }
}
