using Restro.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restro.DTO
{
    public class CreateReservationDTO
    {
        public CreateCustomersDTO Customers { get; set; }
        public int TableNo { get; set; }
        public int PartySize { get; set; }
    }
}
