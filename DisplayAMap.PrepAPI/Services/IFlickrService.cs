using DisplayAMap.API.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DisplayAMap.API.Services
{
    public interface IFlickrService
    {
        Task<List<Photo>> GetPhotoByTag(string tag);
        Task<Photo> GetGeoInformationPhoto(Photo photo);
    }
}
