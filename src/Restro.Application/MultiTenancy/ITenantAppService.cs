using Abp.Application.Services;
using Restro.MultiTenancy.Dto;

namespace Restro.MultiTenancy
{
    public interface ITenantAppService : IAsyncCrudAppService<TenantDto, int, PagedTenantResultRequestDto, CreateTenantDto, TenantDto>
    {
    }
}

