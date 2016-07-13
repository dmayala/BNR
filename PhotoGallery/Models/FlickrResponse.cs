using Newtonsoft.Json;

namespace PhotoGallery.Models
{
    public class FlickrResponse
    {
        [JsonProperty(PropertyName = "photos")]
        public FlickrPhotos FlickrPhotos { get; set; }
    }
}