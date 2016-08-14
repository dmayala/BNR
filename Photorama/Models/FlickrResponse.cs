using Newtonsoft.Json;

namespace Photorama.Models
{
    public class FlickrResponse
    {
        [JsonProperty(PropertyName = "photos")]
        public FlickrPhotos FlickrPhotos { get; set; }
    }
}