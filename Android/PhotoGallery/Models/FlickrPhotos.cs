using Newtonsoft.Json;
using System.Collections.Generic;

namespace PhotoGallery.Models
{
    public class FlickrPhotos
    {
        [JsonProperty(PropertyName = "photo")]
        public ICollection<GalleryItem> Photos { get; set; }
    }
}