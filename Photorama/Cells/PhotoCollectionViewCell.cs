using Foundation;
using System;
using UIKit;

namespace Photorama
{
    public partial class PhotoCollectionViewCell : UICollectionViewCell
    {
        public PhotoCollectionViewCell (IntPtr handle) : base (handle)
        {
        }

        public void UpdateWithImage(UIImage image)
        {
            if (image != null)
            {
                Spinner.StopAnimating();
                ImageView.Image = image;
            }
            else
            {
                Spinner.StartAnimating();
                ImageView.Image = null;
            }
        }

        public override void AwakeFromNib()
        {
            base.AwakeFromNib();

            UpdateWithImage(null);
        }

        public override void PrepareForReuse()
        {
            base.PrepareForReuse();

            UpdateWithImage(null);
        }
    }
}