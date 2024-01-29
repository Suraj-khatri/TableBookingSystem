using Abp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restro.Models
{
    public class Tables : Entity
    {
        public int TableNo { get; set; }
        public int Capacity { get; set; }
        public bool IsAvailable { get; set; }
    }
}
