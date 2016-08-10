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

            EditButton.TouchUpInside += EditButton_TouchUpInside;
            AddButton.TouchUpInside += AddButton_TouchUpInside;

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

        public override void MoveRow(UITableView tableView, NSIndexPath sourceIndexPath, NSIndexPath destinationIndexPath)
        {
            // Update the model
            _itemStore.MoveItemAtIndex(sourceIndexPath.Row, destinationIndexPath.Row);
        }

        public override void CommitEditingStyle(UITableView tableView, UITableViewCellEditingStyle editingStyle, NSIndexPath indexPath)
        {
            // If the table view is asking to commit a delete command
            if (editingStyle == UITableViewCellEditingStyle.Delete)
            {
                var item = _itemStore.AllItems[indexPath.Row];

                var title = $"Delete {item.Name}?";
                var message = "Are you sure you want to delete this item?";

                var ac = UIAlertController.Create(title, message, UIAlertControllerStyle.ActionSheet);

                var cancelAction = UIAlertAction.Create("Cancel", UIAlertActionStyle.Cancel, null);
                ac.AddAction(cancelAction);

                var deleteAction = UIAlertAction.Create("Delete", UIAlertActionStyle.Destructive, (action) =>
                {
                    // Remove the item from the store
                    _itemStore.RemoveItem(item);

                    // Also remove that row from the table view with an animation
                    TableView.DeleteRows(new NSIndexPath[] { indexPath }, UITableViewRowAnimation.Automatic);
                });
                ac.AddAction(deleteAction);

                // Present the alert controller
                PresentViewController(ac, true, null);
            }
        }

        private void EditButton_TouchUpInside(object sender, EventArgs e)
        {
            if (Editing)
            {
                // Change text of button to inform user of state
                EditButton.SetTitle("Edit", UIControlState.Normal);

                // Turn off editing mode
                SetEditing(false, true);
            }
            else
            {
                // Change text of button to inform user of state
                EditButton.SetTitle("Done", UIControlState.Normal);

                // Enter editing mode
                SetEditing(true, true);
            }
        }

        private void AddButton_TouchUpInside(object sender, EventArgs e)
        {
            // Create a new item and add it to the store
            var newItem = _itemStore.CreateItem();

            // Figure out where that item is in the array
            var index = _itemStore.AllItems.IndexOf(newItem);

            if (index > -1)
            {
                var indexPath = NSIndexPath.FromRowSection(index, 0);

                // Insert this new row into the table
                TableView.InsertRows(new NSIndexPath[] { indexPath }, UITableViewRowAnimation.Automatic);
            }
        }

    }
}