using Android.Net;
using Newtonsoft.Json;

namespace PhotoGallery.Models
{
    public class GalleryItem
    {
        [JsonProperty(PropertyName = "id")]
        public string Id { get; set; }
        [JsonProperty(PropertyName = "title")]
        public string Caption { get; set; }
        [JsonProperty(PropertyName = "url_s")]
        public string Url { get; set; }
        [JsonProperty(PropertyName = "owner")]
        public string Owner { get; set; }

        public Uri GetPhotoPageUri()
        {
            return Uri.Parse($"http://www.flickr.com/photos/{Owner}/{Id}");
        }

        public override string ToString()
        {
            return Caption;
        }
    }
}