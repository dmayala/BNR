using Foundation;
using System;
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

            // Create full URL for iamge
            var imageUrl = ImageUrlForKey(key);

            // Turn image into JPEG data
            var data = image.AsJPEG(0.5f);

            // Write it to the full path
            data.Save(imageUrl, true);
        }

        public UIImage ImageForKey(string key)
        {
            UIImage existingImage = null;
            if (Cache.TryGetValue(key, out existingImage))
            {
                return existingImage;
            }

            var imageURL = ImageUrlForKey(key);

            var imageFromDisk = UIImage.FromFile(imageURL.Path);

            if (imageFromDisk != null)
            {
                Cache.Add(key, imageFromDisk);
                return imageFromDisk;
            }

            return null;
        }

        public NSUrl ImageUrlForKey(string key)
        {
            var documentsDirectories =
                NSFileManager.DefaultManager.GetUrls(NSSearchPathDirectory.DocumentDirectory, NSSearchPathDomain.User);
            var documentDirectory = documentsDirectories[0];
            return documentDirectory.Append(key, false);
        }

        public void DeleteImageForKey(string key)
        {
            Cache.Remove(key);

            var imageUrl = ImageUrlForKey(key);
            var error = new NSError();
            NSFileManager.DefaultManager.Remove(imageUrl, out error);
        }
    }
}
