using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DisplayAMap.API.Models
{
    public class FlickrAPI
    {
        public string Key { get; }

        public FlickrAPI(string key)
        {
            Key = key;
        }
    }
}
