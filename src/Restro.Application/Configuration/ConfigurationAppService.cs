using System.Threading.Tasks;
using Abp.Authorization;
using Abp.Runtime.Session;
using Restro.Configuration.Dto;

namespace Restro.Configuration
{
    [AbpAuthorize]
    public class ConfigurationAppService : RestroAppServiceBase, IConfigurationAppService
    {
        public async Task ChangeUiTheme(ChangeUiThemeInput input)
        {
            await SettingManager.ChangeSettingForUserAsync(AbpSession.ToUserIdentifier(), AppSettingNames.UiTheme, input.Theme);
        }
    }
}
