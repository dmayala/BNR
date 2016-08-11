using Foundation;
using Homepwner.Models;
using Homepwner.Stores;
using System;
using UIKit;
using Microsoft.Practices.Unity;

namespace Homepwner
{
    public partial class DetailViewController : UIViewController, IUITextFieldDelegate, IUINavigationControllerDelegate, IUIImagePickerControllerDelegate
    {
        private ImageStore _imageStore;
      
        public Item Item { get; set; }

        public DetailViewController (IntPtr handle) : base (handle)
        {
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
            _imageStore = App.Container.Resolve<ImageStore>();

            NameField.Delegate = this;
            SerialNumberField.Delegate = this;
            ValueField.Delegate = this;

            NavigationItem.Title = Item.Name;
        }

        public override void ViewWillAppear(bool animated)
        {
            base.ViewWillAppear(animated);

            NameField.Text = Item.Name;
            SerialNumberField.Text = Item.SerialNumber;
            ValueField.Text = $"{Item.ValueInDollars}";
            DateLabel.Text = Item.DateCreated.ToShortDateString();

            // Get the image key
            var key = Item.ImageKey;

            // If there is an associated image with the item
            // display it on the image view
            var imageToDisplay = _imageStore.ImageForKey(key);
            ImageView.Image = imageToDisplay; 
        }

        public override void ViewWillDisappear(bool animated)
        {
            base.ViewWillDisappear(animated);

            // Clear first responder
            View.EndEditing(true);

            // "Save" changes to item
            Item.Name = NameField.Text ?? "";
            Item.SerialNumber = SerialNumberField.Text;

            int value = 0;
            Item.ValueInDollars = int.TryParse(ValueField.Text, out value) ? value : 0;
        }

        partial void BackgroundTapped(UITapGestureRecognizer sender)
        {
            View.EndEditing(true);
        }

        partial void TakePicture(UIBarButtonItem sender)
        {
            var imagePicker = new UIImagePickerController();

            // If the device has a camera, take a picture; otherwise, just pick from photo library
            if (UIImagePickerController.IsSourceTypeAvailable(UIImagePickerControllerSourceType.Camera))
            {
                imagePicker.SourceType = UIImagePickerControllerSourceType.Camera;
            }
            else
            {
                imagePicker.SourceType = UIImagePickerControllerSourceType.PhotoLibrary;
            }

            imagePicker.Delegate = this;

            // Place image picker on the screen
            PresentViewController(imagePicker, true, null);
        }

        #region IUIImagePickerControllerDelegate methods
        [Export("imagePickerController:didFinishPickingMediaWithInfo:")]
        public void FinishedPickingMedia(UIImagePickerController picker, NSDictionary info)
        {
            // Get picked image from info dictionary
            var image = info.ObjectForKey(UIImagePickerController.OriginalImage) as UIImage;

            // Store the image in the ImageStore for the item's key
            _imageStore.SetImage(image, Item.ImageKey);

            // Put that image on the screen in the image view
            ImageView.Image = image;

            // Take image picker off the screen -
            // you must call this dismiss method
            DismissViewController(true, null);
        }
        #endregion

        #region IUITextFieldDelegate methods
        [Export("textFieldShouldReturn:")]
        public bool ShouldReturn(UITextField textField)
        {
            textField.ResignFirstResponder();
            return true;
        }
        #endregion

    }
}