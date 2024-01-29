using Abp.Domain.Entities;

namespace Restro.Models
{
    public class Customers : Entity<int>
    {
        public string CustomerName { get; set; }
        public string CustomerEmail { get; set; }

    }
}
