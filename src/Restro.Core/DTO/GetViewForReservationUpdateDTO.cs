using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restro.DTO
{
    public class GetViewForSpecificReservationDTO
    {
        
        public string CustomerName { get; set; }
        public int ReservationId { get; set; }
        public int CustomerId { get; set; }
        public int TableNo { get; set; }
        public int PartySize { get; set; }

    }
}
