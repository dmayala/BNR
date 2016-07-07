using System;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Views;
using Android.Widget;
using CriminalIntent.Models;
using DialogFragment = Android.Support.V4.App.DialogFragment;

namespace CriminalIntent.Fragments
{
    public class DatePickerFragment : DialogFragment
    {
        public const string ExtraDate = "la.daya.criminalintent.date";

        const string ArgDate = "date";

        private DatePicker _datePicker;

        public static DatePickerFragment NewInstance(DateTime date)
        {
            var args = new Bundle();
            args.PutLong(ArgDate, date.Ticks);

            var fragment = new DatePickerFragment() { Arguments = args };
            return fragment;
        }

        public override Dialog OnCreateDialog(Bundle savedInstanceState)
        {
            var date = new DateTime(Arguments.GetLong(ArgDate));
            var view = LayoutInflater.From(Activity).Inflate(Resource.Layout.DialogDate, null);
            _datePicker = view.FindViewById<DatePicker>(Resource.Id.DialogDateDatePicker);
            _datePicker.DateTime = date;

            return new AlertDialog.Builder(Activity)
                .SetView(view)
                .SetTitle(Resource.String.date_picker_title)
                .SetPositiveButton(Android.Resource.String.Ok, (sender, e) => {
                    SendResult((int)Result.Ok, _datePicker.DateTime);
                })
                .Create();
        }

        private void SendResult(int resultCode, DateTime date)
        {
            if (TargetFragment == null) return;

            var intent = new Intent();
            intent.PutExtra(ExtraDate, date.Ticks);

            TargetFragment.OnActivityResult(TargetRequestCode, resultCode, intent);
        }
    }
}