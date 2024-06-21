using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.DTOs
{
    public class PropertyDto
    {
        public int ID { get; set; }
        public string? LandMark { get; set; }
        public int Price { get; set; }
        public string? Location { get; set; }
    }
}
