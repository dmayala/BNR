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
            NavigationItem.LeftBarButtonItem = EditButtonItem;
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
            _itemStore = App.Container.Resolve<ItemStore>();

            AddButton.Clicked += AddButton_Clicked;

            TableView.RowHeight = UITableView.AutomaticDimension;
            TableView.EstimatedRowHeight = 65;
        }

        public override void ViewWillAppear(bool animated)
        {
            base.ViewWillAppear(animated);

            TableView.ReloadData();
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

        public override void PrepareForSegue(UIStoryboardSegue segue, NSObject sender)
        {
            // If the triggered segue is the "ShowItem" segue
            if (segue.Identifier == "ShowItem")
            {
                // Figure out which row was just tapped
                var row = TableView.IndexPathForSelectedRow.Row;

                // Get the item associated with this row and pass it along
                var item = _itemStore.AllItems[row];
                var detailViewController = segue.DestinationViewController as DetailViewController;
                detailViewController.Item = item;
            }
        }

        public override UITableViewCell GetCell(UITableView tableView, NSIndexPath indexPath)
        {
            // Get a new or recycled cel
            var cell = tableView.DequeueReusableCell("ItemCell", indexPath) as ItemCell;

            // Update the labels for the new preferred text size
            cell.UpdateLabels();

            // Set the text on cell with description of the item
            var item = _itemStore.AllItems[indexPath.Row];

            // Configure the cell with the Item
            cell.NameLabel.Text = item.Name;
            cell.SerialNumberLabel.Text = item.SerialNumber;
            cell.ValueLabel.Text = $"${item.ValueInDollars}";

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

        private void AddButton_Clicked(object sender, EventArgs e)
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