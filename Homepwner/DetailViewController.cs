using Foundation;
using Homepwner.Models;
using System;
using UIKit;

namespace Homepwner
{
    public partial class DetailViewController : UIViewController
    {
        public Item Item { get; set; }

        public DetailViewController (IntPtr handle) : base (handle)
        {
        }

        public override void ViewWillAppear(bool animated)
        {
            base.ViewWillAppear(animated);

            NameField.Text = Item.Name;
            SerialNumberField.Text = Item.SerialNumber;
            ValueField.Text = $"{Item.ValueInDollars}";
            DateLabel.Text = Item.DateCreated.ToShortDateString();
        }
    }
}