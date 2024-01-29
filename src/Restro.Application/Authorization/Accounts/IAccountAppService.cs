using System.Threading.Tasks;
using Abp.Application.Services;
using Restro.Authorization.Accounts.Dto;

namespace Restro.Authorization.Accounts
{
    public interface IAccountAppService : IApplicationService
    {
        Task<IsTenantAvailableOutput> IsTenantAvailable(IsTenantAvailableInput input);

        Task<RegisterOutput> Register(RegisterInput input);
    }
}
