using Foundation;
using Photorama.Utils;
using System;
using System.Linq;
using UIKit;

namespace Photorama
{
    public partial class PhotosViewController : UIViewController
    {
        public PhotosViewController (IntPtr handle) : base (handle)
        {
        }

        public override async void ViewDidLoad()
        {
            base.ViewDidLoad();

            var flickr = new FlickrApi();
            var photosResult = await flickr.FetchRecentPhotosAsync();
            var firstPhoto = photosResult.FirstOrDefault(p => !string.IsNullOrEmpty(p.RemoteUrl));

            if (firstPhoto != null)
            {
                var imageResult = await flickr.FetchImageForPhotoAsync(firstPhoto);
                var imageView = View as UIImageView;
                imageView.Image = imageResult;
            }
        }
    }
}