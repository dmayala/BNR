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

        public override string ToString()
        {
            return Caption;
        }
    }
}