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
        public long? Price { get; set; }
        public string? Image { get; set; }
        public string? Location { get; set; }
        public double Latutide { get;set; }
        public double Longitude { get; set; }
    }
}
