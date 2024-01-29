using Restro.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restro.DTO
{
    public class UpdateReservationDTO
    {
        public int ReservationId { get; set; }
        public int PartySize { get; set; }
        public  ReservationStatus Status { get; set; }
    }
}
