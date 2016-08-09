using Foundation;
using System;
using System.Diagnostics;
using UIKit;

namespace WorldTrotter
{
    public partial class ConversionViewController : UIViewController, IUITextFieldDelegate
    {
        private double _fahrenheitValue;

        protected ConversionViewController(IntPtr handle) : base(handle)
        {
            // Note: this .ctor should not contain any initialization logic.
        }

        public override void DidReceiveMemoryWarning()
        {
            base.DidReceiveMemoryWarning();

            // Release any cached data, images, etc that aren't in use.
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            FahrenheitTextField.Delegate = this;
            FahrenheitTextField.EditingChanged += FahrenheitTextField_EditingChanged;
            View.AddGestureRecognizer(new UITapGestureRecognizer(() => FahrenheitTextField.ResignFirstResponder()));
        }

        private void FahrenheitTextField_EditingChanged(object sender, EventArgs e)
        {
            var text = FahrenheitTextField.Text;
            if (double.TryParse(text, out _fahrenheitValue))
            {
                double celsius = (_fahrenheitValue - 32) * (5.0 / 9.0);
                CelsiusLabel.Text = string.Format("{0:0.#}", celsius);
            }
            else
            {
                CelsiusLabel.Text = "???";
            }
        }

        #region IUITextFieldDelegate

        [Export("textField:shouldChangeCharactersInRange:replacementString:")]
        public bool ShouldChangeCharacters(UITextField textField, NSRange range, string replacementString)
        {
            int existingTextHasDecimalSeparator = textField.Text.IndexOf(".");
            int replacementTextHasDecimalSeparator = replacementString.IndexOf(".");

            if (existingTextHasDecimalSeparator > -1 && replacementTextHasDecimalSeparator > -1)
            {
                return false;
            }

            return true;
        }

        #endregion
    }
}