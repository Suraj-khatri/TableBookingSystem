using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restro.DTO
{
    public class GetViewForAllTablesDTO : EntityDto<int>
    {
        public int TableNo { get; set; }
        public int Capacity { get; set; }
    }
}
