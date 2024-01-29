using Microsoft.EntityFrameworkCore;
using Abp.Zero.EntityFrameworkCore;
using Restro.Authorization.Roles;
using Restro.Authorization.Users;
using Restro.MultiTenancy;
using Restro.Models;

namespace Restro.EntityFrameworkCore
{
    public class RestroDbContext : AbpZeroDbContext<Tenant, Role, User, RestroDbContext>
    {
        /* Define a DbSet for each entity of the application */
        
        public DbSet<Tables> Tables { get; set; }
        public DbSet<Customers> Customers { get; set; }
        public DbSet<Reservations> Reservations { get; set; }
        public DbSet<Photos> Photos { get; set; }
        public RestroDbContext(DbContextOptions<RestroDbContext> options)
            : base(options)
        {
        }
    }
}
