using Foundation;
using Newtonsoft.Json;
using Photorama.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using UIKit;

namespace Photorama.Utils
{
    public class FlickrApi
    {
        const string apiKey = "a6d819499131071f158fd740860a5a88";
        private readonly string _uriBase = $"https://api.flickr.com/services/rest/?api_key={apiKey}&format=json&nojsoncallback=1&extras=url_h,date_taken";

        const string RecentPhotos = "flickr.photos.getRecent";
        const string SearchMethod = "flickr.photos.search";

        public async Task<string> GetStringAsync(string requestUrl)
        {
            using (var client = new HttpClient())
            {
                return await client.GetStringAsync(requestUrl);
            }
        }

        public async Task<IList<GalleryItem>> FetchRecentPhotosAsync()
        {
            var url = BuildUrl(RecentPhotos, null);
            return await DownloadGalleryItemsAsync(url);
        }

        public async Task<UIImage> FetchImageForPhotoAsync(GalleryItem item)
        {

            if (item.Image != null)
            {
                return item.Image;
            }

            var photoUrl = item.RemoteUrl;
            if (!String.IsNullOrEmpty(photoUrl))
            {
                using (var client = new HttpClient())
                {
                    var response = await client.GetByteArrayAsync(photoUrl);
                    var image = new UIImage(NSData.FromArray(response));
                    item.Image = image;
                    return image;
                }
            }
            return null;
        }

        private async Task<IList<GalleryItem>> DownloadGalleryItemsAsync(string url)
        {
            var items = new List<GalleryItem>();

            try
            {
                var jsonString = await GetStringAsync(url);
                Debug.WriteLine("Received JSON: " + jsonString, "FlickrApi");
                var response = JsonConvert.DeserializeObject<FlickrResponse>(jsonString);
                items = response.FlickrPhotos.Photos.ToList();
                return items;
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Failed to fetch items " + ex.Message, "FlickrApi");
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
