using Abp.Domain.Entities;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restro.Models
{
    public class Photos : Entity<Guid>
    {
        public byte[] ImageBytes { get; set; }
        public string FileName { get; set; }
    }
}
