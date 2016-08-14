using Newtonsoft.Json;
using System;

namespace Photorama.Models
{
    public class GalleryItem
    {
        [JsonProperty(PropertyName = "id")]
        public string PhotoId { get; set; }
        [JsonProperty(PropertyName = "title")]
        public string Title { get; set; }
        [JsonProperty(PropertyName = "url_h")]
        public string RemoteUrl { get; set; }
        [JsonProperty(PropertyName = "datetaken")]
        public DateTime DateTaken { get; set; }

        public override string ToString()
        {
            return Title;
        }
    }
}