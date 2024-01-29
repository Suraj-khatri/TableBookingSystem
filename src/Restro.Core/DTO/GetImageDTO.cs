using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restro.DTO
{
    public class GetImageDTO
    {
        public string FileName { get; set; }
        public byte[] ImageBytes { get; set; }
    }
}
