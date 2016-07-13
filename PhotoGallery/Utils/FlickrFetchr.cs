using System.Threading.Tasks;
using System.Net.Http;
using System;
using Android.Util;
using Newtonsoft.Json;
using PhotoGallery.Models;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;
using System.Linq;

namespace PhotoGallery.Utils
{
    public class FlickrFetchr
    {
        const string TAG = "FlickrFetchr";

        public async Task<byte[]> GetBytesAsync(string requestUrl)
        {
            using (var client = new HttpClient())
            {
                return await client.GetByteArrayAsync(requestUrl).ConfigureAwait(false);
            }
        }

        public async Task<string> GetStringAsync(string requestUrl)
        {
            using (var client = new HttpClient())
            {
                return await client.GetStringAsync(requestUrl).ConfigureAwait(false);
            }
        }

        public async Task<IList<GalleryItem>> FetchItems()
        {
            var items = new List<GalleryItem>();

            try
            {
                var url =$"https://api.flickr.com/services/rest/?method=flickr.photos.getRecent&api_key={FlickrStrings.ApiKey}&format=json&nojsoncallback=1&extras=url_s";
                var jsonString = await GetStringAsync(url);
                Log.Info(TAG, "Received JSON: " + jsonString);
                var response = JsonConvert.DeserializeObject<FlickrResponse>(jsonString);
                items = response.FlickrPhotos.Photos.ToList();
                return items;
            }
            catch (Exception ex)
            {
                Log.Error(TAG, "Failed to fetch items", ex.Message);
            }
            return null;
        }
    }
}