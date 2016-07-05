using Android.OS;
using Android.Support.V4.App;
using Android.Views;
using Android.Widget;
using CriminalIntent.Models;

namespace CriminalIntent
{
    public class CrimeFragment : Fragment
    {
        private Crime _crime;
        private EditText _titleField;

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

            return v;
        }
    }
}

