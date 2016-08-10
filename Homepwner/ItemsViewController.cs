using System;
using Microsoft.Practices.Unity;

using UIKit;
using Homepwner.Stores;
using Foundation;

namespace Homepwner
{
    public partial class ItemsViewController : UITableViewController
    {
        private ItemStore _itemStore;

        public ItemsViewController(IntPtr handle) : base(handle)
        {
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
            _itemStore = App.Container.Resolve<ItemStore>();

            // Get the height of the status bar
            var statusBarHeight = UIApplication.SharedApplication.StatusBarFrame.Height;

            var insets = new UIEdgeInsets(statusBarHeight, 0, 0, 0);
            TableView.ContentInset = insets;
            TableView.ScrollIndicatorInsets = insets;
        }

        public override void DidReceiveMemoryWarning()
        {
            base.DidReceiveMemoryWarning();
            // Release any cached data, images, etc that aren't in use.
        }

        public override nint RowsInSection(UITableView tableView, nint section)
        {
            return _itemStore.AllItems.Count;
        }

        public override UITableViewCell GetCell(UITableView tableView, NSIndexPath indexPath)
        {
            // Get a new or recycled cel
            var cell = tableView.DequeueReusableCell("UITableViewCell", indexPath);

            // Set the text on cell with description of the item
            var item = _itemStore.AllItems[indexPath.Row];

            cell.TextLabel.Text = item.Name;
            cell.DetailTextLabel.Text = $"${item.ValueInDollars}";

            return cell;
        }
    }
}