using System;
using System.Collections.Generic;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Support.V4.App;
using Android.Support.V4.View;
using Android.Support.V7.App;
using CriminalIntent.Fragments;
using CriminalIntent.Core.Models;
using CriminalIntent.DAO;

using Fragment = Android.Support.V4.App.Fragment;
using FragmentManager = Android.Support.V4.App.FragmentManager;

namespace CriminalIntent.Activities
{
    [Activity(ParentActivity = typeof(CrimeListActivity))]
    public class CrimePagerActivity : AppCompatActivity, CrimeFragment.ICallbacks
    {
        const string ExtraCrimeId = "la.daya.criminalintent.crime_id";

        private ViewPager _viewPager;
        private List<Crime> _crimes;

        public static Intent NewIntent(Context packageContext, Guid crimeId)
        {
            var intent = new Intent(packageContext, typeof(CrimePagerActivity));
            intent.PutExtra(ExtraCrimeId, crimeId.ToString());
            return intent;
        }

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.CrimePagerActivity);

            var crimeId = new Guid(Intent.GetStringExtra(ExtraCrimeId));
            _viewPager = FindViewById<ViewPager>(Resource.Id.CrimePagerViewPagerActivity);
            _crimes = CrimeLab.Get(this).Crimes;
            _viewPager.Adapter = new CrimeFragmentAdapter(SupportFragmentManager, _crimes);

            var crimeIndex = _crimes.FindIndex(c => c.Id == crimeId);
            _viewPager.CurrentItem = crimeIndex;
        }

        public void OnCrimeUpdated(Crime crime)
        {
        }
    }

    public class CrimeFragmentAdapter : FragmentStatePagerAdapter
    {
        private readonly List<Crime> _crimes;
        public override int Count { get { return _crimes.Count; } }

        public CrimeFragmentAdapter(FragmentManager fm, List<Crime> crimes) : base(fm)
        {
            _crimes = crimes;
        }

        public override Fragment GetItem(int position)
        {
            var crime = _crimes[position];
            return CrimeFragment.NewInstance(crime.Id);
        }
    }
}

