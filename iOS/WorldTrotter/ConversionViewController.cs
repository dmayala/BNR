using Foundation;
using System;
using System.Diagnostics;
using System.Globalization;
using UIKit;

namespace WorldTrotter
{
    public partial class ConversionViewController : UIViewController
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

            Debug.WriteLine("ConversionViewController loaded its view.");

            FahrenheitTextField.ShouldChangeCharacters = ShouldChangeCharacters;
            FahrenheitTextField.EditingChanged += FahrenheitTextField_EditingChanged;
            View.AddGestureRecognizer(new UITapGestureRecognizer(() => FahrenheitTextField.ResignFirstResponder()));
        }

        private void FahrenheitTextField_EditingChanged(object sender, EventArgs e)
        {
            var text = FahrenheitTextField.Text;

            var numberFormatter = new NSNumberFormatter();
            var value = numberFormatter.NumberFromString(text);

            if (double.TryParse(value?.StringValue, out _fahrenheitValue))
            {
                double celsius = (_fahrenheitValue - 32) * (5.0 / 9.0);
                CelsiusLabel.Text = string.Format("{0:0.#}", celsius);
            }
            else
            {
                CelsiusLabel.Text = "???";
            }
        }

        public bool ShouldChangeCharacters(UITextField textField, NSRange range, string replacementString)
        {
            var decimalSeparator = NSLocale.CurrentLocale.DecimalSeparator;
            int existingTextHasDecimalSeparator = textField.Text.IndexOf(decimalSeparator);
            int replacementTextHasDecimalSeparator = replacementString.IndexOf(decimalSeparator);

            if (existingTextHasDecimalSeparator > -1 && replacementTextHasDecimalSeparator > -1)
            {
                return false;
            }

            return true;
        }
    }
}