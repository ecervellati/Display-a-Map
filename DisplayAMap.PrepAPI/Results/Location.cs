using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DisplayAMap.API.Results
{
    public class Location
    {
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public string Accuracy { get; set; }
        public string Context { get; set; }
        public Locality Locality { get; set; }
        public County County { get; set; }
        public Region Region { get; set; }
        public Country Country { get; set; }
        public Neighbourhood Neighbourhood { get; set; }
    }
}
