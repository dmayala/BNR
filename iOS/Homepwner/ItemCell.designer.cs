// WARNING
//
// This file has been generated automatically by Xamarin Studio from the outlets and
// actions declared in your storyboard file.
// Manual changes to this file will not be maintained.
//
using Foundation;
using System;
using System.CodeDom.Compiler;
using UIKit;

namespace Homepwner
{
    [Register ("ItemCell")]
    partial class ItemCell
    {
        [Outlet]
        public UIKit.UILabel NameLabel { get; private set; }

        [Outlet]
        public UIKit.UILabel SerialNumberLabel { get; private set; }

        [Outlet]
        public UIKit.UILabel ValueLabel { get; private set; }

        void ReleaseDesignerOutlets ()
        {
            if (NameLabel != null) {
                NameLabel.Dispose ();
                NameLabel = null;
            }

            if (SerialNumberLabel != null) {
                SerialNumberLabel.Dispose ();
                SerialNumberLabel = null;
            }

            if (ValueLabel != null) {
                ValueLabel.Dispose ();
                ValueLabel = null;
            }
        }
    }
}