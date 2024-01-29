using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restro.DTO
{
    public class GetViewForAllReservationsDTO
    {
        public int Id { get; set; }
        public string CustomerName { get; set; }
        public string Email { get; set; }
        public int TableNo { get; set; }
        public int PartySize { get; set; }
    }
}
