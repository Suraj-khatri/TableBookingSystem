using System.Threading.Tasks;
using Abp.Application.Services;
using Restro.Sessions.Dto;

namespace Restro.Sessions
{
    public interface ISessionAppService : IApplicationService
    {
        Task<GetCurrentLoginInformationsOutput> GetCurrentLoginInformations();
    }
}
