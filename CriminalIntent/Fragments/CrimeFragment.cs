using System;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Support.V4.App;
using Android.Views;
using Android.Widget;
using CriminalIntent.Models;
using Fragment = Android.Support.V4.App.Fragment;
using FragmentManager = Android.Support.V4.App.FragmentManager;

namespace CriminalIntent.Fragments
{
    public class CrimeFragment : Fragment
    {
        const string ArgCrimeId = "crime_id";
        const string DialogDate = "DialogDate";

        const int RequestDate = 0;

        private Crime _crime;
        private EditText _titleField;
        private Button _dateButton;
        private CheckBox _solvedCheckBox;

        public static CrimeFragment NewInstance(Guid crimeId)
        {
            var args = new Bundle();
            args.PutString(ArgCrimeId, crimeId.ToString());

            var fragment = new CrimeFragment() { Arguments = args };
            return fragment;
        }

        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            HasOptionsMenu = true;
            var crimeId = new Guid(this.Arguments.GetString(ArgCrimeId));
            _crime = CrimeLab.Get(this.Activity).GetCrime(crimeId);
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            View v = inflater.Inflate(Resource.Layout.CrimeFragment, container, false);

            _titleField = v.FindViewById<EditText>(Resource.Id.CrimeTitle);
            _titleField.Text = _crime.Title;
            _titleField.TextChanged += (sender, e) =>
            {
                _crime.Title = e.Text.ToString();
            };

            _dateButton = v.FindViewById<Button>(Resource.Id.CrimeDate);
            UpdateDate();
            _dateButton.Click += (sender, e) =>
            {
                var dialog = DatePickerFragment.NewInstance(_crime.Date);
                dialog.SetTargetFragment(this, RequestDate);
                dialog.Show(FragmentManager, DialogDate);
            };

            _solvedCheckBox = v.FindViewById<CheckBox>(Resource.Id.CrimeSolved);
            _solvedCheckBox.Checked = _crime.Solved;
            _solvedCheckBox.CheckedChange += (sender, e) =>
            {
                _crime.Solved = e.IsChecked;
            };

            return v;
        }

        public override void OnCreateOptionsMenu(IMenu menu, MenuInflater inflater)
        {
            base.OnCreateOptionsMenu(menu, inflater);
            inflater.Inflate(Resource.Menu.CrimeFragment, menu);
        }

        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            switch (item.ItemId)
            {
                case Resource.Id.DeleteCrimeMenuItem:
                    CrimeLab.Get(Activity).RemoveCrime(_crime);
                    Activity.Finish();
                    return true;
                default:
                    return base.OnOptionsItemSelected(item);
            }
        }

        public override void OnActivityResult(int requestCode, int resultCode, Intent data)
        {
            if (resultCode != (int)Result.Ok) return;

            if (requestCode == RequestDate)
            {
                _crime.Date = new DateTime(data.GetLongExtra(DatePickerFragment.ExtraDate, 0));
                UpdateDate();
            }
        }

        private void UpdateDate()
        {
            _dateButton.Text = _crime.Date.ToLongDateString();
        }
    }
}

