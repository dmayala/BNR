using Android.OS;
using Android.Support.V4.App;
using Android.Views;
using Android.Widget;
using CriminalIntent.Models;

namespace CriminalIntent.Fragments
{
    public class CrimeFragment : Fragment
    {
        private Crime _crime;
        private EditText _titleField;
        private Button _dateButton;
        private CheckBox _solvedCheckBox;

        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            _crime = new Crime();
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            View v = inflater.Inflate(Resource.Layout.CrimeFragment, container, false);

            _titleField = v.FindViewById<EditText>(Resource.Id.CrimeTitle);
            _titleField.TextChanged += (sender, e) =>
            {
                _crime.Title = e.Text.ToString();
            };

            _dateButton = v.FindViewById<Button>(Resource.Id.CrimeDate);
            _dateButton.Text = _crime.Date.ToLongDateString();
            _dateButton.Enabled = false;

            _solvedCheckBox = v.FindViewById<CheckBox>(Resource.Id.CrimeSolved);
            _solvedCheckBox.CheckedChange += (sender, e) =>
            {
                _crime.Solved = e.IsChecked;
            };

            return v;
        }
    }
}

