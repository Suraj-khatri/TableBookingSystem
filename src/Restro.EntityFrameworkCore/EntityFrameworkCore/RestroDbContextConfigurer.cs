using System.Data.Common;
using Microsoft.EntityFrameworkCore;

namespace Restro.EntityFrameworkCore
{
    public static class RestroDbContextConfigurer
    {
        public static void Configure(DbContextOptionsBuilder<RestroDbContext> builder, string connectionString)
        {
            builder.UseSqlServer(connectionString);
        }

        public static void Configure(DbContextOptionsBuilder<RestroDbContext> builder, DbConnection connection)
        {
            builder.UseSqlServer(connection);
        }
    }
}
