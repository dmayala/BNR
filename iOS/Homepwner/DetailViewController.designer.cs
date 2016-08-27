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
    [Register ("DetailViewController")]
    partial class DetailViewController
    {
        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UILabel DateLabel { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIImageView ImageView { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UITextField NameField { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UITextField SerialNumberField { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UITextField ValueField { get; set; }

        [Action ("BackgroundTapped:")]
        [GeneratedCode ("iOS Designer", "1.0")]
        partial void BackgroundTapped (UIKit.UITapGestureRecognizer sender);

        [Action ("TakePicture:")]
        [GeneratedCode ("iOS Designer", "1.0")]
        partial void TakePicture (UIKit.UIBarButtonItem sender);

        void ReleaseDesignerOutlets ()
        {
            if (DateLabel != null) {
                DateLabel.Dispose ();
                DateLabel = null;
            }

            if (ImageView != null) {
                ImageView.Dispose ();
                ImageView = null;
            }

            if (NameField != null) {
                NameField.Dispose ();
                NameField = null;
            }

            if (SerialNumberField != null) {
                SerialNumberField.Dispose ();
                SerialNumberField = null;
            }

            if (ValueField != null) {
                ValueField.Dispose ();
                ValueField = null;
            }
        }
    }
}