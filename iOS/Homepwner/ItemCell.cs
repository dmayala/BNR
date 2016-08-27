using Foundation;
using System;
using UIKit;

namespace Homepwner
{
    public partial class ItemCell : UITableViewCell
    {
        public ItemCell (IntPtr handle) : base (handle)
        {
        }

        public void UpdateLabels()
        {
            var bodyFont = UIFont.PreferredBody;
            NameLabel.Font = bodyFont;
            ValueLabel.Font = bodyFont;

            SerialNumberLabel.Font = UIFont.PreferredCaption1;
        }
    }
}