using Abp.Domain.Entities;
using Castle.Core.Resource;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restro.Models
{
    public class Reservations : Entity<int>
    {
        public virtual int CustomerId { get; set; }
        [ForeignKey("CustomerId")]
        public Customers CustomerFK { get; set; }
        public virtual int TableId { get; set; }
        [ForeignKey("TableId")]
        public Tables TablesFK { get; set; }
        public DateTime ReservationTime { get; set; }
        public int PartySize { get; set; }
    }
}
