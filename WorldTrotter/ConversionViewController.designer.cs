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

namespace WorldTrotter
{
    [Register ("ConversionViewController")]
    partial class ConversionViewController
    {
        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UILabel CelsiusLabel { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UITabBarItem Convert { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UITextField FahrenheitTextField { get; set; }

        void ReleaseDesignerOutlets ()
        {
            if (CelsiusLabel != null) {
                CelsiusLabel.Dispose ();
                CelsiusLabel = null;
            }

            if (Convert != null) {
                Convert.Dispose ();
                Convert = null;
            }

            if (FahrenheitTextField != null) {
                FahrenheitTextField.Dispose ();
                FahrenheitTextField = null;
            }
        }
    }
}