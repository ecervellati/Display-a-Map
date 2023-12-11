using DisplayAMap.API;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DisplayAMap.API
{
    public class FlickrHttpClientFactory
    {
        private readonly string _apiKey;

        public FlickrHttpClientFactory(string apikey)
        {
            _apiKey = apikey;
        }
        public FlickrHttpClient CreateHttpClient() 
        {
            return new FlickrHttpClient(_apiKey);
        }
    }
}
