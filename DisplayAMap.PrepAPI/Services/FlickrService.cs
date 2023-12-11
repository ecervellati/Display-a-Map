using DisplayAMap.API;
using DisplayAMap.API.Services;
using DisplayAMap.API.Models;
using DisplayAMap.API.Results;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DisplayAMap.API.Services
{
    public class FlickrService : IFlickrService
    {
        private readonly FlickrHttpClientFactory _httpClientFactory;
        private readonly FlickrHttpClient _client;
        private readonly string _uri = "https://api.flickr.com/services/rest/";
        public FlickrService(FlickrHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<List<Photo>> GetPhotoByTag(string tag)
        {
            FlickrHttpClient _client = _httpClientFactory.CreateHttpClient();
            _client.MethodToCall = ConfigurationManager.AppSettings.Get(FlickrAPIMethod.GetPhotoByTag);
            _client.Data = new Dictionary<string, string>
            {
                { FlickrAPIParameter.Tag, tag },
                { FlickrAPIParameter.HasGeo, "1" }
            };

            Root photosByTagResult = await _client.GetAsync<Root>(_uri);

            if (photosByTagResult == null)
            {
                throw new Exception("No photo founded");
            }

            PhotoCollection photoColl = photosByTagResult.Photos;

            return photoColl.Photo;
        }

        public async Task<Photo> GetGeoInformationPhoto(Photo photo)
        {
            FlickrHttpClient _client = _httpClientFactory.CreateHttpClient();
            _client.MethodToCall = ConfigurationManager.AppSettings.Get(FlickrAPIMethod.GetGeoInformationPhoto);
            _client.Data = new Dictionary<string, string>
            {
                { FlickrAPIParameter.PhotoId, photo.Id }
            };

            Root photosByTagResult = await _client.GetAsync<Root>(_uri);

            if (photosByTagResult == null)
            {
                throw new Exception("No geo information founded");
            }

            photo.Location = photosByTagResult.Photo.Location;

            return photo;
        }
    }
}
