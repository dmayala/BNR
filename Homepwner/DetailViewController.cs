using Foundation;
using Homepwner.Models;
using System;
using UIKit;

namespace Homepwner
{
    public partial class DetailViewController : UIViewController, IUITextFieldDelegate
    {
        public Item Item { get; set; }

        public DetailViewController (IntPtr handle) : base (handle)
        {
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

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