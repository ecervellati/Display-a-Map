using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DisplayAMap.API.Results
{
    public class Root
    {
        public Photo Photo { get; set; }
        public PhotoCollection Photos { get; set; }
        public string Stat { get; set; }
    }
}
