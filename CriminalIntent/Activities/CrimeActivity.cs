using System;
using Android.App;
using Android.Content;
using Android.Support.V4.App;
using CriminalIntent.Fragments;

namespace CriminalIntent.Activities
{
    [Activity]
    public class CrimeActivity : SingleFragmentActivity
    {
        private const string ExtraCrimeId = "la.daya.criminalintent.crime_id";

        public static Intent NewIntent(Context packageContext, Guid crimeId)
        {
            var intent = new Intent(packageContext, typeof(CrimeActivity));
            intent.PutExtra(ExtraCrimeId, crimeId.ToString());
            return intent;
        }

        protected override Android.Support.V4.App.Fragment CreateFragment()
        {
            Guid crimeId = new Guid(this.Intent.GetStringExtra(ExtraCrimeId));
            return CrimeFragment.NewInstance(crimeId);
        }
    }
}

