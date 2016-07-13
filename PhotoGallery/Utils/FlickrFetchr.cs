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
        const string FetchRecentsMethod = "flickr.photos.getRecent";
        const string SearchMethod = "flickr.photos.search";
        private readonly string _uriBase = $"https://api.flickr.com/services/rest/?api_key={FlickrStrings.ApiKey}&format=json&nojsoncallback=1&extras=url_s";

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

        public async Task<IList<GalleryItem>> FetchRecentPhotosAsync()
        {
            var url = BuildUrl(FetchRecentsMethod, null);
            return await DownloadGalleryItemsAsync(url);
        }


        public async Task<IList<GalleryItem>> SearchPhotosAsync(string query)
        {
            var url = BuildUrl(SearchMethod, query);
            return await DownloadGalleryItemsAsync(url);
        }

        private async Task<IList<GalleryItem>> DownloadGalleryItemsAsync(string url)
        {
            var items = new List<GalleryItem>();

            try
            {
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
            return items;
        }

        private string BuildUrl(string method, string query)
        {
            var uri = $"{_uriBase}&method={method}";

            if (method.Equals(SearchMethod))
            {
                uri = $"{uri}&text={query}";
            }

            return uri;
        }

    }
}