using DisplayAMap.API.Models;
using Newtonsoft.Json;
using System.Configuration;
using System.Text.RegularExpressions;
using System.Web;
using System.Xml;
using System.Xml.Serialization;

namespace DisplayAMap.API
{
    public class FlickrHttpClient : HttpClient
    {
        private readonly string _apiKey;
        public Uri? RequestUri { get; set; }
        public string? MethodToCall { get; set; }
        public Dictionary<string, string>? Data { get; set; }

        public FlickrHttpClient(string apiKey)
        {
            _apiKey = apiKey;
        }

        /// <summary>
        /// Method to send the request we built before
        /// </summary>
        public async Task<T> GetAsync<T>(string uri)
        {
            try
            {
                RequestUri = new Uri(uri);
                string url = BuildUri();
                HttpResponseMessage response = await GetAsync(url).ConfigureAwait(false);
                response.EnsureSuccessStatusCode();
                var jsonResponse = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                
                return JsonConvert.DeserializeObject<T>(jsonResponse);
            }
            catch (HttpRequestException ex)
            {
                throw new Exception($"HTTP request failed: {ex.Message}", ex);
            }
            catch (Exception ex)
            {
                throw new Exception($"An error occurred: {ex.Message}", ex);
            }
        }

        public string BuildUri()
        {
            if (RequestUri == null)
            {
                throw new InvalidOperationException("RequestUri is not set.");
            }

            var builder = new UriBuilder(RequestUri)
            {
                Port = -1
            };

            var query = MethodToCall != null ? $"?method={MethodToCall}&api_key={HttpUtility.UrlEncode(_apiKey)}" : $"&api_key ={HttpUtility.UrlEncode(_apiKey)}";

            if (Data != null)
            {
                foreach (var item in Data)
                {
                    query += $"&{HttpUtility.UrlEncode(item.Key)}={HttpUtility.UrlEncode(item.Value)}";
                }
            }

            builder.Query = query + "&format=json&nojsoncallback=1";

            return builder.ToString();
        }
    }
}