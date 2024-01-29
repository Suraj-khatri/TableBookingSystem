using System.Threading.Tasks;
using Restro.Configuration.Dto;

namespace Restro.Configuration
{
    public interface IConfigurationAppService
    {
        Task ChangeUiTheme(ChangeUiThemeInput input);
    }
}
