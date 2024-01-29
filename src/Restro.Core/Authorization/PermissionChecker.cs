using Abp.Authorization;
using Restro.Authorization.Roles;
using Restro.Authorization.Users;

namespace Restro.Authorization
{
    public class PermissionChecker : PermissionChecker<Role, User>
    {
        public PermissionChecker(UserManager userManager)
            : base(userManager)
        {
        }
    }
}
