using Foundation;
using Photorama.Models;
using Photorama.Utils;
using System;
using UIKit;

namespace Photorama
{
    public partial class PhotoInfoViewController : UIViewController
    {
        public GalleryItem Photo { get; set; }

        public PhotoInfoViewController(IntPtr handle) : base(handle)
        {
        }

        public override async void ViewDidLoad()
        {
            base.ViewDidLoad();

            var flickrApi = new FlickrApi();
            var image = await flickrApi.FetchImageForPhotoAsync(Photo);
            ImageView.Image = image;
        }
    }
}