using System.Collections.Generic;
using UIKit;

namespace Homepwner.Stores
{
    public class ImageStore
    {
        public static Dictionary<string, UIImage> Cache = new Dictionary<string, UIImage>();

        public void SetImage(UIImage image, string key)
        {
            Cache.Add(key, image);
        }

        public UIImage ImageForKey(string key)
        {
            UIImage image = null;
            if (Cache.TryGetValue(key, out image))
            {
                return image;
            }
            return null;
        }

        public void DeleteImageForKey(string key)
        {
            Cache.Remove(key);
        }
    }
}
