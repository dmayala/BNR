using Foundation;
using Photorama.Models;
using Photorama.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using UIKit;

namespace Photorama
{
    public partial class PhotosViewController : UIViewController, IUICollectionViewDelegate
    {
        private PhotoDataSource _photoDataSource;
        private FlickrApi _flickrApi;

        public PhotosViewController (IntPtr handle) : base (handle)
        {
        }

        public override async void ViewDidLoad()
        {
            base.ViewDidLoad();
            _photoDataSource = new PhotoDataSource();
            _flickrApi = new FlickrApi();

            CollectionView.DataSource = _photoDataSource;
            CollectionView.Delegate = this;

            var photosResult = await _flickrApi.FetchRecentPhotosAsync();
            var remotePhotos = photosResult.Where(p => !string.IsNullOrEmpty(p.RemoteUrl)).ToList();
            _photoDataSource.Photos = remotePhotos;
            CollectionView.ReloadSections(new NSIndexSet(0));
        }

        public override void PrepareForSegue(UIStoryboardSegue segue, NSObject sender)
        {
            if (segue.Identifier == "ShowPhoto")
            {
                var selectedIndexPath = CollectionView.GetIndexPathsForSelectedItems().First();
                var photo = _photoDataSource.Photos[selectedIndexPath.Row];
                var destinationVC = segue.DestinationViewController as PhotoInfoViewController;
                destinationVC.Photo = photo;
            }
        }

        [Export("collectionView:willDisplayCell:forItemAtIndexPath:")]
        public async void WillDisplayCell(UICollectionView collectionView, UICollectionViewCell cell, NSIndexPath indexPath)
        {
            var photo = _photoDataSource.Photos[indexPath.Row];
            var imageResult = await _flickrApi.FetchImageForPhotoAsync(photo);

            // You will have an error on the next line; you will fix soon
            var photoIndex = _photoDataSource.Photos.IndexOf(photo);
            var photoIndexPath = NSIndexPath.FromRowSection(photoIndex, 0);

            // When the request finishes, only update the cell if it's still visible
            var photoCell = collectionView.CellForItem(photoIndexPath) as PhotoCollectionViewCell;
            if (photoCell != null)
            {
                photoCell.UpdateWithImage(imageResult);
            }
        }
    }

    public class PhotoDataSource : UICollectionViewDataSource
    {
        public IList<GalleryItem> Photos { get; set; }

        public override UICollectionViewCell GetCell(UICollectionView collectionView, NSIndexPath indexPath)
        {
            const string identifier = "PhotoCollectionViewCell";
            var cell = collectionView.DequeueReusableCell(identifier, indexPath) as PhotoCollectionViewCell;

            var photo = Photos[indexPath.Row];
            cell.UpdateWithImage(photo.Image);

            return cell;
        }

        public override nint GetItemsCount(UICollectionView collectionView, nint section)
        {
            if (Photos != null)
            {
                return Photos.Count;
            }

            return 0;
        }
    }
}