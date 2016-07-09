using System;
using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.OS;
using Android.Provider;
using Android.Support.V4.App;
using Android.Views;
using Android.Widget;
using CriminalIntent.Core.Models;
using CriminalIntent.DAO;
using Fragment = Android.Support.V4.App.Fragment;
using FragmentManager = Android.Support.V4.App.FragmentManager;

namespace CriminalIntent.Fragments
{
    public class CrimeFragment : Fragment
    {
        const string ArgCrimeId = "crime_id";
        const string DialogDate = "DialogDate";

        const int RequestDate = 0;
        const int RequestContact = 1;

        private Crime _crime;
        private EditText _titleField;
        private Button _dateButton;
        private CheckBox _solvedCheckBox;
        private Button _reportButton;
        private Button _suspectButton;
        private Button _callButton;

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

        public override void OnPause()
        {
            base.OnPause();
            CrimeLab.Get(Activity).UpdateCrime(_crime);
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

            _reportButton = v.FindViewById<Button>(Resource.Id.CrimeReport);
            _reportButton.Click += (sender, e) =>
            {
                var intent = ShareCompat.IntentBuilder.From(Activity)
                                        .SetType("text/plain")
                                        .SetText(GetCrimeReport())
                                        .SetSubject(GetString(Resource.String.crime_report_subject))
                                        .SetChooserTitle(GetString(Resource.String.send_report))
                                        .CreateChooserIntent();
                StartActivity(intent);
            };

            var pickContact = new Intent(Intent.ActionPick, ContactsContract.Contacts.ContentUri);
            _suspectButton = v.FindViewById<Button>(Resource.Id.CrimeSuspect);
            _suspectButton.Click += (sender, e) =>
            {
                StartActivityForResult(pickContact, RequestContact);
            };

            if (_crime.Suspect != null)
            {
                _suspectButton.Text = _crime.Suspect;
            }

            var packageManager = Activity.PackageManager;
            if (packageManager.ResolveActivity(pickContact, PackageInfoFlags.MatchDefaultOnly) == null)
            {
                _suspectButton.Enabled = false;
            }

            _callButton = v.FindViewById<Button>(Resource.Id.CrimeCall);
            _callButton.Enabled = _crime.PhoneNumber != null;
            _callButton.Click += (sender, e) =>
            {
                var i = new Intent(Intent.ActionDial, Android.Net.Uri.Parse("tel:" + _crime.PhoneNumber));
                StartActivity(i);
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
            else if (requestCode == RequestContact && data != null)
            {
                var contactUri = data.Data;
                var queryFields = new string[] { 
                    ContactsContract.Contacts.InterfaceConsts.DisplayName,
                    ContactsContract.ContactsColumns.HasPhoneNumber
                };
                var c = Activity.ContentResolver.Query(contactUri, queryFields, null, null, null);
                try
                {
                    if (c.Count == 0) return;
                    c.MoveToFirst();
                    var suspect = c.GetString(0);
                    var hasNumber = Convert.ToInt32(c.GetString(1)) > 0;
                    _crime.Suspect = suspect;
                    _suspectButton.Text = suspect;

                    if (hasNumber)
                    {
                        var phoneC = Activity.ContentResolver.Query(
                            ContactsContract.CommonDataKinds.Phone.ContentUri,
                            new string[] { ContactsContract.CommonDataKinds.Phone.Number },
                            ContactsContract.ContactsColumns.DisplayName + "=?",
                            new string[] { suspect },
                            null
                        );
                        using (phoneC)
                        {
                            if (phoneC.Count > 0) 
                            {
                                phoneC.MoveToFirst();
                                string number = phoneC.GetString(phoneC.GetColumnIndex(ContactsContract.CommonDataKinds.Phone.Number));
                                _callButton.Enabled = true;
                                _crime.PhoneNumber = number;
                            }
                        }
                    }
                }
                finally
                {
                    c.Close();
                }
            }
        }

        private void UpdateDate()
        {
            _dateButton.Text = _crime.Date.ToLongDateString();
        }

        private string GetCrimeReport()
        {
            var solvedString = GetString(_crime.Solved ?
                Resource.String.crime_report_solved : Resource.String.crime_report_unsolved);

            var suspect = _crime.Suspect;

            if (suspect == null)
            {
                suspect = GetString(Resource.String.crime_report_no_suspect);
            }
            else 
            {
                suspect = GetString(Resource.String.crime_report_suspect, suspect);
            }

            var report = GetString(Resource.String.crime_report, _crime.Title,
                                   _crime.Date.ToLongDateString(), solvedString,
                                   suspect);
            return report;
        }
    }
}

